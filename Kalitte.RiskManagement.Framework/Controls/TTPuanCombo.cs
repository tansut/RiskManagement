using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTPuanCombo : TTComboBox
    {
        public TTPuanCombo()
            : base()
        {
            FieldLabel = "Puan";
            AllowBlank = false;
            DisplayField = "Value";
            ValueField = "Key";
            Editable = false;
            AllowBlank = false;
            AutoDataBind = true;
            TTStore store = new TTStore();
            store.AutoDataBind = true;
            store.UseServerSidePaging = false;
            JsonReader reader = new JsonReader();
            reader.IDProperty = "Key";
            reader.Fields.Add("Key");
            reader.Fields.Add("Value");
            store.Reader.Add(reader);
            this.Store.Add(store);
        }

        public SoruGrupTur Tur
        {
            get
            {
                if (ViewState["t"] == null)
                    return SoruGrupTur.Etki;
                else return (SoruGrupTur)ViewState["t"];
            }
            set
            {
                ViewState["t"] = value;
            }
        }

        public override void DataBind()
        {
            var list = new List<KeyValuePair<int, string>>();
            if (Tur == SoruGrupTur.Etki)
            {
                for (int i = 1; i < 6; i++)
                {
                    list.Add(new KeyValuePair<int, string>(i, i.ToString()));
                }
            }
            else
            {
                for (int i = 1; i < 6; i++)
                {
                    list.Add(new KeyValuePair<int, string>(i, i.ToString()));
                }
                //for (int i = 5; i < 100; i += 5)
                //{
                //    list.Add(new KeyValuePair<int, string>(i, string.Format("{0}%", i)));
                //}
            }
            Store.Primary.DataSource = list;
            Store.Primary.DataBind();
            base.DataBind();
        }





        public double Puan
        {
            get
            {
                return Convert.ToDouble(SelectedAsInt);
            }

            set
            {
                SelectedAsInt = Convert.ToInt32(value);
            }
        }
    }
}
