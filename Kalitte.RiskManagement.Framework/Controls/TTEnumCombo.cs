using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTEnumCombo: TTComboBox
    {
        public string EnumType { get; set; }

        private TTEnumStore store = null;

        public TTEnumCombo()
            : base()
        {
            DisplayField = "Value";
            ValueField = "Key";
            Editable = false;
            AllowBlank = false;
            AutoDataBind = true;
        }

        public override Ext.Net.StoreCollection Store
        {
            get
            {
                CreateStore();
                if (store != null)
                {
                    var collection = base.Store;
                    if (collection.Count == 0)
                    {
                        collection.Add(store);
                        if (AutoDataBind && store != null && !Ext.Net.X.IsAjaxRequest)
                            store.DataBind();
                    }
                    return collection;
                }
                else return base.Store;
            }
        }

        

        public T GetSelectedAsType<T>() where T: struct
        {
            Type t = typeof(T);
            T result;
            if (Enum.TryParse<T>(SelectedAsString, out result))
                return result;
            else throw new IndexOutOfRangeException("Invalid Enum Value:" + SelectedAsString);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CreateStore();
        }

        private void CreateStore()
        {
            if (store == null && !string.IsNullOrEmpty(EnumType))
            {
                store = new TTEnumStore();
                store.ID = this.ID + "store";
                store.EnumType = this.EnumType;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

    }
}
