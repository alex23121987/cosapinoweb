using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMEspecialidad : VMBase
    {
        public List<Especialidad> List_Especialidad { get; set; }

        public Especialidad Especialidad { get; set; }

        public int MessageResultType { get; set; }

        public string MessageResult { get; set; }
    }
}