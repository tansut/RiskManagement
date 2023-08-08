using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using System.Collections;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Utility;
using System.Web;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class AutoCompleteEventArgs : EventArgs
    {
        public IList Data { get; set; }
        public ListingParameters Parameters { get; private set; }

        public AutoCompleteEventArgs(ListingParameters parameters)
        {
            Data = null;
            Parameters = parameters;
        }
    }

    public class TTAutoComplete: TTComboBox
    {

        public event EventHandler<AutoCompleteEventArgs> AutoCompleteEvent;
        public bool UseAutoCompleteEngine { get; set; }

        public string EngineGroup { get; set; }
        public string EngineField { get; set; }

        public TTAutoComplete()
            : base()
        {
            Editable = true;
            TypeAhead = false;
            TriggerAction = Ext.Net.TriggerAction.Query;
            SelectOnFocus = false;
            DisplayField = "Value";
            ValueField = "Key";
            LoadingText = "Yükleniyor...";
            PageSize = 10;
            HideTrigger = true;
            MinChars = 1;
            ForceSelection = false;
            Mode = DataLoadMode.Default;
            UseAutoCompleteEngine = false;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (UseAutoCompleteEngine)
            {
                Store.Clear();
                TTStore str = new TTStore();
                str.AutoLoad = false;
                JsonReader reader = new JsonReader();
                reader.IDProperty = "ID";
                reader.Fields.Add(new RecordField("ID"));
                reader.Fields.Add(new RecordField("Deger"));
                str.Reader.Add(reader);
                Store.Add(str);
                ValueField = "ID";
                DisplayField = "Deger";

            }
            var store = GetStore();
            if (store != null)
                store.RefreshData += new Ext.Net.Store.AjaxRefreshDataEventHandler(store_RefreshData);
        }

        void store_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var store = sender as Store;
            var parameters = new ListingParameters(e);

            if (UseAutoCompleteEngine)
            {
                var bll = new AutoCompleteBusiness();
                parameters.FieldFilters.Add(new FilteringInfo("Grup", this.EngineGroup));
                parameters.FieldFilters.Add(new FilteringInfo("Alan", this.EngineField));
                store.DataSource = bll.RetreiveEntityItems(parameters);
            }
            else if (AutoCompleteEvent != null)
            {
                
                var args = new AutoCompleteEventArgs(parameters);
                AutoCompleteEvent(this, args);
                if (args.Data != null)
                {
                    store.DataSource = args.Data;
                    store.DataBind();
                }
            }
        }

        public void Initialize(int value, string display)
        {
            var store = GetStore() as TTStore;
            store.AddScript(string.Format("{0}.addRecord({1});", store.ClientID,
                "{" + ValueField + ":" + value.ToString() + "," + DisplayField + ":'" + HttpUtility.JavaScriptStringEncode(display) + "'}"));
            SelectedItem.Value = value.ToString();
            store.RejectChanges();
        }

        public void Initialize(string value, string display)
        {
            var store = GetStore() as TTStore;
            store.AddScript(string.Format("{0}.addRecord({1});", store.ClientID,
                "{" + ValueField + ":'" + HttpUtility.JavaScriptStringEncode(value) + "'," + DisplayField + ":'" + HttpUtility.JavaScriptStringEncode(display) + "'}"));
            SelectedItem.Value = value;
            store.RejectChanges();
        }

        public void EndEdit()
        {
            int selectedId;
            if (UseAutoCompleteEngine && !string.IsNullOrWhiteSpace(this.SelectedAsString) && this.SelectedAsString == this.SelectedText)
            {
                new AutoCompleteBusiness().AddToList(this.EngineGroup, this.EngineField, this.SelectedText);
            }
        }
    }
}
