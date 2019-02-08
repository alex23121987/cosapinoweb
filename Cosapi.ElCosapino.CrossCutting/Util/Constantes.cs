using System;
using System.Collections.Generic;
using System.Configuration;

namespace Cosapi.ElCosapino.CrossCutting.Util
{
    public class Constantes
    {
        public static class Paginacion
        {
            public const string First = "Primero";
            public const string Before = "Anterior";
            public const string Next = "Siguiente";
            public const string Last = "Ultimo";
            public const int RowsByPage_Max = 100;
            public const int RowsByPage_Plus= 20;
            public const int RowsByPage_Normal = 10;
            public const int RowsByPage_Detail = 5;
            public const int DefaultPage = 1;
        }
     
        public static class AppSettingsKey
        {
            public const string ServerSra = "ServerSra";
            public const string ReportingService = "ReportingService";
            public const string RootReporte = "RootReporte";
            public const string UserCredencial = "UserCredencial";
            public const string PasswordCredencial = "PasswordCredencial";
            public const string DominioCredencial = "DominioCredencial";

            #region Mail

            public const string MailSMTP = "Mail.SMTP";
            public const string ServerSMTP = "Servidor.SMTP";
            public const string UserSMTP = "User.SMTP";
            public const string PasswordSMTP = "Password.SMTP";
            public const string PortSMTP = "Port.SMTP";
            public const string EnableSSLSMTP = "EnableSSL.SMTP";
            public const string UserNameSMTP = "UserName.SMTP";

            #endregion Mail

            #region Sharepoint

            public const string SharepointSiteUrl = "Sharepoint.SiteUrl";

            #endregion Sharepoint
        }

        public class ReportExtension
        {
            public const string DOCUMENTO_PDF = ".pdf";
            public const string DOCUMENTO_EXCEL = ".xls";
        }

        public class ReportFormat
        {
            public const string FORMATO_REPORTE_PDF = "pdf";
            public const string FORMATO_REPORTE_EXCEL = "excel";
            public const string FORMATO_REPORTE_EXCELOPENXML = "EXCELOPENXML";
        }

        public sealed class ReporteFormato
        {
            public const string PDF = "pdf";
            public const string EXCEL = "excel";
            public const string EXCELOPENXML = "EXCELOPENXML";
            public const string WORD = "word";
            public const string WORDOPENXML = "WORDOPENXML";
        }

        /// <summary>
        /// Obtener la extension de los formatos
        /// </summary>
        public static Dictionary<string, string> ObtenerExtensionReporte
        {
            get
            {
                var formatExtension = new Dictionary<string, string>
                {
                    {ReporteFormato.PDF, ".pdf"},
                    {ReporteFormato.EXCEL, ".xls"},
                    {ReporteFormato.EXCELOPENXML, ".xlsx"},
                    {ReporteFormato.WORD, ".doc"},
                    {ReporteFormato.WORDOPENXML, ".docx"},

                };
                return formatExtension;
            }
        }

        public class EstadoGenerado
        {
            public const string Registrado = "R";
            public const string Pendiente = "P";
            public const string Generado = "G";
        }

        public class EstadosConfirmacion
        {
            public const int Confirmado = 1;
            public const int Pendiente = 2;
            public const int Rechazado = 0;
            public const int Eliminado = -1;
            public const int Confirmado_Eliminado = 3;

            public const string S_ACEPTAR = "A";
            public const string S_CANCELAR = "C";

        }

        public class Logueo
        {
            public const int NoLogueado = -1;
            public const int Logueado = 1;           
        }

        public class ModulosWeb
        {
            public const int CosapiEnBreve = 1;
            public const int ProyectosEmblematicos = 2;
            public const int CentroCapacitacion = 3;
            public const int TrabajaConNosotros = 4;
            public const int RequisitosIngreso = 5;
            public const int Noticias = 6;
            public const int Contacto = 7;
            public const int Portada = 8;
            public const int Footer = 9;
        }

