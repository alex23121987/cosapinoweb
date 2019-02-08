using Cosapi.ElCosapino.Dominio.Base;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg
{
    public class Log : EntidadBase
    {
        public int IDLog { get; set; }

        public int IDCategoria { get; set; }

        public string Mensaje { get; set; }
    }
}
