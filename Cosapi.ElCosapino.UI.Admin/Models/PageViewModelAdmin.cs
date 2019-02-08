using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Models
{
    public class PageViewModelAdmin
    {
        [AllowHtml]
        public string CosapiBreve_SeccionSuperior { get; set; }

        [AllowHtml]
        public string ProyectosEmblematicos_SeccionMedio { get; set; }

        [AllowHtml]
        public string CentroCapacitacion_SeccionSuperior { get; set; }
    }
}