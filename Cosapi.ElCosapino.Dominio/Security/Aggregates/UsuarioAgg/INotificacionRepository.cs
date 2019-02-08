using Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg
{
    public interface INotificacionRepository
    {
        List<Usuario> List_Notificacion_Paginate(PaginateParams paginateParams, Usuario _Usuario);

        List<Notificacion> List_HistorialNotificacion_Paginate(PaginateParams paginateParams);

        DataTable DT_Notificacion();

        Notificacion Insert(Notificacion item);
    }
}