        public class ModulosSeccionWeb
        {
            public const int CosapiEnBreve_Superior = 100;
            public const int CosapiEnBreve_Logo = 101;
            public const int CosapiEnBreve_Inferior = 102;
            public const int ProyectosEmblematicos_ImagenesSuperiores = 200;
            public const int ProyectosEmblematicos_TextoMedio = 201;
            public const int ProyectosEmblematicos_EventosLineaTiempo = 202;
            public const int ProyectosEmblematicos_EventosLineaTiempoProyecto = 2021;
            public const int ProyectosEmblematicos_ProyectosEmblematicos = 203;
            public const int CentroCapacitacion_TextoSuperior = 300;
            public const int CentroCapacitacion_Galeria = 301;
            public const int CentroCapacitacion_GaleriaFoto = 3011;
            public const int CentroCapacitacion_AlbunesCapacitacion = 302;
            public const int CentroCapacitacion_ImagenesSuperiores = 303;
            public const int CentroCapacitacion_Programas = 304;
            public const int RequisitosIngreso_RequisitosIngreso = 500;
            public const int RequisitosIngreso_ImagenesLaterales = 501;
            public const int Noticias_Noticias = 600;
            public const int Contacto_MayorInformacion = 700;
            public const int Contacto_DireccionesOficina = 701;
            public const int Portada_ImagenesSuperiores = 801;
            public const int Footer_RedesSociales = 901;
        }

        public class Respuesta
        {
            public const string Si = "SI";
            public const string No = "NO";
            public const string SeRevisaNivelTransacciones = "SE REVISA A NIVEL DE TRANSACCIONES";
            public const string NoExistenConflictosPorControlesSOX = "No existen conflictos, por controles SOX y procedimientos internos se valida que el aprobador no sea la misma persona que preparó la valorización. Para este fin las valorizaciones llevan ambas firmas.";
            public const string S_TI = "TI";
            public const string S_NO_TI = "NO TI";
            public const string S_ACEPTAR_WORK = "A";
            public const string S_CANCELAR_WORK = "C";
            public const string NO_APLICA = "NO APLICA";

        }

        public class TypeActivity
        {

            public const string AUTOMATICO = "Automática";
            public const string MANUAL = "Manual";
            public const string AUTOMATICO_MANUAL = "Automática / Manual";
        }
        public class OpeningAF
        {
            public const string Apertura_AF = "http://docs.oracle.com/cd/B40089_10/current/acrobat/120faug.pdf";
        }

        public class Process
        {
            public const int ActivoFijo = 1;
            public const int AdministracionEfectivo = 3;
            public const int CierreContable = 4;
            public const int ComprasCuentasPorPagar = 5;
            public const int Ingresos = 6;
            public const int Inventarios = 7;
            public const int Planillas = 9;
        }


        public class Works
        {
            public const string _1100 = "1100";
            public const string _0001 = "0001";
            public const string _1001 = "1001";
        }


        public class ValHeadGrilla
        {
            public const string personal_Almacen = "personal_Almacen";
            public const string almacenista_Jefe_Almacen = "almacenista_Jefe_Almacen";
            public const string revisor = "revisor";
            public const string usuarioRevisor = "usuario_Revisor";
            public const string observacion = "observacion";
            public const string administrador_Obra = "administrador_Obra";
            public const string asistente_Supervisor_Jefe_Almacen = "asistente_Supervisor_Jefe_Almacen";
            public const string plan_Firmas = "plan_Firmas";
            public const string obra = "obra";
            public const string usuario = "usuario";
            public const string nombreCompleto = "nombreCompleto";
            public const string existeJefeAlmacen = "existeJefeAlmacen";
            public const string JefeGerente = "JefeGerente";
        }

