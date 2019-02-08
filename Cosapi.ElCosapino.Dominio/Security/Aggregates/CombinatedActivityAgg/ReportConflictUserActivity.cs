using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserActivity
    {
        #region Properties
        public int N_ID_CONFLICTUSER { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String S_USER_NAME1 { get; set; }
        public String S_DESCRIPTION1 { get; set; }
        public String S_BOOK1 { get; set; }
        public String S_PROYECTO1 { get; set; }
        public String S_RESPONSIBILITY_NAME1 { get; set; }
        public String S_OPCION1 { get; set; }
        public String S_TI1 { get; set; }
        public String S_USER_NAME2 { get; set; }
        public String S_DESCRIPTION2 { get; set; }
        public String S_BOOK2 { get; set; }
        public String S_PROYECTO2 { get; set; }
        public String S_RESPONSIBILITY_NAME2 { get; set; }
        public String S_OPCION2 { get; set; }
        public String S_TI2 { get; set; }
        public String S_NUM_DOC1 { get; set; }
        public String S_NUM_DOC2 { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion

        #region Constructors

        public ReportConflictUserActivity()
        {
        }

        public ReportConflictUserActivity(
                 int n_ID_CONFLICTUSER,
                 int n_ID_COMPANY,
                 int n_ID_PROCESS,
                 int n_ID_COMBINATED_ACTIVITY,
                 DateTime d_DATE_PROCESS,
                 String s_UsER_NAME1,
                 String s_DEsCRIPTION1,
                 String s_BOOK1,
                 String s_PROYECTO1,
                 String s_REsPONsIBILITY_NAME1,
                 String s_OPCION1,
                 String s_TI1,
                 String s_UsER_NAME2,
                 String s_DEsCRIPTION2,
                 String s_BOOK2,
                 String s_PROYECTO2,
                 String s_REsPONsIBILITY_NAME2,
                 String s_OPCION2,
                 String s_TI2
            )
        {
            N_ID_CONFLICTUSER = N_ID_CONFLICTUSER;
            N_ID_COMPANY = N_ID_COMPANY;
            N_ID_PROCESS = N_ID_PROCESS;
            N_ID_COMBINATED_ACTIVITY = N_ID_COMBINATED_ACTIVITY;
            D_DATE_PROCESS = D_DATE_PROCESS;
            S_USER_NAME1 = S_USER_NAME1;
            S_DESCRIPTION1 = S_DESCRIPTION1;
            S_BOOK1 = S_BOOK1;
            S_PROYECTO1 = S_PROYECTO1;
            S_RESPONSIBILITY_NAME1 = S_RESPONSIBILITY_NAME1;
            S_OPCION1 = S_OPCION1;
            S_TI1 = S_TI1;
            S_USER_NAME2 = S_USER_NAME2;
            S_DESCRIPTION2 = S_DESCRIPTION2;
            S_BOOK2 = S_BOOK2;
            S_PROYECTO2 = S_PROYECTO2;
            S_RESPONSIBILITY_NAME2 = S_RESPONSIBILITY_NAME2;
            S_OPCION2 = S_OPCION2;
            S_TI2 = S_TI2;
            S_NUM_DOC1 = S_NUM_DOC1;
            S_NUM_DOC2 = S_NUM_DOC2;
        }

        #endregion

    }
}
