using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DepartamentoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ProvinciaAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Web.Models
{
    public class PageViewModel
    {
        [AllowHtml]
        public string CosapiBreve_SeccionSuperior { get; set; }

        [AllowHtml]
        public string ProyectosEmblematicos_SeccionMedio { get; set; }

        [AllowHtml]
        public string CentroCapacitacion_SeccionSuperior { get; set; }
        
        public List<InterfaceWeb> Noticias_Trimestre { get; set; }

        public List<InterfaceWeb> CosapiBreve_SeccionLogo { get; set; }

        public List<InterfaceWeb> CosapiBreve_SeccionInferior { get; set; }

        public List<InterfaceWeb> ProyectosEmblematicos_SeccionImagenesSuperiores { get; set; }

        public List<InterfaceWeb> ProyectosEmblematicos_SeccionProyectosEmblematicos { get; set; }

        public List<InterfaceWeb> ProyectosEmblematicos_SeccionLineaTiempo { get; set; }

        public List<InterfaceWeb> ProyectosEmblematicos_SeccionLineaTiempoProyecto { get; set; }

        public List<InterfaceWeb> CentroCapacitacion_SeccionImagenesSuperiores { get; set; }

        public List<InterfaceWeb> CentroCapacitacion_SeccionPrograma { get; set; }

        public List<InterfaceWeb> CentroCapacitacion_SeccionGaleria { get; set; }

        public List<InterfaceWeb> CentroCapacitacion_SeccionGaleriaConFotos { get; set; }

        public List<InterfaceWeb> CentroCapacitacion_SeccionFotos { get; set; }

        public List<InterfaceWeb> RequisitosDeIngreso_SeccionImagen { get; set; }

        public List<InterfaceWeb> RequisitosDeIngreso_SeccionRequisitos { get; set; }

        public List<InterfaceWeb> Noticias_SeccionNoticias { get; set; }

        public List<InterfaceWeb> Noticias_SeccionNoticiasDestacado { get; set; }

        public List<InterfaceWeb> Noticias_SeccionNoticiasUltimos { get; set; }

        public InterfaceWeb Noticias_SeccionDetalle { get; set; }

        public List<InterfaceWeb> Contacto_SeccionContacto { get; set; }

        public List<InterfaceWeb> Contacto_SeccionDireccion { get; set; }

        public List<Vacante> TrabajaConNosotros_SeccionVacante { get; set; }

        public Usuario Usuario { get; set; }

        public List<CategoriaBE> Perfil_Categoria { get; set; }

        public List<EspecialidadBE> Perfil_Especialidad { get; set; }

        public List<DepartamentoBE> Perfil_Departamento { get; set; }

        public List<ProvinciaBE> Perfil_Provincia{ get; set; }

        public List<DistritoBE> Perfil_Distrito { get; set; }

        public List<PostulacionBE> Perfil_Postulacion { get; set; }

        public List<InterfaceWeb> Portada_SeccionImagenesSuperiores { get; set; }

        public List<InterfaceWeb> Footer_SeccionIconos { get; set; }
    }
}