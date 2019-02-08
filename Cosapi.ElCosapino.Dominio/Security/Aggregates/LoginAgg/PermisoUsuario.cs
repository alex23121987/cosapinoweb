using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg
{
    public class PermisoUsuario
    {
        #region Propiedades        
        public int N_IDPermiso { get; set; }
        public String S_IDGrupo { get; set; }
        public String S_GrupoDescripcion { get; set; }
        public String S_IDPagina { get; set; }
        public String S_PaginaDescripcion { get; set; }
        public String S_IDAccion { get; set; }
        public String S_AccionDescripcion { get; set; }        
        #endregion

        #region Constructor
        public PermisoUsuario()
        {
        }
        #endregion
    }
}
