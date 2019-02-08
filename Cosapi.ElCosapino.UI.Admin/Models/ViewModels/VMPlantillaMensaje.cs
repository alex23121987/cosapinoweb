using Cosapi.ElCosapino.Dominio.Security.Aggregates.PlantillaMensajeAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMPlantillaMensaje : VMBase
    {
        public List<PlantillaMensaje> List_PlantillaMensaje { get; set; }
        public PlantillaMensaje PlantillaMensaje { get; set; }
        public int MessageResultType { get; set; }
        public string MessageResult { get; set; }
    }
}