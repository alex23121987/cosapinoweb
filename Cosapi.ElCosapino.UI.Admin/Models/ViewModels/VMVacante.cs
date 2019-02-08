using Cosapi.ElCosapino.Dominio.Security.Aggregates.VacanteAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMVacante : VMBase
    {
        public List<Vacante> List_Vacante { get; set; }

        public Vacante Vacante { get; set; }

        public int MessageResultType { get; set; }

        public string MessageResult { get; set; }
    }
}