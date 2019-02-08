using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.NotificacionService
{
    public class NotificacionAppService : INotificacionAppService
    {
        readonly INotificacionRepository _NotificacionRepository;

        public NotificacionAppService()
        {
            _NotificacionRepository = new NotificacionRepository();
        }

        public List<Usuario> List_Notificacion_Paginate(PaginateParams paginateParams, Usuario _Usuario)
        {
            return _NotificacionRepository.List_Notificacion_Paginate(paginateParams, _Usuario);
        }

        public List<Notificacion> List_HistorialNotificacion_Paginate(PaginateParams paginateParams)
        {
            return _NotificacionRepository.List_HistorialNotificacion_Paginate(paginateParams);
        }

        public DataTable DT_Notificacion()
        {
            return _NotificacionRepository.DT_Notificacion();
        }

        public Notificacion Insert(Notificacion item)
        {
            return _NotificacionRepository.Insert(item);
        }
    }
}
