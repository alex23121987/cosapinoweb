using Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{  
    public class VMParametroGeneral : VMBase
    {
        public List<ParametroGeneral> List_ParametroGeneral { get; set; }
        public ParametroGeneral ParametroGeneral { get; set; }
        public int MessageResultType { get; set; }
        public string MessageResult { get; set; }
    }
}