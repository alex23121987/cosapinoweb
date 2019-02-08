using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserOracle
    {
        #region Properties
        public int N_ID_CONFLICTUSER_ORACLE { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String S_USER_NAME { get; set; }
        public String S_RESPONSIBILITY_NAME { get; set; }
        public String S_DESCRIPTION { get; set; }
        public String S_OPCION { get; set; }
        public String S_TI { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion

        #region Constructors

        public ReportConflictUserOracle()
        {
        }

        #endregion
    }
}
