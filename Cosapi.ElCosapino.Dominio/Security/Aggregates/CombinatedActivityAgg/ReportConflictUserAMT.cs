using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserAMT
    {
        #region Properties
        public int N_ID_CONFLICTUSER { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public bool TERMINATED { get; set; }
        public String LOGIN_NAME { get; set; }
        public String REAL_NAME { get; set; }
        public String GROUP { get; set; }
        public String EMAIL_ADDRESS { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public String S_TERMINATED { get; set; }
        public String S_DATE_PROCESS { get; set; }
        public string ACTIVIDAD { get; set; }

        #endregion

        #region Constructors

        public ReportConflictUserAMT()
        {
        }

        public ReportConflictUserAMT(
                 int n_ID_CONFLICTUSER,
                 int n_ID_COMPANY,
                 int n_ID_PROCESS,
                 int n_ID_COMBINATED_ACTIVITY,
                 DateTime d_DATE_PROCESS,
                 bool b_TERMINATED,
                 String s_LOGIN_NAME,
                 String s_REAL_NAME,
                 String s_GROUP,
                 String s_EMAIL_ADDRESS,
                 String s_DOC_IDENTITY,
                 String s_TERMINATED,
                 String s_DATE_PROCES
            )
        {
            N_ID_CONFLICTUSER = n_ID_CONFLICTUSER;
            N_ID_COMPANY = n_ID_COMPANY;
            N_ID_PROCESS = n_ID_PROCESS;
            N_ID_COMBINATED_ACTIVITY = n_ID_COMBINATED_ACTIVITY;
            D_DATE_PROCESS = d_DATE_PROCESS;
            TERMINATED = b_TERMINATED;
            LOGIN_NAME = s_LOGIN_NAME;
            REAL_NAME = s_REAL_NAME;
            GROUP = s_GROUP;
            EMAIL_ADDRESS = s_EMAIL_ADDRESS;
            S_DOC_IDENTITY = s_DOC_IDENTITY;
            S_TERMINATED = s_TERMINATED;
            //  S_DATE_PROCESS = s_DATE_PROCESS;
            // S_DESCRIPTION2 = s_DESCRIPTION2;
        }

        #endregion

    }
}
