using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Business;
using Ext.Net.Utilities;
using Ext.Net;
using System.Collections;

namespace Kalitte.RiskManagement.Framework.UI
{
    public abstract class ViewControlBase<T> : BaseViewUserControl, ICommandSource where T : BusinessBase
    {
        public virtual T BusinessObject { get; set; }

        public virtual void HideAllWindows()
        {
            ControlUtils.FindControls<Window>(this).ForEach(new Action<Window>((w) => { w.Hide(); }));
        }

        public ListerViewControl<T> CurrentLister
        {
            get {
                return PageInstance.GetLister<T>();
            }
        }

        public EditorViewControl<T> CurrentEditor
        {
            get
            {
                return PageInstance.GetEditor<T>();
            }
        }



        #region ICommandSource Members

        public BusinessBase ControllerObject
        {
            get { return BusinessObject as BusinessBase; }
        }

        #endregion
    }
}
