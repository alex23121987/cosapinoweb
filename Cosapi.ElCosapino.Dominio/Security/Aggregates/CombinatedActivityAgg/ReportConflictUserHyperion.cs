using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserHyperion
    {
        #region Properties
        public int N_ID_CONFLICTUSER_HYPERION { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String S_IDENTIFICADO_NACIONAL { get; set; }
        public String S_USUARIO { get; set; }
        public String S_NOMBRE { get; set; }
        public String S_GRUPO_SHARED_SERVICES { get; set; }
        public String S_RESPONSABILIDADES_ASIGNADAS { get; set; }
        public String S_EMPRESA { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion

        #region Constructors

        public ReportConflictUserHyperion()
        {
        }

        #endregion
    }
}
