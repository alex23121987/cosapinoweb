using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PerfilAgg
{
    public class PerfilBE
    {
        public int PerfilId { get; set; }
        public String Requisitos { get; set; }
        public int EspecialidadId { get; set; }
        public int CategoriaId { get; set; }
        public String NivelAcademico { get; set; }
        public String PostulaEn { get; set; }
        public String Especialidad { get; set; }
        public String Categoria { get; set; }
    }
}
