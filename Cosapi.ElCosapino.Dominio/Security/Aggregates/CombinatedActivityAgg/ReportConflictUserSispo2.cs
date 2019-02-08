using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSispo2
    {
        #region 'Properties'
        public int N_ID_CONFLICTUSER_SISPO2 { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String Empresa { get; set; }
        public String Usuario { get; set; }
        public String Nombre_Completo { get; set; }
        public String Puesto { get; set; }
        public String Area { get; set; }
        public String Perfil { get; set; }
        public String Tipo { get; set; }
        public String Ultimo_Acceso { get; set; }
        public String Estado { get; set; }
        public String Servidor { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion
        #region 'Constructors'
        public ReportConflictUserSispo2()
        {
        }
        #endregion
    }
}
