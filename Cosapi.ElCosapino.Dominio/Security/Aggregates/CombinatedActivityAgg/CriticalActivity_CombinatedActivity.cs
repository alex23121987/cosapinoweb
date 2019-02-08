using Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.ConsultConflictAgg;
using Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.PurchaseOrderAgg;
using Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.UserSraAgg;
using System;
using System.Collections.Generic;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class CriticalActivity_CombinatedActivity
    {
        #region Properties


        public int ID_ROWS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY_DETAIL { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }

        public int N_ID_CRITICAL_ACTIVITY { get; set; }
        public String S_CODE { get; set; }
        public String S_NAME { get; set; }
        public String S_DESCRIPTION { get; set; }
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
        public bool B_ISNEWBD { get; set; }

        public String S_NRO_CONFLICT_TI { get; set; }
        public String S_NRO_CONFLICT_NOTI { get; set; }
        public String S_ANEXO { get; set; }
        public String S_CONFLICT_TI { get; set; }
        public String S_CONFLICT_NOTI { get; set; }

        public String S_NRO_TRANSACTION { get; set; }
        public String S_NRO_USER_TRANSACTION { get; set; }
        public String S_FLAG_TRANSACTION { get; set; }
        public String S_ANEXO_TRANSACTION { get; set; }
        public String S_MODULE { get; set; }
        public String S_ID_CRITICAL_ACTIVITY { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric2 { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity2 { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric3 { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity3 { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric4 { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity4 { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric5 { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity5 { get; set; }

        public List<CombinatedSectionGeneric> ListCombinatedSectionGeneric6 { get; set; }
        public List<CombinatedSectionActivity> ListCombinatedSectionActivity6 { get; set; }

        public List<PossibleConflictUserTransaction> ListPossibleConflictUserTransaction { get; set; }

        public List<PossibleConflictUserTransaction> ListDaysPossibleConflictUserTransaction { get; set; }
        public List<PurchaseOrder> ListPurchaseOrder { get; set; }
        public List<List<PurchaseOrder>> ListPurchaseOrderProcess { get; set; }

        public List<List<PurchaseOrder>> ListHierarchyPurchaseOrder = new List<List<PurchaseOrder>>();

        public List<List<PurchaseOrder>> ListPurchaseOrderModification { get; set; }

        public List<UserSRA> ListReportUserSRA { get; set; }
        public List<ReportConflictUserActivity> ListReportConflictUserActivity { get; set; }
        public List<ReportConflictUserSISME> ListReportConflictUserSISME { get; set; }
        public List<ReportConflictUserAMT> ListReportConflictUserAMT { get; set; }
        public List<ReportConflictUserOracle> ListReportConflictUserOracle { get; set; }
        public List<ReportConflictUserHyperion> ListReportConflictUserHyperion { get; set; }
        public List<ReportConflictUserMeta4> ListReportConflictUserMeta4 { get; set; }
        public List<ReportConflictUserSiesa> ListReportConflictUserSiesa { get; set; }
        public List<ReportConflictUserPayroll> ListReportConflictUserPayroll { get; set; }
        public List<ReportConflictUserSispo1> ListReportConflictUserSispo1 { get; set; }
        public List<ReportConflictUserSispo2> ListReportConflictUserSispo2 { get; set; }
        public List<ReportConflictUserSispo3> ListReportConflictUserSispo3 { get; set; }
        public List<ReportConflictUserSCCP> ListReportConflictUserSCCP { get; set; }
        #endregion

        #region Constructors
        public CriticalActivity_CombinatedActivity()
        {
            this.ListCombinatedSectionGeneric = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity = new List<CombinatedSectionActivity>();

            this.ListCombinatedSectionGeneric2 = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity2 = new List<CombinatedSectionActivity>();

            this.ListCombinatedSectionGeneric3 = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity3 = new List<CombinatedSectionActivity>();

            this.ListCombinatedSectionGeneric4 = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity4 = new List<CombinatedSectionActivity>();

            this.ListCombinatedSectionGeneric5 = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity5 = new List<CombinatedSectionActivity>();

            this.ListCombinatedSectionGeneric6 = new List<CombinatedSectionGeneric>();
            this.ListCombinatedSectionActivity6 = new List<CombinatedSectionActivity>();

            this.ListPossibleConflictUserTransaction = new List<PossibleConflictUserTransaction>();
            this.ListPurchaseOrder = new List<PurchaseOrder>();

            this.ListDaysPossibleConflictUserTransaction = new List<PossibleConflictUserTransaction>();

            this.ListReportUserSRA = new List<UserSRA>();
            this.ListReportConflictUserActivity = new List<ReportConflictUserActivity>();
            this.ListReportConflictUserSISME = new List<ReportConflictUserSISME>();
            this.ListReportConflictUserAMT = new List<ReportConflictUserAMT>();
            this.ListReportConflictUserOracle = new List<ReportConflictUserOracle>();
            this.ListReportConflictUserHyperion = new List<ReportConflictUserHyperion>();
            this.ListReportConflictUserMeta4 = new List<ReportConflictUserMeta4>();
            this.ListReportConflictUserSiesa = new List<ReportConflictUserSiesa>();
            this.ListReportConflictUserPayroll = new List<ReportConflictUserPayroll>();
            this.ListReportConflictUserSispo1 = new List<ReportConflictUserSispo1>();
            this.ListReportConflictUserSispo2 = new List<ReportConflictUserSispo2>();
            this.ListReportConflictUserSispo3 = new List<ReportConflictUserSispo3>();
            this.ListReportConflictUserSCCP = new List<ReportConflictUserSCCP>();
        }

        public CriticalActivity_CombinatedActivity
            (
            int id_ROWS,
            int n_ID_COMBINATED_ACTIVITY_DETAIL,
            int n_ID_COMBINATED_ACTIVITY,
            int n_ID_CRITICAL_ACTIVITY,
            String s_CODE,
            String s_NAME,
            String s_DESCRIPTION,
            DateTime d_DATE_INITIATE_ACTIVITY,
            String s_MODULE,
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
            bool b_ISNEWBD,
            String s_NRO_CONFLICT_TI,
            String s_NRO_CONFLICT_NOTI,
            String s_ANEXO,
            String s_CONFLICT_TI,
            String s_CONFLICT_NOTI

            )
        {
            ID_ROWS = id_ROWS;
            N_ID_COMBINATED_ACTIVITY_DETAIL = n_ID_COMBINATED_ACTIVITY_DETAIL;
            N_ID_COMBINATED_ACTIVITY = n_ID_COMBINATED_ACTIVITY;
            N_ID_CRITICAL_ACTIVITY = n_ID_CRITICAL_ACTIVITY;
            S_CODE = s_CODE;
            S_NAME = s_NAME;
            S_DESCRIPTION = s_DESCRIPTION;
            D_DATE_INITIATE_ACTIVITY = d_DATE_INITIATE_ACTIVITY;
            S_MODULE = s_MODULE;
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
            B_ISNEWBD = b_ISNEWBD;
            S_NRO_CONFLICT_TI = s_NRO_CONFLICT_TI;
            S_NRO_CONFLICT_NOTI = s_NRO_CONFLICT_NOTI;
            S_ANEXO = s_ANEXO;
            S_CONFLICT_TI = s_CONFLICT_TI;
            S_CONFLICT_NOTI = s_CONFLICT_NOTI;
        }
        #endregion
    }
}
