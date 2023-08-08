using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public enum StoreProxyType
    {
        Page,
        HttpPost
    }

    public class TTStore : Store
    {
        public virtual bool UseServerSidePaging { get; set; }
        public StoreProxyType ProxyType { get; set; }



        public TTStore()
            : base()
        {
            UseServerSidePaging = true;
            ProxyType = StoreProxyType.Page;
        }


        protected override void OnInit(EventArgs e)
        {
            if (UseServerSidePaging)
            {
                CheckAndAddBaseParameter("start", "0");
                CheckAndAddBaseParameter("limit", "25");
                CheckAndAddBaseParameter("sort", "''");
                CheckAndAddBaseParameter("dir", "''");
                RemoteSort = true;
                if (ProxyType == StoreProxyType.Page)
                {
                    if (!Proxy.Any()) Proxy.Add(new PageProxy());
                }
                else if (!Proxy.Any()) Proxy.Add(new HttpProxy() { Method = HttpMethod.POST });
            }
            base.OnInit(e);
        }

        protected void CheckAndAddBaseParameter(string name, string value)
        {
            if (!BaseParams.Any(p => p.Name == name))
            {
                BaseParams.Add(new Parameter(name, value, ParameterMode.Raw)); 
            }
        }

        public PageProxy PageProxy
        {
            get
            {
                if (UseServerSidePaging)
                    return Proxy[0] as PageProxy;
                return null;
            }
        }

        public HttpProxy HttpProxy
        {
            get
            {
                if (UseServerSidePaging)
                    return Proxy[0] as HttpProxy;
                return null;
            }
        }
    }
}
