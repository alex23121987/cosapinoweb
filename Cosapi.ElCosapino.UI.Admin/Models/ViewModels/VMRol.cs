using Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg;
using System.Web.Script.Serialization;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMRol : VMBase
    {
        public Rol Rol { get; set; }
           
        public string javaSerial { get; set; }

        public JavaScriptSerializer javaSerializer { get; set; }

        public JsTreeModel model { get; set; }
    }
}