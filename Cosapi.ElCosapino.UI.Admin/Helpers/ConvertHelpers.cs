using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cosapi.ElCosapino.UI.Admin.Helpers
{
    public static class AppSettings
    {
        public static String Get(string key, String value = "")
        {
            return ConfigurationManager.AppSettings[key] ?? value;
        }
    }

    public static class ConvertHelpers
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static class SeparaPalabras
        {
            public enum TipoSeparacion
            {
                Defecto,
                Personalizado
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(params String[] palabras)
            {
                return SepararPalabras(TipoSeparacion.Defecto, palabras);
            }
            /// <summary>
            /// Separa palabras, por defecto el separador será un espacio.
            /// </summary>
            /// <param name="tipoSeparacion">si el tipo de separación es Personalizado, entonces la primera palabra será el separador</param>
            /// <param name="palabras">grupo de palabras a separar</param>
            /// <returns></returns>
            public static String SepararPalabras(TipoSeparacion tipoSeparacion, params String[] palabras)
            {
                int posicion = 1;
                String separador = palabras[0] ?? "";
                if (tipoSeparacion == TipoSeparacion.Defecto)
                {
                    separador = " ";
                    posicion--;
                }
                String palabraSeparada = "";
                posicion++;
                if (palabras.Length > posicion)
                {
                    palabraSeparada = palabras[posicion];
                    for (int i = posicion; i < palabras.Length; i++)
                    {
                        palabraSeparada += separador + (palabras[i] ?? "");
                    }
                }
                return palabraSeparada;
            }
        }


        public static IDictionary<String, Object> ToObjectDictionary(this object val)
        {
            try
            {
                return val.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(prop => prop.Name, prop => prop.GetValue(val, null));
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }


        public static String GetAppSetings(string key, string defaultVal = "")
        {
            try
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[key].ToSafeString();
                var result = String.IsNullOrEmpty(value) ? defaultVal : value;
                return result;
            }
            catch (Exception)
            {
                return defaultVal;
            }
        }

        public static T GetAppSetings<T>(string key)
        {
            try
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static String ToDurationString(this Int32 val)
        {
            var hours = val / 60;
            var minutes = val % 60;
            var text = String.Empty;
            if (hours > 0)
                text += hours.ToString() + "h ";
            text += minutes.ToString("D2") + " m";
            return text;
        }

        public static Int32 ToInteger(this object val)
        {
            return ConvertHelpers.ToInteger(val, 0);
        }
        public static String ToSafeString(this object val)
        {
            return (val ?? String.Empty).ToString();
        }

        public static Int32 ToInteger(this object val, Int32 def)
        {
            try
            {
                Int32 reval = 0;

                if (Int32.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Decimal ToDecimal(this object val)
        {
            return ConvertHelpers.ToDecimal(val, 0);
        }

        public static Decimal ToDecimal(this object val, Decimal def)
        {
            try
            {
                Decimal reval = 0;

                if (Decimal.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static long ToJsonDateTime(this DateTime val)
        {
            return ((long)(val - unixEpoch).TotalSeconds) * 1000;
        }

        public static String ToFullCallendarDate(this DateTime val)
        {
            return val.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static DateTime ToDateTime(this object val)
        {
            return ConvertHelpers.ToDateTime(val, DateTime.MinValue);
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime ToLocalDate(this DateTime val)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(val, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
        }

        /// <summary>
        /// Convierte una fecha UTC a una fecha "Local". Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static DateTime? ToLocalDate(this DateTime? val)
        {
            if (val.HasValue)
                return ToLocalDate(val.Value);
            else
                return val;
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Per� (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Peru</returns>
        public static String ToLocalDateFormat(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime val, String format)
        {
            return val.ToLocalDate().ToString(format);
        }

        /// <summary>
        /// Convierte una fecha UTC a una string de una fecha "Local" con formato. Se toma la hora de Perú (GMT-5)
        /// </summary>
        /// <param name="val">Fecha, en UTC</param>
        /// <param name="format">Formato para el string</param>
        /// <returns>Fecha, en hora de Perú</returns>
        public static String ToLocalDateString(this DateTime? val, String format)
        {
            if (val.HasValue)
                return ToLocalDateString(val.Value, format);
            else
                return "";
        }

        public static DateTime ToDateTime(this object val, DateTime def)
        {
            try
            {
                DateTime reval = DateTime.MinValue;

                if (DateTime.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static Boolean IsBetween(this DateTime val, DateTime Start, DateTime End)
        {
            return val >= Start && val <= End;
        }

        public static Boolean ToBoolean(this object val)
        {
            return ConvertHelpers.ToBoolean(val, false);
        }

        public static Boolean ToBoolean(this object val, Boolean def)
        {
            try
            {
                Boolean reval = false;

                if (Boolean.TryParse(val.ToString(), out reval))
                    return reval;
            }
            catch (Exception ex)
            {
            }

            return def;
        }

        public static string ToJson(this object val)
        {
            return JsonConvert.SerializeObject(val, Formatting.Indented, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, ObjectCreationHandling = ObjectCreationHandling.Reuse });
        }

        public static MvcHtmlString ToHTMLJson(this object val)
        {
            return MvcHtmlString.Create(val.ToJson());
        }



        /// <summary>
        /// Guarda la imagen en una ruta especifica del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <param name="size">Tamaño de la imagen a guardar</param>
        /// <returns>Ruta del de la imagen</returns>
        public static String SaveImageToServer(this HttpPostedFileBase file, HttpServerUtilityBase server, Int32 size = 320)
        {
            var result = String.Empty;
            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Substring(0, 6);
                    result = guid + "_" + Path.GetFileName(file.FileName);
                    var path = server.MapPath(ConstantHelpers.DEFAULT_SERVER_PATH);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var image = System.Drawing.Image.FromStream(file.InputStream);
                    image = image.GetThumbnailImage(size, (size * (image.Height / image.Width).ToDecimal()).ToInteger(), null, IntPtr.Zero);
                    image.Save(Path.Combine(path, result));
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Guarda la imagen en una ruta especifica del servidor
        /// </summary>
        /// <param name="file">Archivo a guardar</param>
        /// <param name="server"></param>
        /// <param name="size">Tamaño de la imagen a guardar</param>
        /// <returns>Ruta del de la imagen</returns>
        public static String SaveFileToServer(this HttpPostedFileBase file, HttpServerUtilityBase server)
        {
            var result = String.Empty;

            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Substring(0, 4);
                    result = guid + "_" + file.FileName;
                    var path = Path.Combine(server.MapPath(ConstantHelpers.DEFAULT_SERVER_PATH), result);
                    file.SaveAs(path);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static String SaveFileToServer(this HttpPostedFileBase file, HttpServerUtilityBase server, string seccondaryServerPath = "")
        {
            var fileName = String.Empty;

            try
            {
                if (file != null && file.ContentLength != 0)
                {
                    var guid = Guid.NewGuid().ToString().Substring(0, 4);
                    fileName = guid + "_" + file.FileName;
                    var path = Path.Combine(server.MapPath(ConstantHelpers.DEFAULT_SERVER_PATH), fileName);
                    file.SaveAs(path);
                    if (!String.IsNullOrEmpty(seccondaryServerPath))
                    {
                        var directoryInfo = new DirectoryInfo(seccondaryServerPath);
                        if (!directoryInfo.Exists)
                            directoryInfo.Create();

                        path = Path.Combine(seccondaryServerPath, fileName);
                        file.SaveAs(path);
                    }
                }
                return fileName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public static class PeruDateTime
    {
        public static DateTime Now
        {
            get
            {
                return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")));
            }
        }
    }
}