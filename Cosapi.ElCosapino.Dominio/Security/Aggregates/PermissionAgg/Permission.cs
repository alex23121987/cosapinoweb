using System;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PermissionAgg
{
    public class Permission : Entity
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Permission class.
        /// </summary>
        public Permission()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Permission class.
        /// </summary>
        public Permission(int n_ID_ROLE, int n_ID_ACTION, int n_ID_OPTION, string s_LEVEL_NAME_PERMISSION, string s_NAME, string s_TYPE, string s_STATUS, string s_STATUS_REGISTER, string s_USER_CREATION, DateTime d_DATE_CREATION, string s_TERMINAL_CREATION, string s_USER_MODIFICATION, DateTime d_DATE_MODIFICATION, string tERMINAL_MODIFICATION,string s_IDRol)
        {
            N_ID_ROLE = n_ID_ROLE;
            N_ID_ACTION = n_ID_ACTION;
            N_ID_OPTION = n_ID_OPTION;
            S_LEVEL_NAME_PERMISSION = s_LEVEL_NAME_PERMISSION;
            S_NAME = s_NAME;
            S_TYPE = s_TYPE;
            S_STATUS = s_STATUS;
            S_STATUS_REGISTER = s_STATUS_REGISTER;
            S_USER_CREATION = s_USER_CREATION;
            D_DATE_CREATION = d_DATE_CREATION;
            S_TERMINAL_CREATION = s_TERMINAL_CREATION;
            S_USER_MODIFICATION = s_USER_MODIFICATION;
            D_DATE_MODIFICATION = d_DATE_MODIFICATION;
            S_TERMINAL_MODIFICATION = tERMINAL_MODIFICATION;
            S_IDRol = s_IDRol;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the N_ID_PERMISSION value.
        /// </summary>
        public int N_ID_PERMISSION { get; set; }
        /// <summary>
		/// Gets or sets the N_ID_ROLE value.
		/// </summary>
		public int N_ID_ROLE { get; set; }

        /// <summary>
        /// Gets or sets the N_ID_ACTION value.
        /// </summary>
        public int N_ID_ACTION { get; set; }

        /// <summary>
        /// Gets or sets the N_ID_OPTION value.
        /// </summary>
        public int N_ID_OPTION { get; set; }

        /// <summary>
        /// Gets or sets the S_LEVEL_NAME_PERMISSION value.
        /// </summary>
        public string S_LEVEL_NAME_PERMISSION { get; set; }
      
        public string S_NAME { get; set; }

        public string S_IDRol { get; set; }

        public string S_TYPE { get; set; }

        public string S_STATUS { get; set; }

        public string S_STATUS_REGISTER { get; set; }
   
        public string S_USER_CREATION { get; set; }

        public DateTime D_DATE_CREATION { get; set; }

        public string S_TERMINAL_CREATION { get; set; }

        public string S_USER_MODIFICATION { get; set; }

        public DateTime? D_DATE_MODIFICATION { get; set; }

        public string S_TERMINAL_MODIFICATION { get; set; }

        public string S_OBSERVATION { get; set; }

        public string S_NAME_ACCION { get; set; }

        #endregion

        public class ListPermission
        {
            #region Constructors
            public ListPermission()
            {
            }
            public ListPermission(
                int n_ID_PERMISSION,
                int n_ID_ROLE,
                string s_ROL,
                int n_ID_MODULE,
                string s_MODULe,
                int n_ID_OPTION,
                string s_OPTION,
                int n_ID_ACTION,
                string s_ACTION,
                int n_ID_SYSTEM,
                string s_SYSTEM
                )
            {
                N_ID_PERMISSION = n_ID_PERMISSION;
                N_ID_ROLE = N_ID_ROLE;
                S_ROL = s_ROL;
                N_ID_MODULE = n_ID_MODULE;
                S_MODULE = s_MODULe;
                N_ID_OPTION = n_ID_OPTION;
                S_OPTION = s_OPTION;
                N_ID_ACTION = n_ID_ACTION;
                S_ACTION = s_ACTION;
                N_ID_SYSTEM = n_ID_SYSTEM;
                S_SYSTEM = s_SYSTEM;
            }

            #endregion

            #region Properties
            public int N_ID_PERMISSION { get; set; }
            public int N_ID_ROLE { get; set; }
            public string S_ROL { get; set; }
            public int N_ID_MODULE { get; set; }
            public string S_MODULE { get; set; }
            public int N_ID_OPTION { get; set; }
            public string S_OPTION { get; set; }
            public int N_ID_ACTION { get; set; }
            public string S_ACTION { get; set; }
            public int N_ID_SYSTEM { get; set; }
            public string S_SYSTEM { get; set; }
            public int N_ID_MODULO_PADRE { get; set; }
            #endregion
        }

        public class ListPermissionTree
        {
            #region Constructors
            public ListPermissionTree()
            {
            }
            public ListPermissionTree(
                int n_ID_PERMISSION,
                string s_ID_RESULT,
                int n_ID_REFERENCE_RESULT,
                string s_TIPO_RESULT,
                string s_NAME_RESULT,
                string s_ID_PARENT_RESULT,
                int n_ID_ACTION,
                string s_ACTION,
                string s_STATUS

                )
            {
                N_ID_PERMISSION = n_ID_PERMISSION;
                S_ID_RESULT = s_ID_RESULT;
                N_ID_REFERENCE_RESULT = n_ID_REFERENCE_RESULT;
                S_TIPO_RESULT = s_TIPO_RESULT;
                S_NAME_RESULT = s_NAME_RESULT;
                S_ID_PARENT_RESULT = s_ID_PARENT_RESULT;
                N_ID_ACTION = n_ID_ACTION;
                S_ACTION = s_ACTION;
                S_STATUS = s_STATUS;
            }

            #endregion

            #region Properties
            public int N_ID_PERMISSION { get; set; }
            public string S_ID_RESULT { get; set; }
            public int N_ID_REFERENCE_RESULT { get; set; }
            public string S_TIPO_RESULT { get; set; }
            public string S_NAME_RESULT { get; set; }
            public string S_ID_PARENT_RESULT { get; set; }
            public int N_ID_ACTION { get; set; }
            public string S_ACTION { get; set; }
            public string S_STATUS { get; set; }
            public List<ListPermissionTree> Hijos { get; set; }
            public string Data { get; set; }
            public string S_IDS { get; set; }
            #endregion            
        }
    }
}
