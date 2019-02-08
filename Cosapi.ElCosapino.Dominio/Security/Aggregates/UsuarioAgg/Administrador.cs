using Cosapi.ElCosapino.Dominio.Base;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg
{
    public  class Administrador : EntidadBase
    {
        public int AdministradorId { get; set; }

        public string Codigo { get; set; }

        public string Clave { get; set; }

        public string Rol { get; set; }

        public string Estado { get; set; }

        public int IDRol { get; set; }
    }
}
