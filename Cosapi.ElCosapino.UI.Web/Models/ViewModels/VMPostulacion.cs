using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Web.Models.ViewModels
{
    public class VMPostulacion
    {
        public List<PostulacionBE> List_PostulacionBE { get; set; }

        public PostulacionBE PostulacionBE { get; set; }
      
        public int MessageResultType { get; set; }

        public string MessageResult { get; set; }
    }
}