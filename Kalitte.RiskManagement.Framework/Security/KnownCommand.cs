using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Security
{
    [Serializable]
    public enum KnownCommand
    {
        None,
        EditInEditor,
        CreateInEditor,
        DeleteEntity,
        ViewInEditor,
        ExportData,
        CreateEntity,
        UpdateEntity,
        ViewInLister,
        StartItem,
        StopItem,
        ViewReport
                
    }


}
