using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class RiskQuestionAnswerBusiness : EntityBusiness<RiskSoruCevap>
    {
        public RiskQuestionAnswerBusiness()
        {

        }

        public List<RiskSoruCevap> RetreiveByRisk(int riskId)
        {
            var query = GetQueryable().Where(p => p.RiskID == riskId);
            return ExecuteListQuery(query);
        }

        public List<RiskSoruCevap> RetreiveByRiskAndUser(int riskId, Guid userId)
        {
            var query = GetQueryable().Where(p => p.RiskID == riskId && p.KullaniciID == userId);
            return ExecuteListQuery(query);
        }

        public List<RiskSoruCevap> RetreiveByRiskListAndUser(List<Risk> risks, List<Guid> userId)
        {
            var riskid = risks.Select(p => p.ID).ToList();
            return GetQueryable().Where(p => riskid.Contains(p.RiskID) && userId.Contains(p.KullaniciID)).ToList();
        }

        public void ApproveControl(Risk risk)
        {
       //     if (!GetQueryable().Where(p => p.RiskID == risk.ID && p.KullaniciID == risk.KullaniciID).Any())
            if (risk.KullaniciID!=AuthenticationManager.CurrentUserID)
                throw new BusinessException("Riski onaylayabilmeniz için risk sahibinin puanlama yapması gerekmektedir.");

            var dairebaskani = "Daire Başkanı";
            var list = new UserBusiness().GetQueryable().Where(p => p.BirimID == risk.BirimID && p.Unvan.Ad == dairebaskani).Select(u => u.UserId).ToList();
            if (list.Count > 0)
            {
                if (!GetQueryable().Where(p => p.RiskID == risk.ID && list.Contains(p.KullaniciID)).Any())
                    throw new BusinessException("Riski onaylayabilmeniz için ilgili Daire Başkanının puanlama yapması gerekmektedir.");
            }
            else
                throw new BusinessException("İlgili birime ait Daire Başkanı bulunmadığından Risk onaylanamadı.");
        }

        public void UpdateUserAnswers(Risk entity, Dictionary<int, int> userAnswers)
        {
            var currentAnswersToRemove = GetQueryable().Where(p => p.KullaniciID == AuthenticationManager.CurrentUserID &&
                p.RiskID == entity.ID);
            foreach (var existingAnswer in currentAnswersToRemove)
            {
                DataContext.DeleteObject(existingAnswer);
            }
            foreach (var item in userAnswers)
            {
                var newEntity = new RiskSoruCevap();
                newEntity.RiskID = entity.ID;
                newEntity.SoruID = item.Key;
                newEntity.CevapID = item.Value;
                Insert(newEntity);
            }
            DataContext.SaveChanges();
            validateRiskStatus(entity.ID);

        }

        private void validateRiskStatus(int riskID )
        {
            var entity = new RiskBusiness().Retrieve(riskID);
            entity.Durum = new CalismaGrupKullaniciBusiness().CheckAllUserAnswers(entity) ? RiskDurum.OnayBekler.ToString() : RiskDurum.PuanlamaBekler.ToString();
            DataContext.SaveChanges();
        }



        internal void DeleteAllAnswersOfRisk(int riskId)
        {
            var query = DataContext.RiskSoruCevap.Where(p => p.RiskID == riskId);
            foreach (var item in query)
            {
                DataContext.DeleteObject(item);
            }
        }

    }
}
