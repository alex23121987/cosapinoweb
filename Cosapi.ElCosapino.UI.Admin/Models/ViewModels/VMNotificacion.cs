using Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMNotificacion
    {
        public Usuario Usuario { get; set; }

        public Notificacion Notificacion { get; set; }
    }
}