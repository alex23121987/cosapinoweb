using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.InboxAgg
{
    public class Inbox
    {
        #region Propiedades
        public int N_IDMensaje { get; set; }

        public int N_TotalA { get; set; }

        public int N_TotalN { get; set; }

        public int N_TotalC { get; set; }

        public String S_IDTipo { get; set; }

        public String S_IDEstado{ get; set; }

        public String S_Asunto { get; set; }

        public String S_Cuerpo { get; set; }

        public String S_FechaCreacion { get; set; }
        #endregion

        #region Constructor
        public Inbox()
        {
        }
        #endregion
    }
}