        public class CombinationActivity
        {
            public const string AF19_AF20 = "AF19, AF20";
            public const string AF19_AF22_AF24 = "AF19, AF22, AF24";
            public const string AF21_AF23_AF24 = "AF21, AF23, AF24";
            public const string AF20_AF33 = "AF20, AF33";
            public const string AF22_AF24_AF33 = "AF22, AF24, AF33";
            public const string CC10_CC12 = "CC10, CC12";
            public const string CC28_CC44 = "CC28, CC44";
            public const string CC40_CC41_CC42 = "CC40, CC41, CC42";
            public const string CC29_CC30_CC43 = "CC29, CC30, CC43";
            public const string CC28_CC29_CC30 = "CC28, CC29, CC30";
            public const string CC07_CC50 = "CC07, CC50";
            public const string CC07_CC59 = "CC07, CC59";

            public const string PA01_PA02 = "PA01, PA02";
            public const string PA03_PA04 = "PA03, PA04";
            public const string PA05_PA06 = "PA05, PA06";
            public const string PA03_PA07 = "PA03, PA07";
            public const string PA05_PA07 = "PA05, PA07";
            public const string PA08_PA09 = "PA08, PA09";
            public const string PA10_PA11 = "PA10, PA11";

            public const string PA12_PA13 = "PA12, PA13";
            public const string PA14_PA15 = "PA14, PA15";
            public const string PA16_PA17 = "PA16, PA17";
            public const string PA14_PA18 = "PA14, PA18";
            public const string PA16_PA18 = "PA16, PA18";
            public const string PA19_PA20 = "PA19, PA20";
            public const string PA21_PA22 = "PA21, PA22";

            public const string PA23_PA24 = "PA23, PA24";
            public const string PA27_PA28 = "PA27, PA28";
            public const string PA27_PA29 = "PA27, PA29";
            public const string PA25_PA26 = "PA25, PA26";
            public const string PA25_PA29 = "PA25, PA29";
            public const string PA30_PA31 = "PA30, PA31";

            public const string PA05_PA32 = "PA05, PA32";
            public const string PA08_PA09_PA33 = "PA08, PA09, PA33";

            public const string EF14_EF15_EF16 = "EF14, EF15, EF16";
            public const string EF17_EF18_EF19 = "EF17, EF18, EF19";

            public const string EF01_EF08_EF11 = "EF01, EF08, EF11";
            public const string EF01_EF08_EF12 = "EF01, EF08, EF12";

            public const string INV04_INV15 = "INV04, INV15";
            public const string INV12_INV17 = "INV12, INV17";
            public const string INV13_INV17 = "INV13, INV17";
            public const string INV12_INV14 = "INV12, INV14";
            public const string INV15_INV17_INV18 = "INV15, INV17, INV18";
            public const string INV06_INV07 = "INV06, INV07";
            public const string INV05_INV16 = "INV05, INV16";

            public const string INV02_INV06 = "INV02, INV06";
            public const string INV03_INV07 = "INV03, INV07";
            public const string INV04_INV07 = "INV04, INV07";
            public const string INV03_INV05 = "INV03, INV05";
            public const string INV06_INV07_INV08 = "INV06, INV07, INV08";

            public const string CO03_CO04 = "CO03, CO04";
            public const string CO03_CO04_CO07 = "CO03, CO04, CO07";
            public const string CO03_CO04_CO08 = "CO03, CO04, CO08";

            public const string CO05_CO06 = "CO05, CO06";

            public const string CO06_CO07 = "CO06, CO07";
            public const string CO06_CO08 = "CO06, CO08";
            public const string CO06_CO07_CO08 = "CO06, CO07, CO08";

            public const string ING10_ING11 = "ING10, ING11";
            public const string EF02_EF03_EF07 = "EF02, EF03, EF07";
            public const string ING30_ING32 = "ING30, ING32";
            public const string EF02_EF03_EF27 = "EF02, EF03, EF27";

            public const string CO07_CO12_CO14 = "CO07, CO12, CO14";
            public const string CO08_CO12_CO14 = "CO08, CO12, CO14";

            public const string CO07_CO13_CO14 = "CO07, CO13, CO14";
            public const string CO08_CO13_CO14 = "CO08, CO13, CO14";

            public const string CO07_CO15_CO16 = "CO07, CO15, CO16";
            public const string CO08_CO15_CO16 = "CO08, CO15, CO16";
        }

