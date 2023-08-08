using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public enum WizardStep
    {
        Welcome,
        Kontrol,
        ArtikRisk,
        OzetOnay
    }

    public class SummaryInfo
    {
        public string Soru { get; set; }
        public string SoruGrup { get; set; }
        public string Cevap { get; set; }
        public string SoruYardim { get; set; }
        public string CevapYardim { get; set; }
    }

    public partial class answerseditor : EditorViewControl<RiskQuestionAnswerBusiness>, IQuestionDataProvider
    {
        public Dictionary<int, int> CurrentAnswers
        {
            get
            {
                if (ViewState["ca"] == null)
                    return new Dictionary<int, int>();
                else return (Dictionary<int, int>)ViewState["ca"];
            }
            set
            {
                ViewState["ca"] = value;
            }
        }


        public WizardStep CurrentStep
        {
            get
            {
                if (ViewState["step"] == null)
                    return WizardStep.Welcome;
                else return (WizardStep)ViewState["step"];
            }
            set
            {
                ViewState["step"] = value;
            }
        }

        public int QuestionCount
        {
            get
            {
                if (ViewState["qc"] == null)
                    return 0;
                else return (int)ViewState["qc"];
            }
            private set
            {
                ViewState["qc"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var riskEntity = new RiskBusiness().Retrieve(command.RecordID);
            if (riskEntity.RiskDurum != RiskDurum.PuanlamaBekler && riskEntity.RiskDurum != RiskDurum.OnayBekler)
                    throw new BusinessException("Sadece PuanlamaBekler ve OnayBekler durumundaki riskler için puanlama yapılabilir");
            if (!riskEntity.CalismaGrupTanim.CalismaGrupKullanici.Any(p => p.KatilimciKullaniciID == AuthenticationManager.CurrentUserID))
                throw new BusinessException("Risk puanlama grubunda olmadığınız için puanlama yapma yetkiniz bulunmamaktadır.");
            CurrentID = riskEntity.ID;
            var currentAnswersOfuser = BusinessObject.RetreiveByRiskAndUser(riskEntity.ID, AuthenticationManager.CurrentUserID);
            var bindings = CurrentAnswers;
            bindings.Clear();
            foreach (var item in currentAnswersOfuser)
            {
                bindings.Add(item.SoruID, item.CevapID);
            }
            CurrentAnswers = bindings;
            ctlReadControlsCheck.Checked = false;
            ChangeUiState(WizardStep.Welcome);
            entityWindow.Title = "Risk Puanlama Sihirbazı";
            ctlRiskName.Html = string.Format("<b><font size='2' Color=#B40404>{0}</font></b>", HttpUtility.HtmlEncode(riskEntity.Ad));
            if (!string.IsNullOrWhiteSpace(riskEntity.Aciklama))
                ctlRiskName.Html += string.Format(" - {0}", HttpUtility.HtmlEncode(riskEntity.Aciklama));
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "PreviousPage")]
        public void PreviosPageHandler(object sender, CommandInfo command)
        {
            WizardStep newStep = CurrentStep - 1;
            ChangeUiState(newStep);
        }



        [CommandHandler(CommandName = "ApprovePuanWizard")]
        public void ApprovePuanWizardHandler(object sender, CommandInfo command)
        {
            var entity = new RiskBusiness().Retrieve(CurrentID);
            BusinessObject.UpdateUserAnswers(entity, CurrentAnswers);
           
            
            entityWindow.Hide();
            PageInstance.GetLister<RiskBusiness>().LoadItems();
            WebHelper.ShowMessage("Puanlama başarıyla tamamlandı, teşekkürler.");
        }

        [CommandHandler(CommandName = "NextPage")]
        public void NextPageHandler(object sender, CommandInfo command)
        {
            WizardStep newStep = CurrentStep + 1;
            ChangeUiState(newStep);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ctlQList.Provider = this;
        }

        private void validateUiState(WizardStep newStep)
        {
            switch (newStep)
            {
                case WizardStep.Welcome:
                    break;
                case WizardStep.Kontrol:
                    //foreach (var question in CurrentQuestions)
                    //{
                    //    if (question.ZorunlulukDurumu == Mantiksal.Evet.ToString() && !CurrentAnswers.Keys.Contains(question.ID))
                    //        throw new BusinessException("Zorunlu soruları yanıtlamanız gerekmektedir: " + question.Ad);
                    //}
                    break;
                case WizardStep.ArtikRisk:
                    if (!ctlReadControlsCheck.Checked)
                        throw new BusinessException("Lütfen kontrolleri okuduğunuzu onaylayınız");
                    break;
                case WizardStep.OzetOnay:
                    foreach (var question in CurrentQuestions)
                    {
                        if (question.ZorunlulukDurumu == Mantiksal.Evet.ToString() && !CurrentAnswers.ContainsKey(question.ID))
                            throw new BusinessException("Zorunlu soruları yanıtlamanız gerekmektedir: " + question.Ad);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeUiState(WizardStep wizardStep)
        {
            validateUiState(wizardStep);
            CurrentStep = wizardStep;
            switch (wizardStep)
            {
                case WizardStep.Welcome:
                    {
                        ctlWizardPanel.ActiveIndex = 0;
                        ctlPrev.Disabled = true;
                        ctlNext.Text = "Sonraki Adım";
                        ctlNext.Icon = Ext.Net.Icon.NextGreen;
                        ctlNext.CommandName = "NextPage";
                        break;
                    }
                //case WizardStep.Risk:
                //    ctlPrev.Disabled = false;
                //    ctlWizardPanel.ActiveIndex = 1;
                //    ctlQList.LoadItems();
                //    ctlQList.Title = "<u>Kontrollerin uygulanmadığı varsayımıyla</u> aşağıdaki soruları yanıtlayınız.";
                //    ctlQList.CurrentTip = RiskTip.Risk;
                //    break;
                case WizardStep.Kontrol:
                    ctlWizardPanel.ActiveIndex = 2;
                    var controls = new RiskControlBusiness().RetreiveItemsOfRisk(CurrentID);
                    if (controls.Count == 0)
                    {
                        entityWindow.Hide();
                        WebHelper.ShowMessage("Risk üzerinde hiçbir kontrol tanımlı olmadığı için işlem durdurulmuştur.", MessageType.Error);
                    }
                    else
                    {
                        ctlControlGrid.Store.Primary.DataSource = controls;
                        ctlControlGrid.Store.Primary.DataBind();
                    }
                    break;
                case WizardStep.ArtikRisk:
                    ctlNext.Text = "Sonraki Adım";
                    ctlNext.Icon = Ext.Net.Icon.NextGreen;
                    ctlNext.CommandName = "NextPage";
                    ctlWizardPanel.ActiveIndex = 1;
                    ctlQList.LoadItems();
                    ctlQList.Title = "<u>Kontrollerin uygulandığı varsayımıyla</u> aşağıdaki soruları yanıtlayınız.";
                    break;
                case WizardStep.OzetOnay:
                    ctlWizardPanel.ActiveIndex = 3;
                    ctlNext.Text = "Onayla";
                    ctlNext.CommandName = "ApprovePuanWizard";
                    ctlNext.Icon = Ext.Net.Icon.Accept;
                    //ctlRiskSummaryGrid.GetStore().DataSource = GetSummary(RiskTip.Risk);
                    //ctlRiskSummaryGrid.GetStore().DataBind();
                    ctlArtikRiskSummaryGrid.GetStore().DataSource = GetSummary();
                    ctlArtikRiskSummaryGrid.GetStore().DataBind();
                    break;
                default:
                    break;
            }
        }

        private List<SummaryInfo> GetSummary()
        {
            var result = new List<SummaryInfo>();
            var riskEntity = new RiskBusiness().Retrieve(CurrentID);

            List<Soru> questions = new QuestionBusiness().RetreiveActiveArtikRiskQuestions(riskEntity);

            foreach (var question in questions)
            {
                var summary = new SummaryInfo() { Soru = question.Ad, SoruYardim = question.Yardim, SoruGrup = question.GrupAd };
                if (CurrentAnswers.ContainsKey(question.ID))
                {
                    var answer = new AnswerBusiness().Retrieve(CurrentAnswers[question.ID]);
                    summary.Cevap = answer.Ad;
                    summary.CevapYardim = answer.Yardim;
                }
                result.Add(summary);
            }
            return result;
        }


        #region IQuestionDataProvider Members


        public List<Soru> CurrentQuestions
        {
            get
            {
                List<Soru> questionsToAsk = null;
                QuestionBusiness bll = new QuestionBusiness();
                var riskEntity = new RiskBusiness().Retrieve(CurrentID);
                switch (CurrentStep)
                {
                    case WizardStep.Welcome:
                        break;
                 
                    case WizardStep.Kontrol:
                        break;
                    case WizardStep.ArtikRisk:
                        questionsToAsk = bll.RetreiveActiveArtikRiskQuestions(riskEntity);
                        QuestionCount = questionsToAsk.Count;

                        break;
                    case WizardStep.OzetOnay:
                        break;
                    default:
                        break;
                }
                return questionsToAsk;
            }
        }

        public void UpdateAnswer(int questionId, int answerId)
        {
            var bindings = CurrentAnswers;
            if (bindings.ContainsKey(questionId))
                bindings[questionId] = answerId;
            else bindings.Add(questionId, answerId);
            CurrentAnswers = bindings;
        }

        public void NexStep()
        {
            NextPageHandler(null, null);
        }

        #endregion
    }
}