using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class DuyuruBusiness : EntityBusiness<Duyuru>
    {
        public List<Duyuru> GetCurrentAnnouncements()
        {
            return GetQueryable().Where(w => w.BaslangicTarihi >= DateTime.Today && w.BitisTarihi <= DateTime.Today).ToList();
        }
    }
}
