using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Model.Common;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public interface IQuestionDataProvider
    {
        Dictionary<int, int> CurrentAnswers { get; }
        List<Soru> CurrentQuestions { get; }
        void UpdateAnswer(int questionId, int answerId);
        int QuestionCount { get; }
        void NexStep();

    }

    public partial class questionlist : ListerViewControl<QuestionBusiness>
    {
        Dictionary<string, string> groupUsers;

        public IQuestionDataProvider Provider { get; set; }

        public override void DataBindStore(System.Collections.IList items)
        {
            base.DataBindStore(items);
            grid.SelectRow(0);
        }

        private Dictionary<string, string> UserGroupUsers
        {
            get
            {
                if (groupUsers == null)
                {
                    var items = new CalismaGrupBusiness().GetAllGroupUsersOfCurrentUser();
                    groupUsers = new Dictionary<string, string>(items.Count);
                    items.ForEach(p => groupUsers.Add(p.UserName, p.AdSoyad));
                }
                return groupUsers;
            }
        }

        protected override TTStore LocateListerStore()
        {
            return this.dsMain;
        }

        protected override TTGrid LocateGrid()
        {
            return this.grid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Title
        {
            get
            {
                return ctlMessage.Text;
            }
            set
            {
                ctlMessage.Html = string.Format(" <h2>{0}</h2>", value);
            }
        }

        protected void SelectQuestionHandler(object sender, DirectEventArgs e)
        {
            var id = int.Parse(grid.RowSelection.SelectedRecordID);
            var entity = BusinessObject.Retrieve(id);
            ctlGroupName.Html = string.Format("<h3 style='color:gray'>{0}</h3>", HttpUtility.HtmlEncode(entity.GrupAd));
            ctlQuestionName.Html = string.Format("<h2>{0}</h2>", entity.Ad);
            ctlQuestionName.ToolTips.Clear();
            ctlQuestionName.ToolTips.Add(new ToolTip() { Html = entity.Yardim });
            ctlAnswerPanel.DoLayout();
            BindAnswerGrid(id);
            ctlPrevQuestion.Disabled = grid.RowSelection.SelectedIndex == 0;
        }

        private void BindAnswerGrid(int questionId)
        {
            var answers = new AnswerBusiness().RetreiveItemsOfQuestion(questionId);
            ctlAnswerGrid.Store.Primary.DataSource = answers;
            ctlAnswerGrid.Store.Primary.DataBind();
            if (Provider.CurrentAnswers.Keys.Contains(questionId))
                ctlAnswerGrid.SelectById(Provider.CurrentAnswers[questionId]);
            else ctlAnswerGrid.ClearSelections();
        }

        private int CurrentQuestion
        {
            get
            {
                return int.Parse(grid.RowSelection.SelectedRecordID);
            }
        }

        private int? CurrentAnswer
        {
            get
            {
                return ctlAnswerGrid.CheckboxSelection.SelectedRow == null ? null : new int?(int.Parse(ctlAnswerGrid.CheckboxSelection.SelectedRecordID));
            }
        }

        [CommandHandler(CommandName = "SelectNextQuestion")]
        protected void SelectNextQuestionHandler(object sender, CommandInfo e)
        {
            if (!CurrentAnswer.HasValue)
                throw new BusinessException("Lütfen soruya yanıt veriniz");
            Provider.UpdateAnswer(CurrentQuestion, CurrentAnswer.Value);
            if (Provider.QuestionCount - 1 == grid.RowSelection.SelectedIndex)
                Provider.NexStep();
            else grid.SelectNext();
        }

        [CommandHandler(CommandName = "SelectPreviousQuestion")]
        protected void SelectPreviousQuestionHandler(object sender, CommandInfo e)
        {
            grid.SelectPrevious();
        }

        [CommandHandler(CommandName = "ClearAnswer")]
        protected void ClearAnswerHandler(object sender, CommandInfo e)
        {
            ctlAnswerGrid.ClearSelections();
            if (Provider.CurrentAnswers.Keys.Contains(CurrentQuestion))
                Provider.CurrentAnswers.Remove(CurrentQuestion);
        }

        protected override System.Collections.IList GetItems()
        {
            var items = Provider.CurrentQuestions;
            return items;
        }
    }
}