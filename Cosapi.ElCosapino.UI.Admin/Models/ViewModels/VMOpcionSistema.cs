using Cosapi.ElCosapino.Dominio.Security.Aggregates.OpcionSistemaAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMOpcionSistema : VMBase
    {
        public List<OpcionSistema> List_OpcionSistema { get; set; }
        public OpcionSistema OpcionSistema { get; set; }
        public int MessageResultType { get; set; }
        public string MessageResult { get; set; }
    }
}