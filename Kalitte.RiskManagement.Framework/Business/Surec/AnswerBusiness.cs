using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class AnswerBusiness: EntityBusiness<Cevap>
    {
        public List<Cevap> RetreiveItemsOfQuestion(int questionId)
        {
            return GetQueryable().Where(p => p.SoruID == questionId).OrderBy(p => p.Sira).ToList();
        }



        internal List<Cevap> RetreiveByList(int[] idList)
        {
            var query = GetQueryable().Where(p=>idList.Contains(p.ID));
            return ExecuteListQuery(query);
        }
    }
}
