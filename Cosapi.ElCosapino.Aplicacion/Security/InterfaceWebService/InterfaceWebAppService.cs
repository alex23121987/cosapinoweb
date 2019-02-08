using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.InterfaceWebService
{
    public class InterfaceWebAppService : IInterfaceWebAppService
    {
        readonly IInterfaceWebRepository _InterfaceWebRepository;

        public InterfaceWebAppService()
        {
            _InterfaceWebRepository = new InterfaceWebRepository();
        }

        public List<InterfaceWeb> List_InterfaceWeb_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWeb_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebNoticia_Trimestre()
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticia_Trimestre();
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaFiltro_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticiaFiltro_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebNoticia_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticia_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaDestacado_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticiaDestacado_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaUltimos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticiaUltimos_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebOficina_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            return _InterfaceWebRepository.List_InterfaceWebOficina_Paginate(paginateParams, IDModulo, IDSeccion);
        }

        public List<InterfaceWeb> List_InterfaceWebLineaTiempo_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            return _InterfaceWebRepository.List_InterfaceWebLineaTiempo_Paginate(paginateParams, IDModulo, IDSeccion, IDInterface);
        }

        public List<InterfaceWeb> List_InterfaceWebLineaTiempoProyecto_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            return _InterfaceWebRepository.List_InterfaceWebLineaTiempoProyecto_Paginate(paginateParams, IDModulo, IDSeccion, IDInterface);
        }

        public List<InterfaceWeb> List_InterfaceWebProgramasConFotos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            return _InterfaceWebRepository.List_InterfaceWebProgramasConFotos_Paginate(paginateParams, IDModulo, IDSeccion, IDInterface);
        }

        public List<InterfaceWeb> List_InterfaceWebProgramasFotosGaleria_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            return _InterfaceWebRepository.List_InterfaceWebProgramasFotosGaleria_Paginate(paginateParams, IDModulo, IDSeccion, IDInterface);
        }

        public InterfaceWeb List_InterfaceWeb_Unique(InterfaceWeb item)
        {
            return _InterfaceWebRepository.List_InterfaceWeb_Unique(item);
        }

        public InterfaceWeb List_InterfaceWebNoticia_Unique(InterfaceWeb item)
        {
            return _InterfaceWebRepository.List_InterfaceWebNoticia_Unique(item);
        }

        public InterfaceWeb Update(InterfaceWeb item)
        {
            return _InterfaceWebRepository.Update(item);
        }

        public InterfaceWeb UpdateNoticia(InterfaceWeb item)
        {
            return _InterfaceWebRepository.UpdateNoticia(item);
        }
        
        public InterfaceWeb UpdateDireccion(InterfaceWeb item)
        {
            return _InterfaceWebRepository.UpdateDireccion(item);
        }
        public InterfaceWeb Insert(InterfaceWeb item)
        {
            return _InterfaceWebRepository.Insert(item);
        }

        public InterfaceWeb InsertOficina(InterfaceWeb item)
        {
            return _InterfaceWebRepository.InsertOficina(item);
        }

        public InterfaceWeb InsertNoticia(InterfaceWeb item)
        {
            return _InterfaceWebRepository.InsertNoticia(item);
        }

        public InterfaceWeb InsertUnique(InterfaceWeb item)
        {
            return _InterfaceWebRepository.InsertUnique(item);
        }

        public InterfaceWeb Delete(InterfaceWeb item)
        {
            return _InterfaceWebRepository.Delete(item);
        }

        public InterfaceWeb DeleteNoticia(InterfaceWeb item)
        {
            return _InterfaceWebRepository.DeleteNoticia(item);
        }

        public InterfaceWeb DeleteDireccion(InterfaceWeb item)
        {
            return _InterfaceWebRepository.DeleteDireccion(item);
        }
    }
}
