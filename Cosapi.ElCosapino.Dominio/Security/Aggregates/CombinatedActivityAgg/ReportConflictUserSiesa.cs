﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSiesa
    {
        #region 'Properties'
        public int N_ID_CONFLICTUSER_SIESA { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String USUARIO { get; set; }
        public String NOMBRE_USUARIO { get; set; }
        public String ROL { get; set; }
        public String ORGANIZACION { get; set; }
        public String CARGO { get; set; }
        public String ESTADO { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }
        #endregion
        #region 'Constructors'
        public ReportConflictUserSiesa()
        {
        }
        #endregion
    }
}