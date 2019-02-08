using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.UI.Admin.Models.ViewModels
{
    public class VMUsuario : VMBase
    {
        public List<Usuario> List_Usuario { get; set; }

        public Usuario Usuario { get; set; }

        public Administrador Administrador { get; set; }

        public int MessageResultType { get; set; }

        public string MessageResult { get; set; }
    }
}