        public class Company
        {
            public const int GYM = 1;
            public const int StraconGYM = 2;
            public const int VyVDsD = 3;
            public const int GMI = 4;
            public const int NORVIAL = 6;
            public const int GYM_FERROVIAS = 9;
            public const int GMP = 11;
            public const int CONCAR = 14;
            public const int VivaGYM = 12;
            public const int HOLDING = 22;
            public const int MORELCO = 23;
            public const int STRACON_PANAMA = 24;
            public const int TERMINALES_DEL_PERU = 25;
            public const int GRUPO12 = 27;
        }

        public static Dictionary<string, string> ReportFormatExtension
        {
            get
            {
                Dictionary<string, string> formatExtension = new Dictionary<string, string>();
                formatExtension = new Dictionary<string, string>();
                formatExtension.Add(ReportFormat.FORMATO_REPORTE_EXCEL, ReportExtension.DOCUMENTO_EXCEL);
                formatExtension.Add(ReportFormat.FORMATO_REPORTE_PDF, ReportExtension.DOCUMENTO_PDF);
                return formatExtension;
            }
        }     

        /// <summary>
        /// Variables de Configuración de Reporting
        /// </summary>
        public sealed class ConfiguracionReporting
        {
            /// <summary>
            /// ReporteServerUrl
            /// </summary>
            public static readonly string ReporteServerUrl = ConfigurationManager.AppSettings["ReportingService"];

            /// <summary>
            /// ReporteRuta
            /// </summary>
            public static readonly string ReporteRuta = ConfigurationManager.AppSettings["RootReporte"];
        }

        public static class Formaters
        {
            public const string FormatDate = "dd/MM/yyyy";
            public const string FormatDecimalNumber = "#0,#.00";
            public const string StringFormatToNumber = "{0:#0,#.00}";
            public const string FormatDate_US = "yyyy/MM/dd";
        }

        public static DateTime? StringToDateTime(string dateString)
        {
            try
            {
                DateTime? dateValue = new DateTime(int.Parse(dateString.Split('/')[2]), int.Parse(dateString.Split('/')[1]), int.Parse(dateString.Split('/')[0]), 0, 0, 0);
                return dateValue;
            }
            catch (Exception ex)
            {
                throw new Exception("La fecha no es correcta " + ex);
            }
        }

        public static class Estado_Activo_Inactivo
        {
            public const string Seleccione = "Seleccione";
            public const string act = "Activo";
            public const string inac = "Inactivo";
        }

        public static class SessionKey
        {
            public const string USERALIAS = "SK_userAlias";
            public const string USERNAME = "SK_userName";
            public const string USERROLE = "SK_userRole";
            public const string USERPHOTO = "SK_userPhoto";
            public const string USERPERMISSIONS = "SK_userPermission";
            public const string USERID = "SK_userId";
            public const string USERIDUNIDAD = "SK_userIdUnidad";
            public const string USERESTADO = "SK_userEstado";
            public const string USERDNI = "SK_userDni";
        }        

        public static class Jerarquia
        {
            public const string SISTEMA = "Sistema";
            public const string MODULO = "Módulo";
            public const string OPCIONM = "Opción de Menu";
            public const string PERMISO = "Permiso";
            public const string ACCION = "Acción";
        }

        public static class OtherFilters
        {
            public const string Todos = "Todos";
            public const string Seleccione = "Seleccione";
            public const string Otros = "Otros";
        }

        public static class Estado
        {
            public const string Desactivo = "Desactivo";
            public const string Activo = "Activo";
            public const string Pendiente = "P";
            public const string Completado = "C";
        }

        public static class EstadosHardCode
        {
            public const string Activo = "1";
            public const string Inactivo = "0";
        }     

        public static class RutaArchivoCargaMasiva
        {
            public const string RutaCargaMasiva = "RutaCargaMasiva";
        }

        public static class SystemParameter
        {
            public const string Domains = "001";
            public const string FiltersAD = "002";
            public const string TypeFunctions = "003";
            public const string TypeActivities = "004";
            public const string TypeActivCombinated = "005";
            public const string TypePosition = "015";


