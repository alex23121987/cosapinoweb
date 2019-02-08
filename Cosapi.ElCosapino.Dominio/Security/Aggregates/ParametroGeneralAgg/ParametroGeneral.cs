using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg
{
    public class ParametroGeneral
    {
        #region Propiedades
        public int N_IDParametroGeneral { get; set; }

        public int N_IDUnidad { get; set; }

        public String S_IDTabla { get; set; }

        public String S_IDParametro { get; set; }

        public String S_Descripcion { get; set; }

        public String S_Tipo { get; set; }

        public int N_Orden { get; set; }

        public String S_Estado { get; set; }

        public String S_UsuarioCreacion { get; set; }

        public DateTime? D_FechaCreacion { get; set; }

        public String S_UsuarioModificacion { get; set; }

        public DateTime? D_FechaModificacion { get; set; }
        #endregion 

        #region Constructor
        public ParametroGeneral()
        {
        }        
        #endregion
    }
}
