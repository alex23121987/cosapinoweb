using System;

namespace Cosapi.ElCosapino.Dominio.Base
{
    public class EntidadBase
    {
        public string UsuarioCreacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
