using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class RiskBusiness : EntityBusiness<Risk>
    {
        public override void UpdateSingle(Risk entity)
        {
            var relations = DataContext.RiskGrup.Where(p => p.RiskID == entity.ID).Select(p => p);
            foreach (var relation in relations)
            {
                DataContext.DeleteObject(relation);
            }
            base.UpdateSingle(entity);
        }

        protected override IQueryable<Risk> FilteredQuery(IQueryable<Risk> query, Core.ListingParameters e = null)
        {
            RiskScoreBusiness rsb = new RiskScoreBusiness();
            var listScore = rsb.RetreiveEntityItems();
            RiskMatrisBusiness rmb = new RiskMatrisBusiness();
            var listMatris = rmb.RetreiveEntityItems();
            var q = base.FilteredQuery(query, e);
            if (e != null && e.UserParams.ContainsKey("riskFilter"))
            {
                var filter = (RiskFilter)e.UserParams["riskFilter"];
                if (filter.RiskDurum.HasValue)
                {
                    var durum = filter.RiskDurum.Value.ToString();
                    q = q.Where(p => p.Durum == durum);
                }
            }
            else
            {
                if (e != null && e.UserParams.ContainsKey("StatusFilter"))
                {

                    string onaylandi = RiskDurum.Onaylandı.ToString();
                    string puanlamabekler = RiskDurum.PuanlamaBekler.ToString();
                    string onaybekler = RiskDurum.OnayBekler.ToString();
                    q = q.Where(p => p.Durum == onaylandi || p.Durum == puanlamabekler || p.Durum == onaybekler);

                }
            }

            if (e != null && e.UserParams.ContainsKey("idFilter"))
            {
                var filter = (int[])e.UserParams["idFilter"];
                q = q.Where(p => filter.Contains(p.ID));
            }

            if (e != null && e.UserParams.ContainsKey("ArtikSkorFullInfo"))
            {
                var filter = e.UserParams["ArtikSkorFullInfo"].ToString();
                string[] a = filter.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var first = a[0].Trim();
                var firstTrimmed = first.Trim("%*".ToCharArray());
                int Artikskor = 0;
                if (int.TryParse(firstTrimmed, out Artikskor))
                {
                    q = q.Where(p => p.ArtikSkor == Artikskor);
                }
                else
                {
                    if (first[0] == '%') q = q.Where(p => p.ArtikSkorTanim.EndsWith(firstTrimmed));
                    else
                    {
                        switch (first[first.Length - 1])
                        {
                            case '%': q = q.Where(p => p.ArtikSkorTanim.StartsWith(firstTrimmed));
                                break;
                            case '*': q = q.Where(p => p.ArtikSkorTanim.Contains(firstTrimmed));
                                break;
                        }
                    }
                }
                if (a.Length == 2)
                {
                    if (int.TryParse(firstTrimmed, out Artikskor))
                    {
                        q = q.Where(p => p.ArtikSkor == Artikskor);
                    }

                    var second = a[1].Trim();
                    var secondTrimmed = second.Trim("%*".ToCharArray());
                    if (second[0] == '%') q = q.Where(p => p.ArtikSkorTanim.EndsWith(firstTrimmed));
                    else
                    {
                        switch (second[second.Length - 1])
                        {
                            case '%': q = q.Where(p => p.ArtikSkorTanim.StartsWith(secondTrimmed)); break;
                            case '*': q = q.Where(p => p.ArtikSkorTanim.Contains(secondTrimmed)); break;
                        }
                    }
                }
            }

            if (e != null && e.UserParams.ContainsKey("riskFilter"))
            {
                var filter = (RiskFilter)e.UserParams["riskFilter"];

                if (filter.ArtikSkors.Any())
                {
                    var items = listMatris.Where(p => filter.ArtikSkors.Contains(p.ID)).Select(u => u.GrupDeger).ToList();
                    q = q.Where(p => items.Contains(p.ArtikSkorTanim));
                }

                if (filter.ArtikOlasilikSkors.Any())
                {
                    var items = listScore.Where(p => filter.ArtikOlasilikSkors.Contains(p.ID)).Select(u => u.OlasilikBaslik).ToList();
                    q = q.Where(p => items.Contains(p.ArtikOlasilikSkorTanim));
                }

                if (filter.ArtikEtkiSkors.Any())
                {
                    var items = listScore.Where(p => filter.ArtikEtkiSkors.Contains(p.ID)).Select(u => u.EtkiBaslik).ToList();
                    q = q.Where(p => items.Contains(p.ArtikEtkiSkorTanim));
                }

            }

            return base.FilteredQuery(q, e);
        }

        public void ChangeRiskStatus(Risk entity, Model.Common.RiskDurum riskDurum)
        {
            if (entity.RiskDurum == riskDurum)
                throw new BusinessException(string.Format("Risk zaten {0} durumundadır", riskDurum.ToString()));
            switch (riskDurum)
            {
                case Kalitte.RiskManagement.Framework.Model.Common.RiskDurum.Taslak:
                    new RiskQuestionAnswerBusiness().DeleteAllAnswersOfRisk(entity.ID);
                    entity.RiskDurum = riskDurum;
                    entity.ClearSkor();
                    break;   //aaa
                case Kalitte.RiskManagement.Framework.Model.Common.RiskDurum.PuanlamaBekler:
                    if (!entity.Kontrol.Any())
                        throw new BusinessException("Risk için hiçbir kontrol tanımlı gözükmüyor. Önce kontrolleri tanımlamanız gerekmektedir.");
                    new RiskQuestionAnswerBusiness().DeleteAllAnswersOfRisk(entity.ID);
                    entity.RiskDurum = riskDurum;


                    //entity.ClearSkor();
                    break;
                case Kalitte.RiskManagement.Framework.Model.Common.RiskDurum.Onaylandı:
                    new RiskQuestionAnswerBusiness().ApproveControl(entity);
                    CalculateRiskPuan(entity);
                    entity.RiskDurum = riskDurum;
                    new RiskGecmisBusiness().CreateRiskHistory(entity);
                    break;
                default:
                    break;
            }
            DataContext.SaveChanges();
        }

        private void calculate(List<RiskSoruCevap> userAnswers, List<Cevap> answerDefinitions, List<Soru> questionDefinitions, out double etki, out double olasilik)
        {
            etki = 0;
            olasilik = 0;
            //int totalAnswersEtki = 0;
            int totalAnswersOlasilik = 0;

            var etkianswers = new List<double>();
            var users = userAnswers.Select(p => p.KullaniciID).Distinct();
            var etkigrup = SoruGrupTur.Etki.ToString();
            foreach (var item in users)
            {
                etkianswers.Add(userAnswers.Where(p => p.KullaniciID == item && p.Soru.SoruGrupTanim.GrupTur == etkigrup).Select(u => u.Cevap.Puan).Max());
            }

            if (etkianswers.Count > 0)
                etki = etkianswers.Sum() / etkianswers.Count();

            for (int i = 0; i < userAnswers.Count; i++)
            {
                var userAnswer = userAnswers[i];
                var question = questionDefinitions.Single(p => p.ID == userAnswer.SoruID);
                var answerDefinition = answerDefinitions.Single(p => p.ID == userAnswer.CevapID);
                //if (question.SoruGrupTanim.Tur == SoruGrupTur.Etki)
                //{

                //    etki += Convert.ToInt32(answerDefinition.Puan);
                //    totalAnswersEtki++;
                //}
                if (question.SoruGrupTanim.Tur == SoruGrupTur.Olasilik)
                {
                    olasilik += Convert.ToInt32(answerDefinition.Puan);
                    totalAnswersOlasilik++;
                }
            }

            //if (totalAnswersEtki > 0)
            //{
            //    etki = etki / totalAnswersEtki;
            //}
            if (totalAnswersOlasilik > 0)
            {
                olasilik = olasilik / totalAnswersOlasilik;
            }
        }

        public void CalculateRiskPuan(Risk entity)
        {
            var userAnswers = new RiskQuestionAnswerBusiness().RetreiveByRisk(entity.ID).OrderBy(p => p.Soru.GrupID).ToList();
            var answerDefinitions = new AnswerBusiness().RetreiveByList(userAnswers.Select(p => p.CevapID).ToArray());
            var questionDefinitions = new QuestionBusiness().RetreiveByList(userAnswers.Select(p => p.SoruID).ToArray());

            double artikEtki = 0, artikOlasilik = 0;
            calculate(userAnswers, answerDefinitions, questionDefinitions, out artikEtki, out artikOlasilik);

            entity.ArtikEtki = artikEtki;
            entity.ArtikOlasilik = artikOlasilik;
            entity.ArtikSkor = entity.ArtikEtki * entity.ArtikOlasilik;
            if (entity.ArtikSkor < 1)
                throw new BusinessException("Risk henüz hiç bir kullanıcı tarafından puanlanmamıştır.");
            CalculateSkorVisualization(entity);
        }

        public void CalculateSkorVisualization(Risk entity)
        {
            var matrix = new RiskMatrisBusiness().GetMatrixAsList();
            var itemScore = RiskMatrisBusiness.GetMatixStore(matrix, entity.ArtikSkor.Value);
            var scoreItems = new RiskScoreBusiness().RetreiveEntityItems().OrderBy(p => p.Deger).ToList();

            var artikScore = RiskMatrisBusiness.GetMatixStore(matrix, entity.ArtikSkor.Value);
            entity.ArtikEtkiSkorTanim = scoreItems.SingleOrDefault(p => p.Deger == Math.Round(entity.ArtikEtki.Value)).EtkiBaslik;
            entity.ArtikSkorRenk = artikScore.Color;
            entity.ArtikSkorTanim = artikScore.Display;
            entity.ArtikOlasilikSkorTanim = scoreItems.SingleOrDefault(p => p.Deger == Math.Round(entity.ArtikOlasilik.Value)).OlasilikBaslik;

        }

        public List<Risk> GetRisksbyKullaniciID(Guid kullaniciId, int calismagrupid)
        {
            string durum = RiskDurum.PuanlamaBekler.ToString();
            return GetQueryable().Where(p => p.KullaniciID == kullaniciId && p.Durum == durum && p.CalisanGrupID == calismagrupid).ToList();
        }


        public List<Risk> GetAllUserRisks(Guid userID)
        {
            return GetQueryable().Where(p => p.KullaniciID == userID).ToList();

        }


        public void ValidateUsersRiskStatus(Guid userID)
        {
            var risks =GetUnitWorkerRisks(userID);
            var cgb=new CalismaGrupKullaniciBusiness();
            foreach (var risk in risks)
            {
                risk.Durum = cgb.CheckAllUserAnswers(risk) ? RiskDurum.OnayBekler.ToString() : RiskDurum.PuanlamaBekler.ToString();
            }            
            DataContext.SaveChanges();
        }

        public IQueryable<Risk> GetFilteredQuery(IQueryable<Risk> query, Core.ListingParameters e = null)
        {
            return this.FilteredQuery(PermissionQuery(query, e), e);
        }

        public List<RiskReportData> RetreiveItemsWithControls(Core.ListingParameters lp, bool isGecmis = true)
        {
            var ReportDataList = new List<RiskReportData>();
            var risks = GetFilteredQuery(DataContext.Risk, lp).Select(s => s.ID).Distinct().ToList();
            var controls = (from c in new RiskControlBusiness().GetQueryable().Where(w => risks.Contains(w.RiskID))
                            join risk in DataContext.Risk
                            on c.RiskID equals risk.ID
                            select new RiskReportData()
                            {
                                RiskID = risk.ID,
                                RiskAd = risk.Ad,
                                RiskDurum = risk.Durum,
                                SurecAd = risk.Surec.Ad,
                                BirimAd = risk.Surec.Birim.Ad,
                                KontrolAd = c.Ad,
                                StratejikHedef = risk.Surec.Hedef,
                                EtkiPuan = risk.ArtikEtki,
                                OlasilikPuan = risk.ArtikOlasilik,
                                SkorPuan = risk.ArtikSkor,
                                EtkiSkorTanim = risk.ArtikEtkiSkorTanim,
                                OlasilikSkorTanim = risk.ArtikOlasilikSkorTanim,
                                SkorTanim = risk.ArtikSkorTanim,
                                Tarih = risk.KayitTarih,
                                KullaniciAd = risk.aspnet_Users.Ad + " " + risk.aspnet_Users.Soyad,
                                SkorRenk = risk.ArtikSkorRenk,
                            }).ToList();

            RiskGecmisBusiness rgb = new RiskGecmisBusiness();
            if (isGecmis)
            {
                var gecmis = rgb.GetQueryable().Where(w => risks.Contains(w.RiskID)).Select(s => new { RiskID = s.RiskID, Renk = s.ArtikSkorRenk, Tarih = s.KayitTarih, ArtikSkor = s.ArtikSkor, ArtikSkorTanim = s.ArtikSkorTanim }).ToList();
                foreach (var item in controls)
                {
                    var sGecmis = gecmis.Where(w => w.RiskID == item.RiskID).OrderByDescending(o => o.Tarih).Take(2).ToList();
                    if (sGecmis.Count > 1)
                    {
                        item.OncekiSkor = string.Format("{0} - {1}", Math.Round(sGecmis[1].ArtikSkor, 2), sGecmis[1].ArtikSkorTanim);
                        item.OncekiSkorRenk = sGecmis[1].Renk;
                    }
                    else
                    {
                        item.OncekiSkor = string.Empty;
                        item.OncekiSkorRenk = string.Empty;
                    }

                }
            }
            return controls;
        }

        public List<Risk> GetRisksByWorkflowID(int id)
        {
            return DataContext.Risk.Where(d => d.SurecID == id).ToList();
        }

        public void ResetRiskScore()
        {
            DataContext.ResetRiskScores();
        }


        public List<Risk> GetUnitWorkerRisks(Guid userID, ListingParameters parameters = null)
        {
            var baseQuery = GetQueryable(parameters);
          var   query = baseQuery.Where(d => d.CalismaGrupTanim.CalismaGrupKullanici.Any(x => x.KatilimciKullaniciID == userID));
          query = CreateListingQuery(query, parameters);
            
            return ExecuteListQuery(query);
        }

    }
}
