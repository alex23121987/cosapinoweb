using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.CombinatedActivityAgg
{
    public class ReportConflictUserSISME
    {
        #region Properties
        public int N_ID_CONFLICTUSER { get; set; }
        public int N_ID_COMPANY { get; set; }
        public int N_ID_PROCESS { get; set; }
        public int N_ID_COMBINATED_ACTIVITY { get; set; }
        public DateTime D_DATE_PROCESS { get; set; }
        public String S_DATE_PROCESS { get; set; }

        public String USU_HABILITADO2 { get; set; }
        public String USU_DESUSU { get; set; }
        public String PERFILDESCRIPCION { get; set; }
        public String USUARIOCREACION { get; set; }
        public DateTime FECHACREACION { get; set; }
        public String S_FECHACREACION { get; set; }
        public String USUARIOEDICION { get; set; }
        public DateTime FECHAEDICION { get; set; }
        public String S_FECHAEDICION { get; set; }


        public String USU_PUESTO { get; set; }
        public String USU_TELEFONO { get; set; }
        public String USU_CORREO { get; set; }
        public String USU_COMPANIA { get; set; }
        public String USU_ALIAS { get; set; }
        public DateTime FECHASESION { get; set; }
        public String S_FECHASESION { get; set; }
        public String S_DOC_IDENTITY { get; set; }
        public string ACTIVIDAD { get; set; }

        #endregion

        #region Constructors

        public ReportConflictUserSISME()
        {
        }

        public ReportConflictUserSISME(
                 int n_ID_CONFLICTUSER,
                 int n_ID_COMPANY,
                 int n_ID_PROCESS,
                 int n_ID_COMBINATED_ACTIVITY,
                 DateTime d_DATE_PROCESS,
                 String s_DATE_PROCESS,
                 String s_USU_HABILITADO2,
                 String s_USU_DESUSU,
                 String s_PERFILDESCRIPCION,
                 String s_USUARIOCREACION,
                 DateTime d_FECHACREACION,
                 String s_FECHACREACION,
                 String s_USUARIOEDICION,
                 DateTime d_FECHAEDICION,
                 String s_FECHAEDICION,
                 String s_USU_PUESTO,
                 String s_USU_TELEFONO,
                 String s_USU_CORREO,
                 String s_USU_COMPANIA,
                 String s_USU_ALIAS,
                 DateTime d_FECHASESION,
                 String s_FECHASESION,
                 String s_DOC_IDENTITY
            )
        {
            N_ID_CONFLICTUSER = n_ID_CONFLICTUSER;
            N_ID_COMPANY = n_ID_COMPANY;
            N_ID_PROCESS = n_ID_PROCESS;
            N_ID_COMBINATED_ACTIVITY = n_ID_COMBINATED_ACTIVITY;
            D_DATE_PROCESS = d_DATE_PROCESS;
            S_DATE_PROCESS = s_DATE_PROCESS;
            USU_HABILITADO2 = s_USU_HABILITADO2;
            USU_DESUSU = s_USU_DESUSU;
            PERFILDESCRIPCION = s_PERFILDESCRIPCION;
            USUARIOCREACION = s_USUARIOCREACION;
            FECHACREACION = d_FECHACREACION;
            S_FECHACREACION = s_FECHACREACION;
            USUARIOEDICION = s_USUARIOEDICION;
            FECHAEDICION = d_FECHAEDICION;
            S_FECHAEDICION = s_FECHAEDICION;
            USU_PUESTO = s_USU_PUESTO;
            USU_TELEFONO = s_USU_TELEFONO;
            USU_CORREO = s_USU_CORREO;
            USU_COMPANIA = s_USU_COMPANIA;
            USU_ALIAS = s_USU_ALIAS;
            FECHASESION = d_FECHASESION;
            S_FECHASESION = s_FECHASESION;
            S_DOC_IDENTITY = s_DOC_IDENTITY;
        }

        #endregion

    }
}
