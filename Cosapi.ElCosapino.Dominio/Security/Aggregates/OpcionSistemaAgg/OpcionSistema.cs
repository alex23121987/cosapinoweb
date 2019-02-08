using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.OpcionSistemaAgg
{  
    public class OpcionSistema
    {
        #region Propiedades
        public int N_IDOpcionSistema { get; set; }
        public String S_Etiqueta { get; set; }
        public String S_Grupo { get; set; }
        public String S_Tipo { get; set; }
        public String S_Vinculo { get; set; }
        public int N_Orden { get; set; }
        public String S_Estado { get; set; }
        public String S_UsuarioCreacion { get; set; }
        public DateTime? D_FechaCreacion { get; set; }
        public String S_UsuarioModificacion { get; set; }
        public DateTime? D_FechaModificacion { get; set; }
        #endregion 

        #region Constructor
        public OpcionSistema()
        {
        }
        #endregion
    }
}
