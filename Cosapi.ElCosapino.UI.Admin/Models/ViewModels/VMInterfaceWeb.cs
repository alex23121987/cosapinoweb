using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using System.Collections.Generic;
using System.Web;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMInterfaceWeb : VMBase
    {
        public List<InterfaceWeb> List_InterfaceWeb { get; set; }

        public InterfaceWeb InterfaceWeb { get; set; }

        public string ParamTitulo { get; set; }

        public string ParamSubtitulo { get; set; }

        public HttpPostedFileBase ParamImageFile { get; set; }
    }
}