            public const string Wildcard = "017";
            public const string Weekdays = "018";
            public const string Frequency = "019";
            public const string SharePoint = "020";
        }

        public static class GridType
        {
            public const string GridNoPaginado = "00";
            public const string GridModalEditar = "01";
            public const string GridModalCheckBox = "02";
            public const string GridApprobal = "03";
            public const string GridCriticalPending = "04";
        }
    
        public static class TypeRequest
        {
            public const string Creation = "C";
            public const string Elimination = "D";
        }

        public static class StatusNotification
        {
            public const string Pending = "P";
            public const string Sended = "S";
            public const string Responded = "R";
        }

        public static class TypeApproval
        {
            public const string Accept = "A";
            public const string Denied = "D";
        }

        public static class DatabaseConections
        {
            public const string SRADataBaseContext = "SRADataBaseContext";
            public const string SRAExternalDataBaseContext01 = "SRAExternalDataBaseContext01";
            public const string SRAExternalDataBaseContext02 = "SRAExternalDataBaseContext02";
        }

        public static class SystemType
        {
            public const string Oracle = "ORACLE";
        }

        public static class NamberHeader
        {
            public const string UsuarioConflicto = "USUARIOS EN CONFLICTO";
            public const string TI = "TI";
            public const string NOTI = "NO TI";
            public const string Transacion = "TRANSACCIONES EN CONFLICTO";
        }

        public static class BackgroundColorHeader
        {
            public const string UsuarioConflicto = "#0070C0";
            public const string Transacion = "#8EA9DB";
        }

        public static class FontColorHeader
        {
            public const string UsuarioConflicto = "#FFFFFF";
        }    

        public static class TemplateNotifications
        {
            public const string APROBACION_SOLICITUD = "25737742-6288-4A8C-A62D-7E66D3DCC6FA";
            public const string RESPUESTA_SOLICITUD = "30BEAF39-6A24-40AF-ACB0-28A685057657";
            public const string RESPUESTA_COMITE = "857EA58E-F355-4E77-8E09-B93F9FBAE83A";
        }

        public static class StateCodeRequestDetails
        {
            public const string PENDIENTE_ASIGNAR = "PA";
            public const string PENDIENTE_DESASIGNAR = "PD";
            public const string POR_APROBAR_ASIGNADO = "AA";
            public const string POR_APROBAR_DESASIGNADO = "AD";

            public const string POR_APROBAR_COMPENSATORIO = "PC";

            public const string APROBADO = "A";
            public const string RECHAZADO = "R";
            public const string ASIGNADO = "O";
        }

        public static class StateDescripcionRequestDetails
        {
            public static readonly Dictionary<string, string> ListaValores = new Dictionary<string, string>() {
                                                                { StateCodeRequestDetails.PENDIENTE_ASIGNAR, "Pendiente Asignar" },
                                                                { StateCodeRequestDetails.PENDIENTE_DESASIGNAR, "Pendiente Desasignar" },
                                                                { StateCodeRequestDetails.POR_APROBAR_ASIGNADO, "Por Aprobar Asignado" } ,
                                                                { StateCodeRequestDetails.POR_APROBAR_DESASIGNADO, "Por Aprobar Desasignado" } ,
                                                                { StateCodeRequestDetails.POR_APROBAR_COMPENSATORIO, "Por Aprobar Comité" } ,
                                                                { StateCodeRequestDetails.APROBADO, "Aprobado" } ,
                                                                { StateCodeRequestDetails.RECHAZADO, "Rechazado" } ,
                                                                { StateCodeRequestDetails.ASIGNADO, "Asignado" } };
        }

        public static class Frequency
        {
            public const string Diario = "DI";
            public const string Semanal = "SL";
            public const string Quincenal = "QL";
            public const string Mensual = "ML";
            public const string Bimestral = "BL";
            public const string Trimestral = "TL";
            public const string Semestral = "ST";
            public const string Anual = "AL";
            public const string PorEvento = "EV";
        }
    }
}
