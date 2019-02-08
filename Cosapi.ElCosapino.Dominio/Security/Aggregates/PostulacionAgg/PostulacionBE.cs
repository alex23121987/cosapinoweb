using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg
{
    public class PostulacionBE
    {
        public int PostulacionId { get; set; }
        public DateTime Fecha { get; set; }
        public int PerfilId { get; set; }
        public int UsuarioId { get; set; }
        public String Requisitos { get; set; }
        public int EspecialidadId { get; set; }
        public String NivelAcademico { get; set; }
        public String PostulaEn { get; set; }
        public String Especialidad { get; set; }
        public String Categoria { get; set; }
        public String ColorCategoria { get; set; }

        public string FechaPostulacion { get; set; }

        public string PostulanteNombre { get; set; }

        public string PostulanteEmail { get; set; }

        public string PostulanteNumeroContacto1 { get; set; }

        public string PostulanteNumeroContacto2 { get; set; }
        public string Estado { get; set; }
    }
}
