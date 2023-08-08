using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class QuestionBusiness: EntityBusiness<Soru>
    {
        public override void UpdateSingle(Soru entity)
        {
            base.UpdateSingle(entity);
        }

        public void UpdateSingle(Soru entity, List<Cevap> answers)
        {
            var currentAnswers = entity.Cevap.ToList();
            int order = 0;
            foreach (var newAnswer in answers)
            {
                var existingAnswer = currentAnswers.SingleOrDefault(p => p.ID == newAnswer.ID);
                newAnswer.Sira = order++;
                if (existingAnswer != null)
                {
                    existingAnswer.Ad = newAnswer.Ad;
                    existingAnswer.Puan = newAnswer.Puan;
                    existingAnswer.Yardim = newAnswer.Yardim;
                    existingAnswer.Sira = newAnswer.Sira;
                }
                else
                {
                    newAnswer.SoruID = entity.ID;
                    entity.Cevap.Add(newAnswer);
                }
            }
            foreach (var existinganswer in currentAnswers)
            {
                if (!answers.Any(p => p.ID == existinganswer.ID))
                {
                    DataContext.DeleteObject(existinganswer);
                }
            }

            UpdateSingle(entity);
            
        }

        private List<Soru> RetreiveActiveQuestions(Risk entity)
        {
            var etkin = Mantiksal.Evet.ToString();
            var grouptypes = new string[] { SoruGrupTur.Etki.ToString(), SoruGrupTur.Olasilik.ToString() };
            var questionGroupBindings = new RiskGroupQuestionGroupBusiness().RetreiveAllItemsOfGroup(entity.RiskGrup.Select(p => p.RiskGrupID).ToArray()).Select(p=>p.SoruGrupID).Distinct().ToArray();
            var query = GetQueryable();
            query = CreateListingQuery(query).Where(p => p.EtkinDurum == etkin && p.SoruGrupTanim.EtkinDurum == etkin && grouptypes.Contains(p.SoruGrupTanim.GrupTur) && questionGroupBindings.Contains(p.SoruGrupTanim.ID));
            query = query.OrderBy(p => p.SoruGrupTanim.Ad);
            return ExecuteListQuery(query);
        }



        public List<Soru> RetreiveActiveArtikRiskQuestions(Risk entity)
        {
            return RetreiveActiveQuestions(entity);
        }

        internal List<Soru> RetreiveByList(int[] idList)
        {
            var query = GetQueryable().Where(p => idList.Contains(p.ID));
            return ExecuteListQuery(query);
        }
    }
}
