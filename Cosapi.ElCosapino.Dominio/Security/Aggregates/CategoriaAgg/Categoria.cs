using Cosapi.ElCosapino.Dominio.Base;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg
{
    public class Categoria : EntidadBase
    {
        public int IDCategoria { get; set; }

        public string Descripcion { get; set; }

        public string Color { get; set; }

        public int Orden { get; set; }

        public string Estado { get; set; }
    }
}
