using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.ConsultConflictAgg
{
    public class PossibleConflictUserTransaction
    {
        #region Propiedades

        public int orden { get; set; }
        public int N_ID_POSSIBLECONFLICTUSERTRANSACTION { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public String S_PERIODO_AUDITADO { get; set; }
        public String S_OBRA { get; set; }
        public String USUARIO { get; set; }
        public String NOMBRE_USUARIO { get; set; }
        public string S_CODIGO_ACTIVIDAD_COMBINADA { get; set; }
        public int? JEFE_GERENTE { get; set; }
        public int? GERENTE_DIRECTOR_OBRA { get; set; }
        public int? GERENTE_PROYECTO { get; set; }
        //V.2
        public string S_NAME_OBRA { get; set; }
        public int? PERSONAL_ALMACEN { get; set; }
        public int? ALMACENISTA_JEFE_ALMACEN { get; set; }
        public int? ADMINISTRADOR_OBRA { get; set; }
        public int? ASISTENTE_SUPERVISOR_JEFE_ALMACEN { get; set; }
        public int? REVISOR { get; set; }
        public string USUARIO_REVISOR { get; set; }
        public List<Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.UserSraAgg.UserSRA> LIST_USUARIO_REVISOR { get; set; }
        public string OBSERVACION { get; set; }
        public int? CANTIDAD_TRANSACCIONES { get; set; }
        public int? CANTIDAD_REPORTES_DIAS { get; set; }
        //fin V.2
        public char ESTADO_ANALISIS { get; set; }
        public String S_STATUS_REGISTER { get; set; }
        public String S_USER_CREATION { get; set; }
        public DateTime D_DATE_CREATION { get; set; }
        public String S_TERMINAL_CREATION { get; set; }
        public String S_USER_MODIFICATION { get; set; }
        public DateTime D_DATE_MODIFICATION { get; set; }
        public String S_TERMINAL_MODIFICATION { get; set; }
        public string S_FECHA_INFERIOR { get; set; }
        public string S_FECHA_SUPERIOR { get; set; }
        public String DESCRIPCION_JEFE_GERENTE { get; set; }
        public String DESCRIPCION_GERENTE_DIRECTOR_OBRA { get; set; }
        public String DESCRIPCION_GERENTE_PROYECTO { get; set; }
        public String DESCRIPCION_PERSONAL_ALMACEN { get; set; }
        public String DESCRIPCION_ALMACENISTA_JEFE_ALMACEN { get; set; }
        public String DESCRIPCION_ADMINISTRADOR_OBRA { get; set; }
        public String DESCRIPCION_ASISTENTE_SUPERVISOR_JEFE_ALMACEN { get; set; }
        public String DESCRIPCION_REVISOR { get; set; }
        public string FECHA_CONFLICTO { get; set; }
        public PossibleConflictUserTransaction()
        {
            LIST_USUARIO_REVISOR = new List<UserSraAgg.UserSRA>();
        }
        public int? SE_ENCUENTRA_PLAN_FIRMAS { get; set; }
        public String DESCRIPCION_SE_ENCUENTRA_PLAN_FIRMAS { get; set; }
        public int? EXISTE_JEFE_ALMACEN { get; set; }
        public String DESCRIPCION_EXISTE_JEFE_ALMACEN { get; set; }
        public string S_CODIGO_OBRA { get; set; }
        public string S_NAME_OBRA_ENCRIPTADO { get; set; }

        #endregion Propiedades
    }
}
