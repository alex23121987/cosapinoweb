using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.RolAgg
{
    public class Rol
    {
        #region Propiedades
        public int N_IDRol { get; set; }
        public String S_IDRol { get; set; }        
        public String S_Descripcion { get; set; }
        public String S_Estado { get; set; }
        public String S_UsuarioCreacion { get; set; }
        public DateTime? D_FechaCreacion { get; set; }
        public String S_UsuarioModificacion { get; set; }
        public DateTime? D_FechaModificacion { get; set; }
        public String S_Accion { get; set; }
        #endregion 

        #region Constructor
        public Rol()
        {
        }    
        #endregion
    }
}
