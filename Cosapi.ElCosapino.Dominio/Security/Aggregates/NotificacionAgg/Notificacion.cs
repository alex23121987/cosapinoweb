using Cosapi.ElCosapino.Dominio.Base;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.NotificacionAgg
{
    public class Notificacion:EntidadBase
    {
        public int IDNotificacion { get; set; }

        public string Titulo { get; set; }

        public string Subtitulo { get; set; }

        public string Estado { get; set; }

        public string Fecha { get; set; }

        public int EnviarCelular { get; set; }

        public int EnviarEmail { get; set; }
    }
}
