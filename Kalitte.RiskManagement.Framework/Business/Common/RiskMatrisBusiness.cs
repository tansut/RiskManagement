using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    [Serializable]
    public class RiskMatrisEntity
    {
        public int Score { get; set; }
        public string Display { get; set; }
        public string Color { get; set; }
        public string RiskDefinitionName { get; set; }
        public string ProbabilityDefinitionName { get; set; }
    }

    public class RiskMatrisBusiness: EntityBusiness<RiskMatrisTanim>
    {
        public RiskMatrisEntity[,] GetMatrix()
        {
            List<RiskMatrisTanim> allData = RetreiveEntityItems();
            var scoreItems = new RiskScoreBusiness().RetreiveEntityItems().OrderBy(p=>p.Deger).ToList();
            RiskMatrisEntity[,] result = new RiskMatrisEntity[scoreItems.Count, scoreItems.Count];
            for (int i = 0; i < scoreItems.Count; i++)
            {
                for (int j = 0; j < scoreItems.Count; j++)
                {
                    var deger = scoreItems[j].Deger * scoreItems[i].Deger;
                    var matrisData = allData.FirstOrDefault(p => deger >= p.PuanBaslangic && deger <= p.PuanBitis);
                    if (matrisData != null)
                        result[i, j] = new RiskMatrisEntity() { Score = deger, 
                            Display = matrisData.GrupDeger, Color = matrisData.Renk, 
                            RiskDefinitionName = scoreItems[j].EtkiBaslik,
                                                                ProbabilityDefinitionName = scoreItems[i].OlasilikBaslik,
                        };
                    else result[i, j] = new RiskMatrisEntity();
                }
            }
            return result;
        }

        public List<RiskMatrisEntity> GetMatrixAsList()
        {
            var matris = this.GetMatrix();
            var result = new List<RiskMatrisEntity>();
            for (int i = 0; i <= matris.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matris.GetUpperBound(1); j++)
                {
                    result.Add(matris[i, j]);
                }
            }
            return result;
        }

        public static RiskMatrisEntity GetMatixStore(List<RiskMatrisEntity> matrix, double score)
        {
            foreach (var item in matrix)
            {
                if (item.Score >= score)
                    return item;
            }
            return null;
        }

        public List<string> GetGrupDeger() 
        {
            return GetQueryable().Select(p => p.GrupDeger).Distinct().ToList();
        }
        public int GetGrupDegerID(string grupDeger)
        {
            return GetQueryable().Where(u => u.GrupDeger == grupDeger).Select(p => p.ID).Single();
        }

        public List<int> GetGrupDegerIDs()
        {
            return GetQueryable().Select(p => p.ID).ToList();
        }

    }
}
