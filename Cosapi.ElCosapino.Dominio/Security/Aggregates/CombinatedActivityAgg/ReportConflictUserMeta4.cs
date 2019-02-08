using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserMeta4
    {
        #region 'Properties'
        public int N_ID_CONFLICTUSER_META4 { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String ID_APP_USER { get; set; }
        public String NOMBRE_USUARIO { get; set; }
        public String ID_APP_ROLE { get; set; }
        public String ID_ORGANIZATION_US { get; set; }
        public String ESTADO { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion
        #region 'Constructors'
        public ReportConflictUserMeta4()
        {
        }
        #endregion
    }
}
