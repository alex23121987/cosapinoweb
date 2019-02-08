using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.NotificacionService
{
    public interface INotificacionAppService
    {
        List<Usuario> List_Notificacion_Paginate(PaginateParams paginateParams, Usuario _Usuario);

        List<Notificacion> List_HistorialNotificacion_Paginate(PaginateParams paginateParams);

        DataTable DT_Notificacion();

        Notificacion Insert(Notificacion item);
    }
}
