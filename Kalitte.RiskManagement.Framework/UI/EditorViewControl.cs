using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Business;

namespace Kalitte.RiskManagement.Framework.UI
{
    public abstract class EditorViewControl<T> : ViewControlBase<T> where T : BusinessBase
    {
        public override ViewControlType ControlType
        {
            get { return ViewControlType.Editor; }
        }


    }
}
