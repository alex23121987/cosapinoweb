using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CriticalActivityAgg
{
    public class CriticalActivity : GridDatatableFilter
    {
        #region Properties
        public int N_ID_CRITICAL_ACTIVITY { get; set; }
        public int N_ID_MASTER_CRITICAL_ACTIVITY { get; set; }
        public String S_CODE { get; set; }
        public String S_NAME { get; set; }
        public String S_DESCRIPTION { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? D_DATE_INITIATE_ACTIVITY { get; set; }
        public String S_NEW_FLAG { get; set; }
        public String S_ID_CODE_FUNCTION { get; set; }
        public int? N_ID_COMPANY { get; set; }
        public int? N_ID_PROCESS { get; set; }
        public int? N_ID_SUBPROCESS { get; set; }
        public int? N_ID_SYSTEM { get; set; }
        public int? N_ID_MODULE { get; set; }
        public String S_COMPANY_NAME { get; set; }
        public String S_PROCESS_NAME { get; set; }
        public String S_SUBPROCESS_NAME { get; set; }
        public String S_SYSTEM_NAME { get; set; }
        public String S_MODULE_NAME { get; set; }
        public String S_FUNCTION_NAME { get; set; }
        public String S_ACTIVITY_NAME { get; set; }
        public String S_NEW_FLAG_NAME { get; set; }
        public String S_TYPE_FUNCTION { get; set; }
        public String S_TYPE_ACTIVITY { get; set; }
        public String S_STATUS_REGISTER { get; set; }
        public String S_USER_CREATION { get; set; }
        public String S_TERMINAL_CREATION { get; set; }
        public DateTime? D_DATE_CREATION { get; set; }
        public String S_USER_MODIFICATION { get; set; }
        public String S_TERMINAL_MODIFICATION { get; set; }
        public DateTime? D_DATE_MODIFICATION { get; set; }
        public String S_KEY_DETAILS { get; set; }
        public String S_STATUS_APPROVAL { get; set; }
        public String S_STATUS_APPROVAL_NAME { get; set; }
        public DateTime? D_DATE_APPROVAL { get; set; }
        public String S_USER_REQUEST { get; set; }
        public String S_USER_APPROVAL { get; set; }
        public String S_NOTIFICADO { get; set; }
        public String S_NOTIFICADO_NAME { get; set; }
        public String S_OBSERVATION { get; set; }
        public String S_STATUS_ELIMINATION { get; set; }
        public int N_ID_USER_REQUEST { get; set; }
        public int N_ID_USER_APPROVAL { get; set; }
        public String S_TIPO_FILTRO { get; set; }

        public int? ID_USER { get; set; }
        public int? N_ID_ACTIVITY_USER { get; set; }
        public string S_NAME_PROCESS { get; set; }
        public string S_NAME_SUBPROCESS { get; set; }
        public string S_NAME_TYPE_FUNCTION { get; set; }
        public String S_NAME_TYPE_ACTIVITY { get; set; }
        public String S_NAME_COMPANY_CONTROL { get; set; }
        public String S_CODE_CONTROL { get; set; }
        public String S_NAME_CONTROL { get; set; }
        public String S_DESCRIPTION_CONTROL { get; set; }
        public String S_CONTROL_PASSWORD_FLAG_DESCRIPTION { get; set; }
        public String Responsable_Manual { get; set; }
        public String S_DATE_INITIATE_ACTIVITY { get; set; }
        public int CantidadControles { get; set; }

        public int N_ID_PERIOD { get; set; }
        public string S_PERIOD_ORDEN { get; set; }
        public string S_NAME_PERIOD_AUDITED { get; set; }

        public int N_ID_PERIOD_OPEN { get; set; }

        public bool S_REVIEW_JOINT_FLAG { get; set; }

        #region work
        public String S_NAME_WORK { get; set; }
        public int N_ID_MASTER_WORKS { get; set; }

        public string CodigoSessionCriticalActivity { get; set; }
        #endregion

        #endregion

        #region Constructors
        public CriticalActivity()
        {
        }

        public CriticalActivity
            (
            String s_CODE,
            String s_NAME,
            String s_DESCRIPTION,
            DateTime d_DATE_INITIATE_ACTIVITY,
            String s_NEW_FLAG,
            String s_ID_CODE_FUNCTION,
            int n_ID_COMPANY,
            int n_ID_PROCESS,
            int n_ID_SUBPROCESS,
            int n_ID_SYSTEM,
            int n_ID_MODULE,
            String s_TYPE_FUNCTION,
            String s_TYPE_ACTIVITY,
            String s_STATUS_REGISTER,
            String s_USER_CREATION,
            String s_TERMINAL_CREATION,
            DateTime d_DATE_CREATION,
            String s_USER_MODIFICATION,
            String s_TERMINAL_MODIFICATION,
            DateTime d_DATE_MODIFICATION,
            String s_STATUS_APPROVAL
            )
        {
            S_CODE = s_CODE;
            S_NAME = s_NAME;
            S_DESCRIPTION = s_DESCRIPTION;
            D_DATE_INITIATE_ACTIVITY = d_DATE_INITIATE_ACTIVITY;
            S_NEW_FLAG = s_NEW_FLAG;
            S_ID_CODE_FUNCTION = s_ID_CODE_FUNCTION;
            N_ID_COMPANY = n_ID_COMPANY;
            N_ID_PROCESS = n_ID_PROCESS;
            N_ID_SUBPROCESS = n_ID_SUBPROCESS;
            N_ID_SYSTEM = n_ID_SYSTEM;
            N_ID_MODULE = n_ID_MODULE;
            S_TYPE_FUNCTION = s_TYPE_FUNCTION;
            S_TYPE_ACTIVITY = s_TYPE_ACTIVITY;
            S_STATUS_REGISTER = s_STATUS_REGISTER;
            S_USER_CREATION = s_USER_CREATION;
            S_TERMINAL_CREATION = s_TERMINAL_CREATION;
            D_DATE_CREATION = d_DATE_CREATION;
            S_USER_MODIFICATION = s_USER_MODIFICATION;
            S_TERMINAL_MODIFICATION = s_TERMINAL_MODIFICATION;
            D_DATE_MODIFICATION = d_DATE_MODIFICATION;
            S_STATUS_APPROVAL = s_STATUS_APPROVAL;
        }
        #endregion
    }
}
