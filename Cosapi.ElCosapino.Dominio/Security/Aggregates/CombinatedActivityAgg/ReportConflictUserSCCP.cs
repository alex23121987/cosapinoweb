using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSCCP
    {
        #region Properties
        public int N_ID_CONFLICTUSER_SCCP { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String S_USUARIO { get; set; }
        public String S_NOMBRE { get; set; }
        public DateTime? D_EGRESADO { get; set; }
        public String S_ADMINISTRATIVA { get; set; }
        public String S_PARAISO { get; set; }
        public String S_SERPENTIN { get; set; }
        public String S_VARIANTE { get; set; }

        public string ACTIVIDAD { get; set; }
        #endregion

        #region Constructors

        public ReportConflictUserSCCP()
        {
        }

        #endregion
    }
}
