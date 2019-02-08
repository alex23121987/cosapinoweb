using Cosapi.ElCosapino.Dominio.Base;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg
{
    public class InterfaceWeb:EntidadBase
    {
        public int IDInterfaceWeb { get; set; }

        public int IDModulo { get; set; }

        public int IDSeccion { get; set; }

        public int IDRelacion { get; set; }

        [AllowHtml]
        public string Titulo { get; set; }

        [AllowHtml]
        public string SubTitulo { get; set; }

        public string Imagen { get; set; }

        public int Orden { get; set; }

        public string Estado { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string FechaPublicacion { get; set; }

        public int Destacado { get; set; }

        public string Dia { get; set; }

        public string Mes { get; set; }

        public string FiltroNoticia_Anio { get; set; }

        public string FiltroNoticia_Descripcion { get; set; }

        public string FiltroNoticia_Desde { get; set; }

        public string FiltroNoticia_Hasta { get; set; }
    }
}
