using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.UI
{
    public interface ISupportsMultiplePostbackTypes
    {
        PostbackType PostbackType { get; set; }
    }
}
