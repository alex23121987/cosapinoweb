using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMCategoria : VMBase
    {
        public List<Categoria> List_Categoria { get; set; }

        public Categoria Categoria { get; set; }

        public int MessageResultType { get; set; }

        public string MessageResult { get; set; }
    }
}