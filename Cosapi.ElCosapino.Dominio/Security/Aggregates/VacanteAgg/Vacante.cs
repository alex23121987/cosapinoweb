using Cosapi.ElCosapino.Dominio.Base;
using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg
{
    public class Vacante : EntidadBase
    {
        public int IDVacante { get; set; }

        public string Requisitos { get; set; }

        public string NivelAcademico { get; set; }

        public string PostulaEn { get; set; }

        public int IDEspecialidad { get; set; }

        public string Especialidad { get; set; }

        public int IDCategoria { get; set; }

        public string Categoria { get; set; }

        public string Estado { get; set; }

        public string FechaRegistro { get; set; }
    }
}
