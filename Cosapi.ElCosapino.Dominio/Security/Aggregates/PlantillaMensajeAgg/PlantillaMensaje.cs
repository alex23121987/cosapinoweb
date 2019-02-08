using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PlantillaMensajeAgg
{
    public class PlantillaMensaje
    {
        #region Propiedades
        public int N_IDPlantillaMensaje { get; set; }
        public String S_IDTipo { get; set; }
        public String S_Descripcion { get; set; }
        public String S_Estado { get; set; }
        public String S_UsuarioCreacion { get; set; }
        public DateTime? D_FechaCreacion { get; set; }
        public String S_UsuarioModificacion { get; set; }
        public DateTime? D_FechaModificacion { get; set; }
        public String S_Accion { get; set; }
        #endregion 

        #region Constructor
        public PlantillaMensaje()
        {
        }
        #endregion
    }
}
