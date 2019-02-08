using System;
using System.Web;

namespace Cosapi.ElCosapino.UI.Web.Helpers
{
    public static class ConstantHelpers
    {
        public static readonly Int32 DEFAULT_PAGE_SIZE = 25;
        public static readonly Int32 DEFAULT_PAGE_SIZE_MODAL = 10;

        public const String ESTADO_ACTIVO = "ACT";
        public const String ESTADO_ELIMINADO = "ELI";
        public const String ESTADO_INACTIVO = "INA";
        public const String ESTADO_REGISTRADO = "REG";
        public const String ESTADO_VERIFICADO = "VER";
        public const String ESTADO_PENDIENTE = "PEN";
        public const String ESTADO_PROCESO = "PRO";
        public const String ESTADO_ENVIADO = "env";

        public const String ROL_USUARIO = "USU";
        public const String ROL_COMPLETO_USUARIO = "USUARIO";

        public const String ROL_ADMINISTRADOR = "ADM";
        public const String ROL_COMPLETO_ADMINISTRADOR = "ADMINISTRADOR";

        public const String ROL_ANALISTA = "ANA";
        public const String ROL_COMPLETO_ANALISTA = "ANALISTA";

        public const String DEFAULT_SERVER_PATH = "~/Files/Item/";

        public const String INSTITUCION_INTERNA_COSAPI = "UNIVERSIDAD CORPORATIVA COSAPI";
        public static String GetLabelEstado(String Estado)
        {
            switch (Estado)
            {
                case ESTADO_ACTIVO:
                    return "<span class='label label-success'>ACTIVO</span>";
                case ESTADO_REGISTRADO:
                    return "<span class='label label-info'>REGISTRADO</span>";
                case ESTADO_VERIFICADO:
                    return "<span class='label label-success'>VERFICADO</span>";
                case ESTADO_INACTIVO:
                    return "<span class='label label-default'>INACTIVO</span>";
                case ESTADO_PROCESO:
                    return "<span class='label label-secondary'>EN PROCESO</span>";
                case ESTADO_PENDIENTE:
                    return "<span class='label label-danger'>PENDIENTE</span>";
                case ESTADO_ENVIADO:
                    return "<span class='label label-success'>ENVIADO</span>";
            }
            return String.Empty;
        }

        public class Empresa
        {
            public const Int32 COSAPI = 3761;
            public const Int32 OTROS = 7063;
        }

        public class TipoDocumento
        {
            public const String RESOLUCION_MULTA = "RMU";
            public const String ORDEN_PAGO = "OPA";
            public const String RESOLUCION_DETERMINACION = "RDE";
            public const String OFICIO = "OFI";
            public const String GRADUALIDAD = "GRA";
        }

        public class TipoCategoria
        {
            public const String POST_GRADO = "PGR";
            public const String COSAPINO = "ESP";
            public const String CURSO = "CUR";
        }

        public static String ObtenerRutaBase(String Ruta)
        {
            return "~/Files/Item/" + Ruta;
        }

        public static String GetTagTipoDocumento(String Tipo)
        {
            switch (Tipo)
            {
                case TipoDocumento.RESOLUCION_MULTA: return "<span class='tag tag-success float-xl-none'>Resolución de Multa</span>";
                case TipoDocumento.ORDEN_PAGO: return "<span class='tag tag-danger float-xl-none'>Orden de Pago</span>";
                case TipoDocumento.RESOLUCION_DETERMINACION: return "<span class='tag tag-primary float-xl-none'>Resolución de Determinación</span>";
                case TipoDocumento.OFICIO: return "<span class='tag tag-default float-xl-none'>Oficio</span>";
                case TipoDocumento.GRADUALIDAD: return "<span class='tag tag-warning float-xl-none'>Gradualidad</span>";
            }
            return String.Empty;
        }
        public static string GetSiteRoot()
        {
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = String.Empty;
            else
                port = ":" + port;

            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string sOut = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + HttpContext.Current.Request.ApplicationPath;

            if (sOut.EndsWith("/"))
            {
                sOut = sOut.Substring(0, sOut.Length - 1);
            }

            return sOut;
        }
        public static String GetMesporNumeroMes(Int32 numero)
        {
            switch (numero)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
            }
            return String.Empty;
        }

        public static class Layout
        {
            public static readonly String MODAL_LAYOUT_PATH = "~/Views/Shared/_ModalLayout.cshtml";
            public static readonly String MODAL_EMAIL_PATH = "~/Views/Shared/_MailLayout.cshtml";
        }        
    }
}