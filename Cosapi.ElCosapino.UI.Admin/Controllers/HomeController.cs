using Cosapi.ElCosapino.Aplicacion.Security.CategoriaService;
using Cosapi.ElCosapino.Aplicacion.Security.EspecialidadService;
using Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService;
using Cosapi.ElCosapino.Aplicacion.Security.PostulacionService;
using Cosapi.ElCosapino.Aplicacion.Security.UsuarioService;
using Cosapi.ElCosapino.Aplicacion.Security.VacanteService;
using Cosapi.ElCosapino.CrossCutting.Util;
using Cosapi.ElCosapino.Dominio;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Controllers
{
    public class HomeController : BaseController
    {
        readonly IInterfaceWebAppService _InterfaceWebService = new InterfaceWebAppService();
        readonly IUsuarioAppService _UsuarioService = new UsuarioAppService();
        readonly ICategoriaAppService _CategoriaService = new CategoriaAppService();
        readonly IEspecialidadAppService _EspecialidadService = new EspecialidadAppService();
        readonly IVacanteAppService _VacanteService = new VacanteAppService();
        readonly IPostulacionAppService _PostulacionService = new PostulacionAppService();

        private User Usuario = new User();

        public ActionResult Index()
        {
            ViewBag.Message = "Cosapi";
            var paginateParams = new PaginateParams { IsPaginate = false, PageIndex = 0, RowsPerPage = 0, SortColumn = "", SortDirection = "", Activo = "A" };

            //Noticias
            int TotalUsuario = _UsuarioService.List_Usuario_Paginate(paginateParams).Count;
            int TotalCategoria = _CategoriaService.List_Categoria_Paginate(paginateParams).Count;
            int TotalEspecialidad = _EspecialidadService.List_Especialidad_Paginate(paginateParams).Count;
            int TotalVacante = _VacanteService.List_Vacante_Paginate(paginateParams).Count;
            int TotalPostulacion = _PostulacionService.List_Postulacion_Paginate(paginateParams).Count;
            int TotalNoticias=_InterfaceWebService.List_InterfaceWebNoticia_Paginate(paginateParams, Constantes.ModulosWeb.Noticias, Constantes.ModulosSeccionWeb.Noticias_Noticias).Count;

            ViewBag.TotalUsuario = TotalUsuario;
            ViewBag.TotalCategoria = TotalCategoria;
            ViewBag.TotalEspecialidad = TotalEspecialidad;
            ViewBag.TotalVacante = TotalVacante;
            ViewBag.TotalPostulacion = TotalPostulacion;
            ViewBag.TotalNoticias = TotalNoticias;
            return View();
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }

        public ActionResult Ingreso()
        {
            return View();
        }
    }
}