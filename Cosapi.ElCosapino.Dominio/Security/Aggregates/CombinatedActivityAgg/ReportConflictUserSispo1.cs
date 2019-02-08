using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSispo1
    {
        #region 'Properties'
        public int N_ID_CONFLICTUSER_SISPO1 { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String USUARIO { get; set; }
        public String Nombres { get; set; }
        public String Departamento { get; set; }
        public String Perfil { get; set; }
        public String Servidor { get; set; }
        public String Modulo { get; set; }
        public String Opcion_Menu { get; set; }
        public String Accion { get; set; }
        public String Ruta { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion
        #region 'Constructors'
        public ReportConflictUserSispo1()
        {
        }
        #endregion
    }
}
