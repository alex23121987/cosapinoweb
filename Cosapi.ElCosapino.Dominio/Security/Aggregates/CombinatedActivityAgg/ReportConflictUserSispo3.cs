using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSispo3
    {
        #region 'Properties'
        public int N_ID_CONFLICTUSER_SISPO3 { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String Nombres_Apellidos { get; set; }
        public String Cargo { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion
        #region 'Constructors'
        public ReportConflictUserSispo3()
        {
        }
        #endregion
    }
}
