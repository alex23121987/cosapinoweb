using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg
{
    public interface IInterfaceWebRepository
    {
        List<InterfaceWeb> List_InterfaceWeb_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        List<InterfaceWeb> List_InterfaceWebNoticia_Trimestre();

        List<InterfaceWeb> List_InterfaceWebNoticiaFiltro_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        List<InterfaceWeb> List_InterfaceWebNoticia_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        List<InterfaceWeb> List_InterfaceWebNoticiaDestacado_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        List<InterfaceWeb> List_InterfaceWebNoticiaUltimos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        List<InterfaceWeb> List_InterfaceWebLineaTiempo_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface);

        List<InterfaceWeb> List_InterfaceWebLineaTiempoProyecto_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface);

        List<InterfaceWeb> List_InterfaceWebProgramasConFotos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface);

        List<InterfaceWeb> List_InterfaceWebOficina_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion);

        InterfaceWeb List_InterfaceWeb_Unique(InterfaceWeb item);

        InterfaceWeb List_InterfaceWebNoticia_Unique(InterfaceWeb item);

        InterfaceWeb Update(InterfaceWeb item);

        InterfaceWeb UpdateNoticia(InterfaceWeb item);

        InterfaceWeb UpdateDireccion(InterfaceWeb item);

        InterfaceWeb Insert(InterfaceWeb item);

        InterfaceWeb InsertOficina(InterfaceWeb item);

        InterfaceWeb InsertUnique(InterfaceWeb item);

        InterfaceWeb InsertNoticia(InterfaceWeb item);

        InterfaceWeb Delete(InterfaceWeb item);

        InterfaceWeb DeleteNoticia(InterfaceWeb item);

        InterfaceWeb DeleteDireccion(InterfaceWeb item);

        List<InterfaceWeb> List_InterfaceWebProgramasFotosGaleria_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface);
    }
}
