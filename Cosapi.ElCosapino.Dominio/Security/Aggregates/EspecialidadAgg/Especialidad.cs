using Cosapi.ElCosapino.Dominio.Base;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg
{
    public class Especialidad : EntidadBase
    {
        public int IDEspecialidad { get; set; }

        public string Descripcion { get; set; }      

        public int IDCategoria { get; set; }

        public string Categoria { get; set; }

        public string Estado { get; set; }
    }
}
