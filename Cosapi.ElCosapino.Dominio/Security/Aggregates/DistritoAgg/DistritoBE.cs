using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg
{
    public class DistritoBE
    {
        public int DistritoId { get; set; }
        public int ProvinciaId { get; set; }
        public String Descripcion { get; set; }
        public String UbigeoId { get; set; }
    }
}
