using Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Cosapi.ElCosapino.CrossCutting.Util
{
    public static class Utiles
    {
        public static int GetPaginaActual(int paginaActual, string PaginaSolicitada, int cantidadTotalFilas, int FilasPorPagina)
        {
            var page = 0;
            if (paginaActual == 0) return 1;

            if (string.IsNullOrEmpty(PaginaSolicitada)) return 1;

            switch (PaginaSolicitada)
            {
                case Constantes.Paginacion.First: page = 1; break;
                case Constantes.Paginacion.Before: page = paginaActual - 1; break;
                case Constantes.Paginacion.Next: page = paginaActual + 1; break;
                case Constantes.Paginacion.Last: page = (cantidadTotalFilas / FilasPorPagina) + 1; break;
            }
            return page;
        }

        public static bool IsValidStringToDate(string dateformat, string dateString)
        {
            try
            {
                DateTime dateValue;
                return DateTime.TryParseExact(dateString, dateformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime? StringToDatetime(string dateformat, string dateString)
        {
            try
            {
                DateTime dateValue;
                if (DateTime.TryParseExact(dateString, dateformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    return dateValue;
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static T EntidadCopy<T>(object reader)
        {

            Type oTypePrincipal = reader.GetType();
            PropertyInfo[] propsPrincipal = oTypePrincipal.GetProperties();

            Type oTypeHereda = typeof(T);
            var objectHerda = Activator.CreateInstance<T>();

            foreach (PropertyInfo oPropertyInfoPrincipal in propsPrincipal)
                if (oPropertyInfoPrincipal.GetIndexParameters().Length == 0)
                {
                    foreach (PropertyInfo oPropertyInfoHereda in oTypeHereda.GetProperties())
                    {
                        if (oPropertyInfoHereda.Name == oPropertyInfoPrincipal.Name)
                        {
                            oPropertyInfoHereda.SetValue(objectHerda, DefaultValue(oPropertyInfoHereda, oPropertyInfoPrincipal.GetValue(reader)));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("   {0} ({1}): <Indexed>", oPropertyInfoPrincipal.Name,
                                      oPropertyInfoPrincipal.PropertyType.Name);
                }
            return objectHerda;
        }

        public static T Entidad<T>(IDataReader reader)
        {
            Type oType = typeof(T);

            var Object = Activator.CreateInstance<T>();
            foreach (PropertyInfo oPropertyInfo in oType.GetProperties())
            {
                if (ColumnExists(reader, oPropertyInfo.Name))
                {
                    oPropertyInfo.SetValue(Object, DefaultValue(oPropertyInfo, reader[oPropertyInfo.Name]));
                }
            }
            //lstResult.Add(Object);
            return Object;
        }
        private static object DefaultValue(PropertyInfo oPropertyInfo, object objet)
        {
            if (Convert.IsDBNull(objet))
            {
                return null;
            }
            else
            {
                return objet;
            }
        }
        private static bool ColumnExists(IDataReader oIDataReader, string sColumnName)
        {
            for (int i = 0; i < oIDataReader.FieldCount; i++)
            {
                if (oIDataReader.GetName(i).Equals(sColumnName))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ExisteColumna(IDataReader oIDataReader, string sColumnName)
        {
            for (int i = 0; i < oIDataReader.FieldCount; i++)
            {
                if (oIDataReader.GetName(i).Equals(sColumnName))
                {
                    return true;
                }
            }
            return false;
        }

    }

    public static class JsonHelper
    {
        public static T DeserializeObject<T>(string objString)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(objString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        public static string SerializeObject<T>(T valores)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(valores);
        }
    }

    public static class Logeo
    {
        public static string getUser()
        {
            return null;
        }
    }

    //public static class WCF
    //{
    //    private static object sync = new object();
    //    private static ISeguridadService _MyWebServiceSeguridadInstance;
    //    public static ISeguridadService SeguridadInstance
    //    {
    //        get
    //        {
    //            lock (sync)
    //            {
    //                if (_MyWebServiceSeguridadInstance == null)
    //                {
    //                    _MyWebServiceSeguridadInstance = ProxyLocator.InstanciarSeguridadService();
    //                }
    //            }
    //            return _MyWebServiceSeguridadInstance;
    //        }
    //    }
    //}

    public class Encrypting
    {
        const int _intLONGITUD = 5;
        const string _strKEY = "Cosap1";
        public string EncryptKey(string cadena)
        {            
            string vl_strSalidaFormato = "";
            try
            {
                //Permite manejo del algoritmo de enmascaramiento
                System.Security.Cryptography.RijndaelManaged vl_rijAlgoritmo = new System.Security.Cryptography.RijndaelManaged();

                //Texto plano
                byte[] vl_bytTextoPlano = System.Text.Encoding.Unicode.GetBytes(cadena);

                //Saltos del algoritmo
                byte[] vl_bytSaltos = Encoding.ASCII.GetBytes(_strKEY.Length.ToString());

                //Clave derivada de la cantidad de saltos y el reporte
                System.Security.Cryptography.PasswordDeriveBytes vl_serKey = new System.Security.Cryptography.PasswordDeriveBytes(_strKEY, vl_bytSaltos);

                //Permite enmascarar la cadena
                System.Security.Cryptography.ICryptoTransform vl_enmascarador = vl_rijAlgoritmo.CreateEncryptor(vl_serKey.GetBytes(32), vl_serKey.GetBytes(16));

                //Para realizar las operaciones en memoria
                using (MemoryStream vl_menMemoria = new MemoryStream())
                {
                    //Para realizar la enmascaracion
                    using (System.Security.Cryptography.CryptoStream vl_criStream = new System.Security.Cryptography.CryptoStream(vl_menMemoria, vl_enmascarador, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        //Escribimos la memoria en texto plano
                        vl_criStream.Write(vl_bytTextoPlano, 0, vl_bytTextoPlano.Length);

                        //actualizamos el buffer al stream
                        vl_criStream.FlushFinalBlock();

                        //Para la cadena enmascarada
                        byte[] vl_bytCadenaEnmascarada = vl_menMemoria.ToArray();

                        //String de salida sin formato
                        string vl_strSalida = "";

                        //Por cada letra enmascarada se procede a transformarla en Hexadecimal
                        foreach (byte vl_bytLetra in vl_bytCadenaEnmascarada)
                        {
                            //Para que cada dueto hexadecimal sea siempre de 2 caracteres, la maxima combinacion hexadecimal es FF
                            vl_strSalida += vl_bytLetra.ToString("X").PadLeft(2, '0');
                        }

                        //Para la salida con formato


                        //Se recorre la cadena enmascarada para agruparla 
                        for (int vl_intAux = 0; vl_intAux < vl_strSalida.Length; vl_intAux = vl_intAux + _intLONGITUD)
                        {
                            //Si la longitud de la cadena a agrupar es mayor al de la 
                            //cadena restante
                            if (vl_intAux + _intLONGITUD > vl_strSalida.Length)
                            {
                                //PAra ver si se pone o no guion de sepracion
                                if (vl_strSalidaFormato.Length > 0)
                                {
                                    vl_strSalidaFormato += "-" + vl_strSalida.Substring(vl_intAux).PadRight(_intLONGITUD, '0');
                                }
                                else
                                {
                                    vl_strSalidaFormato += vl_strSalida.Substring(vl_intAux).PadRight(_intLONGITUD, '0');
                                }
                            }
                            else
                            {
                                //Para ver si se pone o no guion de separacion
                                if (vl_strSalidaFormato.Length > 0)
                                {
                                    //concatenamos grupo a cadena de salida
                                    vl_strSalidaFormato += "-" + vl_strSalida.Substring(vl_intAux, _intLONGITUD);
                                }
                                else
                                {
                                    //concatenamos grupo a cadena de salida
                                    vl_strSalidaFormato += vl_strSalida.Substring(vl_intAux, _intLONGITUD);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            return vl_strSalidaFormato;
        }

        public string DecryptKey(string clave)
        {
            /* try
             {
                 byte[] keyArray;
                 var Array_a_Descifrar = Convert.FromBase64String(clave);
                 var hashmd5 = new MD5CryptoServiceProvider();
                 keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                 hashmd5.Clear();
                 var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
                 var cTransform = tdes.CreateDecryptor();
                 var resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                 tdes.Clear();
                 return UTF8Encoding.UTF8.GetString(resultArray);
             }
             catch
             {
                 return clave;
             }*/
            string vl_strSalida = "";
            try
            {
                //Cadena enmascarada
                string vl_strCadenaEnmascarada;

                //Quitamos los signos de separacion
                string vl_strCadenaLimpia = clave.Replace("-", "");

                /*****************************************************************************************/
                /*Convertimos la cadena de hexadecimal a bytes y de alli lo pasamos a cadena enmascarada */
                /*****************************************************************************************/

                //TextoOfuscado en byte
                List<byte> vl_bytTextoOfuscado = new List<byte>();

                //limpiamos la cadena de caracteres no validos colocados al completar la cadena de ofuscacion
                int vl_intResiduo = vl_strCadenaLimpia.Length % 32;

                //retiramos la cadena 
                vl_strCadenaLimpia = vl_strCadenaLimpia.Remove(vl_strCadenaLimpia.Length - vl_intResiduo, vl_intResiduo);

                //Hasta donde se recorrera la cadena, ya que al enmascarar una cadena se completa con nulos
                //para mantener grupos iguales, solo se recorren los elementos con datos
                int hasta = vl_strCadenaLimpia.Length - vl_strCadenaLimpia.Length % 2;

                //Recorremos la cadena y la agrupanos en duetos para poder convertirlos en byte[] para
                //poder manipularlo con el algoritmo de enmascaramiento
                for (int vl_intAux = 0; vl_intAux < hasta; vl_intAux = vl_intAux + 2)
                {
                    //Obtenemos el numero entero del decimal
                    int vl_intCaracter = int.Parse(vl_strCadenaLimpia.Substring(vl_intAux, 2), System.Globalization.NumberStyles.HexNumber);

                    //Agregamos el entero como byte[]
                    vl_bytTextoOfuscado.Add(byte.Parse(vl_intCaracter.ToString()));
                }

                //Texto enmascarado
                vl_strCadenaEnmascarada = Convert.ToBase64String(vl_bytTextoOfuscado.ToArray());

                //Algoritmo de enmascaramiento
                System.Security.Cryptography.RijndaelManaged vl_rigAlgoritmo = new System.Security.Cryptography.RijndaelManaged();

                //Data Enmascarada
                byte[] vl_bytDataEnmascarada = Convert.FromBase64String(vl_strCadenaEnmascarada);

                //Numero de saltos que dara el algoritmo
                byte[] vl_bytSaltos = Encoding.ASCII.GetBytes(_strKEY.Length.ToString());

                //Creador de Clave
                System.Security.Cryptography.PasswordDeriveBytes vl_passSecreto = new System.Security.Cryptography.PasswordDeriveBytes(_strKEY, vl_bytSaltos);

                //Desenmascarador
                System.Security.Cryptography.ICryptoTransform vl_cryDesenmascarador = vl_rigAlgoritmo.CreateDecryptor(vl_passSecreto.GetBytes(32), vl_passSecreto.GetBytes(16));

                //Para el trabajo en memoria
                MemoryStream vl_menMemoryStream = new MemoryStream(vl_bytDataEnmascarada);

                //Para desenmascarar en memoria
                System.Security.Cryptography.CryptoStream vl_cryCryptoStream = new System.Security.Cryptography.CryptoStream(vl_menMemoryStream, vl_cryDesenmascarador, System.Security.Cryptography.CryptoStreamMode.Read);

                //Creamos el contenedor del texto desenmascarado
                byte[] PlainText = new byte[vl_bytDataEnmascarada.Length];

                //Cantidad de elementos a leer
                int vl_intLongitud = vl_cryCryptoStream.Read(PlainText, 0, PlainText.Length);

                //devolvemos el texto desenmascarado
                vl_strSalida = Encoding.Unicode.GetString(PlainText, 0, vl_intLongitud);
            }
            catch (Exception vl_BLMensajeError)
            {
                throw vl_BLMensajeError;
            }

            return vl_strSalida;
        }
    }

    public static class Converter
    {
        public static DataTable ListaToDatatable<T>(IList<T> items)
        {
            var dataTable = new DataTable();
            Type itemsType = typeof(T);
            foreach (PropertyInfo prop in itemsType.GetProperties())
            {
                var column = new DataColumn(prop.Name)
                {
                    DataType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                };
                dataTable.Columns.Add(column);
            }
            foreach (var item in items)
            {
                int j = 0;
                object[] newRow = new object[dataTable.Columns.Count];
                foreach (PropertyInfo prop in itemsType.GetProperties())
                {
                    newRow[j] = prop.GetValue(item, null);
                    j++;
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;
        }
    }

    public static class LeerExcel
    {
        public static DataSet ExcelToDataset(String xRuta, bool IsFirstRowAsColumnNames = true)
        {
            FileStream stream = File.Open(xRuta, FileMode.Open, FileAccess.Read);
            DataSet ds = new DataSet();
            if (Path.GetExtension(xRuta) == ".xls")
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                excelReader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;
                ds = excelReader.AsDataSet();
            }
            if (Path.GetExtension(xRuta) == ".xlsx")
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;
                ds = excelReader.AsDataSet();
            }
            return ds;
        }
    }

    public static class ArchivoExcel
    {
        public static int dataRow = 3;
        public static int dataColumn = 1;
        //public static byte[] CrearArchivoExcel(List<string> sheet, List<string> listHeaders = null, List<string> sublistHeaders = null, List<CriticalActivity_CombinatedActivity> sublistData = null)
        //{
        //    byte[] bytes;
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        foreach (var item in sheet)
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(item.Split(',')[0].ToString());

        //            if (Convert.ToInt32(item.Split(',')[2]) == 0)
        //            {
        //                #region Header

        //                if (listHeaders.Count != 0)
        //                {
        //                    int headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[1, headerColumns].Value = string.Empty;
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }
        //                    headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[2, headerColumns].Value = string.Empty;
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }
        //                    headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[3, headerColumns].Style.Font.Bold = true;
        //                        ws.Cells[3, headerColumns].Style.Font.Size = 12;
        //                        ws.Cells[3, headerColumns].Value = Hitem;
        //                        ws.Cells[3, headerColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        ws.Cells[3, headerColumns].Style.WrapText = true;
        //                        ws.Cells[3, headerColumns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        ws.Cells[3, headerColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }

        //                    ExcelRange rndgA = ws.Cells[1, 7, 1, 12];
        //                    rndgA.Merge = true;
        //                    rndgA.Value = Constantes.NamberHeader.UsuarioConflicto;
        //                    rndgA.Style.Font.Bold = true;
        //                    rndgA.Style.Font.Size = 10;
        //                    rndgA.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgA.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgA.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgA.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgA.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgA.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    ExcelRange rndgB = ws.Cells[2, 7, 2, 9];
        //                    rndgB.Merge = true;
        //                    rndgB.Value = Constantes.NamberHeader.TI;
        //                    rndgB.Style.Font.Bold = true;
        //                    rndgB.Style.Font.Size = 10;
        //                    rndgB.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgB.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgB.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgB.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgB.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgB.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    ExcelRange rndgC = ws.Cells[2, 10, 2, 12];
        //                    rndgC.Merge = true;
        //                    rndgC.Value = Constantes.NamberHeader.NOTI;
        //                    rndgC.Style.Font.Bold = true;
        //                    rndgC.Style.Font.Size = 10;
        //                    rndgC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    ExcelRange rndgCC = ws.Cells[2, 13, 2, 16];
        //                    rndgCC.Merge = true;
        //                    rndgCC.Value = Constantes.NamberHeader.Transacion;
        //                    rndgCC.Style.Font.Bold = true;
        //                    rndgCC.Style.Font.Size = 10;
        //                    rndgCC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgCC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgCC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgCC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgCC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgCC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    using (ExcelRange rng = ws.Cells[3, 1, 3, headerColumns - 1])
        //                    {
        //                        rng.Style.Font.Bold = true;
        //                        rng.Style.Font.Size = 10;
        //                        rng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    }
        //                }

        //                #endregion Header

        //                #region Data

        //                int dataRow = 4;
        //                foreach (var itemDataHeader in sublistData)
        //                {
        //                    ws.Cells[dataRow, 1].Value = itemDataHeader.S_COMPANY_NAME;
        //                    ws.Column(1).AutoFit();
        //                    ws.Cells[dataRow, 2].Value = itemDataHeader.S_PROCESS_NAME;
        //                    ws.Column(2).AutoFit();
        //                    ws.Cells[dataRow, 3].Value = itemDataHeader.S_ACTIVITY_NAME;
        //                    ws.Column(3).AutoFit();
        //                    ws.Cells[dataRow, 4].Value = itemDataHeader.S_CODE;
        //                    ws.Column(4).AutoFit();
        //                    ws.Cells[dataRow, 5].Value = itemDataHeader.S_TYPE_ACTIVITY;
        //                    ws.Column(5).AutoFit();
        //                    ws.Cells[dataRow, 6].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(6).AutoFit();
        //                    ws.Cells[dataRow, 7].Value = itemDataHeader.S_CONFLICT_TI;
        //                    ws.Column(7).AutoFit();
        //                    ws.Cells[dataRow, 8].Value = itemDataHeader.S_NRO_CONFLICT_TI;
        //                    ws.Column(8).AutoFit();
        //                    ws.Cells[dataRow, 9].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(9).AutoFit();
        //                    ws.Cells[dataRow, 10].Value = itemDataHeader.S_CONFLICT_NOTI;
        //                    ws.Column(10).AutoFit();
        //                    ws.Cells[dataRow, 11].Value = itemDataHeader.S_NRO_CONFLICT_NOTI;
        //                    ws.Column(11).AutoFit();
        //                    ws.Cells[dataRow, 12].Value = itemDataHeader.S_ANEXO_TRANSACTION;
        //                    ws.Column(12).AutoFit();
        //                    ws.Cells[dataRow, 13].Value = itemDataHeader.S_FLAG_TRANSACTION;
        //                    ws.Column(13).AutoFit();
        //                    ws.Cells[dataRow, 14].Value = itemDataHeader.S_NRO_USER_TRANSACTION;
        //                    ws.Column(14).AutoFit();
        //                    ws.Cells[dataRow, 15].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    ws.Column(15).AutoFit();
        //                    //ws.Cells[dataRow, 16].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    //ws.Column(16).AutoFit();
        //                    //ws.Cells[dataRow, 6].Value = itemDataHeader.S_NEW_FLAG;
        //                    //ws.Column(6).AutoFit();
        //                    dataRow += 1;
        //                }

        //                #endregion Data
        //            }
        //            else if (Convert.ToInt32(item.Split(',')[2]) == 1)
        //            {
        //                #region SubHeader

        //                if (sublistHeaders.Count != 0)
        //                {
        //                    var numCombinacion = int.Parse(item.Split(',')[0].Split('.')[0].Substring(item.Split(',')[0].IndexOf(".") - 2)) - 1;
        //                    int subheaderColumns = 1;
        //                    foreach (var subitem in sublistHeaders)
        //                    {
        //                        ws.Cells[1, subheaderColumns].Style.Font.Bold = true;
        //                        ws.Cells[1, subheaderColumns].Style.Font.Size = 12;
        //                        ws.Cells[1, subheaderColumns].Value = subitem;
        //                        ws.Cells[1, subheaderColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        ws.Cells[1, subheaderColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                        ws.Column(subheaderColumns).AutoFit();
        //                        subheaderColumns += 1;
        //                        //En la sexta cabecera pregunto si es Automática para seguir o no cargando la segunda parte de las cabeceras
        //                        if (subheaderColumns == 7)
        //                        {
        //                            if (sublistData[numCombinacion].S_TYPE_ACTIVITY.IndexOf("Manual") > -1)
        //                            {
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    using (ExcelRange subrng = ws.Cells[1, 1, 1, subheaderColumns - 1])
        //                    {
        //                        subrng.Style.Font.Bold = true;
        //                        subrng.Style.Font.Size = 10;
        //                        subrng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    }
        //                }

        //                #endregion SubHeader

        //                #region SubData

        //                int dataRow = 2;
        //                foreach (var subitem in sublistData)
        //                {
        //                    foreach (var itemm in subitem.ListReportConflictUserActivity)
        //                    {
        //                        if (itemm.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                        {
        //                            ws.Cells[dataRow, 1].Value = itemm.S_USER_NAME1;
        //                            ws.Column(1).AutoFit();
        //                            ws.Cells[dataRow, 2].Value = itemm.S_DESCRIPTION1;
        //                            ws.Column(2).AutoFit();
        //                            ws.Cells[dataRow, 3].Value = itemm.S_BOOK1;
        //                            ws.Column(3).AutoFit();
        //                            ws.Cells[dataRow, 4].Value = itemm.S_PROYECTO1;
        //                            ws.Column(4).AutoFit();
        //                            ws.Cells[dataRow, 5].Value = itemm.S_TI1;
        //                            ws.Column(5).AutoFit();
        //                            ws.Cells[dataRow, 6].Value = itemm.S_NUM_DOC1;
        //                            ws.Column(6).AutoFit();
        //                            if (subitem.S_TYPE_ACTIVITY.IndexOf("Manual") == -1)
        //                            {
        //                                ws.Cells[dataRow, 7].Value = itemm.S_USER_NAME2;
        //                                ws.Column(7).AutoFit();
        //                                ws.Cells[dataRow, 8].Value = itemm.S_DESCRIPTION2;
        //                                ws.Column(8).AutoFit();
        //                                ws.Cells[dataRow, 9].Value = itemm.S_BOOK2;
        //                                ws.Column(9).AutoFit();
        //                                ws.Cells[dataRow, 10].Value = itemm.S_PROYECTO2;
        //                                ws.Column(10).AutoFit();
        //                                ws.Cells[dataRow, 11].Value = itemm.S_TI2;
        //                                ws.Column(11).AutoFit();
        //                                ws.Cells[dataRow, 12].Value = itemm.S_NUM_DOC2;
        //                                ws.Column(12).AutoFit();
        //                            }
        //                            dataRow += 1;
        //                        }
        //                    }
        //                }

        //                #endregion SubData
        //            }
        //            else if (Convert.ToInt32(item.Split(',')[2]) == 2)
        //            {
        //                #region Header Transaction

        //                int subheaderColumns = 1;
        //                foreach (var itemTrans in sublistData)
        //                {
        //                    if (itemTrans.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                    {
        //                        foreach (var itemHeadTras in itemTrans.ListCombinatedSectionActivity)
        //                        {
        //                            ws.Cells[1, subheaderColumns].Style.Font.Bold = true;
        //                            ws.Cells[1, subheaderColumns].Style.Font.Size = 12;
        //                            ws.Cells[1, subheaderColumns].Value = itemHeadTras.NAME;
        //                            ws.Cells[1, subheaderColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                            ws.Cells[1, subheaderColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                            ws.Column(subheaderColumns).AutoFit();
        //                            subheaderColumns += 1;
        //                        }
        //                    }
        //                }

        //                using (ExcelRange subrng = ws.Cells[1, 1, 1, subheaderColumns - 1])
        //                {
        //                    subrng.Style.Font.Bold = true;
        //                    subrng.Style.Font.Size = 10;
        //                    subrng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                }

        //                #endregion Header Transaction

        //                #region Body

        //                int dataRow = 2;
        //                foreach (var itemTrans in sublistData)
        //                {
        //                    if (itemTrans.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                    {
        //                        foreach (var itemTrans_ in itemTrans.ListCombinatedSectionGeneric)
        //                        {
        //                            ws.Cells[dataRow, 1].Value = itemTrans_.COLUMNA1; ws.Column(1).AutoFit();
        //                            ws.Cells[dataRow, 2].Value = itemTrans_.COLUMNA2; ws.Column(2).AutoFit();
        //                            ws.Cells[dataRow, 3].Value = itemTrans_.COLUMNA3; ws.Column(3).AutoFit();
        //                            ws.Cells[dataRow, 4].Value = itemTrans_.COLUMNA4; ws.Column(4).AutoFit();
        //                            ws.Cells[dataRow, 5].Value = itemTrans_.COLUMNA5; ws.Column(5).AutoFit();
        //                            ws.Cells[dataRow, 6].Value = itemTrans_.COLUMNA6; ws.Column(6).AutoFit();
        //                            ws.Cells[dataRow, 7].Value = itemTrans_.COLUMNA7; ws.Column(7).AutoFit();
        //                            ws.Cells[dataRow, 8].Value = itemTrans_.COLUMNA8; ws.Column(8).AutoFit();
        //                            ws.Cells[dataRow, 9].Value = itemTrans_.COLUMNA9; ws.Column(9).AutoFit();
        //                            ws.Cells[dataRow, 10].Value = itemTrans_.COLUMNA10; ws.Column(10).AutoFit();
        //                            ws.Cells[dataRow, 11].Value = itemTrans_.COLUMNA11; ws.Column(11).AutoFit();
        //                            ws.Cells[dataRow, 12].Value = itemTrans_.COLUMNA12; ws.Column(12).AutoFit();
        //                            ws.Cells[dataRow, 13].Value = itemTrans_.COLUMNA13; ws.Column(13).AutoFit();
        //                            ws.Cells[dataRow, 14].Value = itemTrans_.COLUMNA14; ws.Column(14).AutoFit();
        //                            ws.Cells[dataRow, 15].Value = itemTrans_.COLUMNA15; ws.Column(15).AutoFit();
        //                            ws.Cells[dataRow, 16].Value = itemTrans_.COLUMNA16; ws.Column(16).AutoFit();
        //                            dataRow += 1;
        //                        }
        //                    }
        //                }

        //                #endregion Body
        //            }
        //        }
        //        bytes = pck.GetAsByteArray();
        //    }
        //    return bytes;
        //}
        //public static byte[] CrearArchivoExcelCierreContable(List<string> sheet, List<string> listHeaders = null, List<string> sublistHeaders = null, List<CriticalActivity_CombinatedActivity> sublistData = null)
        //{
        //    byte[] bytes;
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        foreach (var item in sheet)
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(item.Split(',')[0].ToString());

        //            if (Convert.ToInt32(item.Split(',')[2]) == 0)
        //            {
        //                #region Header

        //                if (listHeaders.Count != 0)
        //                {
        //                    int headerColumns = 1;

        //                    //foreach (var Hitem in listHeaders)
        //                    //{
        //                    //    ws.Cells[1, headerColumns].Value = string.Empty;
        //                    //    ws.Column(headerColumns).AutoFit();
        //                    //    headerColumns += 1;
        //                    //}
        //                    //headerColumns = 1;

        //                    //foreach (var Hitem in listHeaders)
        //                    //{
        //                    //    ws.Cells[2, headerColumns].Value = string.Empty;
        //                    //    ws.Column(headerColumns).AutoFit();
        //                    //    headerColumns += 1;
        //                    //}
        //                    //headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[1, headerColumns].Value = string.Empty;
        //                        ws.Cells[2, headerColumns].Value = string.Empty;
        //                        ws.Cells[3, headerColumns].Style.Font.Bold = true;
        //                        ws.Cells[3, headerColumns].Style.Font.Size = 12;
        //                        ws.Cells[3, headerColumns].Value = Hitem;
        //                        ws.Cells[3, headerColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        ws.Cells[3, headerColumns].Style.WrapText = true;
        //                        ws.Cells[3, headerColumns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        ws.Cells[3, headerColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }
        //                    using (ExcelRange rndgA = ws.Cells[1, 6, 1, 11])
        //                    {
        //                        //ExcelRange rndgA = ws.Cells[1, 6, 1, 11];
        //                        rndgA.Merge = true;
        //                        rndgA.Value = Constantes.NamberHeader.UsuarioConflicto;
        //                        rndgA.Style.Font.Bold = true;
        //                        rndgA.Style.Font.Size = 10;
        //                        rndgA.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        rndgA.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        rndgA.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        rndgA.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        rndgA.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        rndgA.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    }

        //                    using (ExcelRange rndgA = ws.Cells[2, 6, 2, 15])
        //                    {
        //                        rndgA.Style.Font.Bold = true;
        //                        rndgA.Style.Font.Size = 10;
        //                        rndgA.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        rndgA.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        rndgA.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        rndgA.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        rndgA.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        rndgA.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    }

        //                    using (ExcelRange rndgB = ws.Cells[2, 6, 2, 8])
        //                    {
        //                        //ExcelRange rndgB = ws.Cells[2, 6, 2, 8];
        //                        rndgB.Merge = true;
        //                        rndgB.Value = Constantes.NamberHeader.TI;
        //                        //rndgB.Style.Font.Bold = true;
        //                        //rndgB.Style.Font.Size = 10;
        //                        //rndgB.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        //rndgB.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        //rndgB.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        //rndgB.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        //rndgB.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        //rndgB.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    }

        //                    using (ExcelRange rndgC = ws.Cells[2, 9, 2, 11])
        //                    {
        //                        //ExcelRange rndgC = ws.Cells[2, 9, 2, 11];
        //                        rndgC.Merge = true;
        //                        rndgC.Value = Constantes.NamberHeader.NOTI;
        //                        //rndgC.Style.Font.Bold = true;
        //                        //rndgC.Style.Font.Size = 10;
        //                        //rndgC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        //rndgC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        //rndgC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        //rndgC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        //rndgC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        //rndgC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    }

        //                    using (ExcelRange rndgCC = ws.Cells[2, 12, 2, 15])
        //                    {
        //                        //ExcelRange rndgCC = ws.Cells[2, 12, 2, 15];
        //                        rndgCC.Merge = true;
        //                        rndgCC.Value = Constantes.NamberHeader.Transacion;
        //                        //rndgCC.Style.Font.Bold = true;
        //                        //rndgCC.Style.Font.Size = 10;
        //                        //rndgCC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        //rndgCC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        //rndgCC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        //rndgCC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        //rndgCC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        //rndgCC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    }

        //                    using (ExcelRange rng = ws.Cells[3, 1, 3, headerColumns - 1])
        //                    {
        //                        rng.Style.Font.Bold = true;
        //                        rng.Style.Font.Size = 10;
        //                        rng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    }
        //                }

        //                #endregion Header

        //                #region Data

        //                int dataRow = 4;
        //                foreach (var itemDataHeader in sublistData)
        //                {
        //                    ws.Cells[dataRow, 1].Value = itemDataHeader.S_COMPANY_NAME;
        //                    ws.Column(1).AutoFit();
        //                    ws.Cells[dataRow, 2].Value = itemDataHeader.S_PROCESS_NAME;
        //                    ws.Column(2).AutoFit();
        //                    ws.Cells[dataRow, 3].Value = itemDataHeader.S_ACTIVITY_NAME;
        //                    ws.Column(3).AutoFit();
        //                    ws.Cells[dataRow, 4].Value = itemDataHeader.S_CODE;
        //                    ws.Column(4).AutoFit();
        //                    ws.Cells[dataRow, 5].Value = itemDataHeader.S_TYPE_ACTIVITY;
        //                    ws.Column(5).AutoFit();
        //                    ws.Cells[dataRow, 6].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(6).AutoFit();
        //                    ws.Cells[dataRow, 7].Value = itemDataHeader.S_CONFLICT_TI;
        //                    ws.Column(7).AutoFit();
        //                    ws.Cells[dataRow, 8].Value = itemDataHeader.S_NRO_CONFLICT_TI;
        //                    ws.Column(8).AutoFit();
        //                    ws.Cells[dataRow, 9].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(9).AutoFit();
        //                    ws.Cells[dataRow, 10].Value = itemDataHeader.S_CONFLICT_NOTI;
        //                    ws.Column(10).AutoFit();
        //                    ws.Cells[dataRow, 11].Value = itemDataHeader.S_NRO_CONFLICT_NOTI;
        //                    ws.Column(11).AutoFit();
        //                    ws.Cells[dataRow, 12].Value = itemDataHeader.S_ANEXO_TRANSACTION;
        //                    ws.Column(12).AutoFit();
        //                    ws.Cells[dataRow, 13].Value = itemDataHeader.S_FLAG_TRANSACTION;
        //                    ws.Column(13).AutoFit();
        //                    ws.Cells[dataRow, 14].Value = itemDataHeader.S_NRO_USER_TRANSACTION;
        //                    ws.Column(14).AutoFit();
        //                    ws.Cells[dataRow, 15].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    ws.Column(15).AutoFit();
        //                    //ws.Cells[dataRow, 16].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    //ws.Column(16).AutoFit();
        //                    //ws.Cells[dataRow, 6].Value = itemDataHeader.S_NEW_FLAG;
        //                    //ws.Column(6).AutoFit();
        //                    dataRow += 1;
        //                }

        //                #endregion Data
        //            }
        //            else if (Convert.ToInt32(item.Split(',')[2]) == 1)
        //            {
        //                #region SubHeader

        //                if (sublistHeaders.Count != 0)
        //                {
        //                    var numCombinacion = int.Parse(item.Split(',')[0].Split('.')[0].Substring(item.Split(',')[0].IndexOf(".") - 2)) - 1;
        //                    int subheaderColumns = 1;
        //                    foreach (var subitem in sublistHeaders)
        //                    {
        //                        ws.Cells[1, subheaderColumns].Style.Font.Bold = true;
        //                        ws.Cells[1, subheaderColumns].Style.Font.Size = 12;
        //                        ws.Cells[1, subheaderColumns].Value = subitem;
        //                        ws.Cells[1, subheaderColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        ws.Cells[1, subheaderColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                        ws.Column(subheaderColumns).AutoFit();
        //                        //En la septima cabecera pregunto si es Automática para seguir o no cargando la segunda parte de las cabeceras
        //                        if (subheaderColumns == 7)
        //                        {
        //                            if (sublistData[numCombinacion].S_TYPE_ACTIVITY.IndexOf("Manual") > -1)
        //                            {
        //                                break;
        //                            }
        //                        }
        //                        subheaderColumns += 1;
        //                    }
        //                    using (ExcelRange subrng = ws.Cells[1, 1, 1, subheaderColumns - 1])
        //                    {
        //                        subrng.Style.Font.Bold = true;
        //                        subrng.Style.Font.Size = 10;
        //                        subrng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    }
        //                }

        //                #endregion SubHeader

        //                #region SubData

        //                int dataRow = 2;
        //                foreach (var subitem in sublistData)
        //                {
        //                    foreach (var itemm in subitem.ListReportConflictUserActivity)
        //                    {
        //                        if (itemm.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                        {
        //                            ws.Cells[dataRow, 1].Value = itemm.S_USER_NAME1;
        //                            ws.Column(1).AutoFit();
        //                            ws.Cells[dataRow, 2].Value = itemm.S_DESCRIPTION1;
        //                            ws.Column(2).AutoFit();
        //                            ws.Cells[dataRow, 3].Value = itemm.S_BOOK1;
        //                            ws.Column(3).AutoFit();
        //                            ws.Cells[dataRow, 4].Value = itemm.S_PROYECTO1;
        //                            ws.Column(4).AutoFit();
        //                            ws.Cells[dataRow, 5].Value = itemm.S_RESPONSIBILITY_NAME1;
        //                            ws.Column(5).AutoFit();
        //                            ws.Cells[dataRow, 6].Value = itemm.S_TI1;
        //                            ws.Column(6).AutoFit();
        //                            ws.Cells[dataRow, 7].Value = itemm.S_NUM_DOC1;
        //                            ws.Column(7).AutoFit();
        //                            if (subitem.S_TYPE_ACTIVITY.IndexOf("Manual") == -1)//si es automatica
        //                            {
        //                                ws.Cells[dataRow, 8].Value = itemm.S_USER_NAME2;
        //                                ws.Column(8).AutoFit();
        //                                ws.Cells[dataRow, 9].Value = itemm.S_DESCRIPTION2;
        //                                ws.Column(9).AutoFit();
        //                                ws.Cells[dataRow, 10].Value = itemm.S_BOOK2;
        //                                ws.Column(10).AutoFit();
        //                                ws.Cells[dataRow, 11].Value = itemm.S_PROYECTO2;
        //                                ws.Column(11).AutoFit();
        //                                ws.Cells[dataRow, 12].Value = itemm.S_RESPONSIBILITY_NAME2;
        //                                ws.Column(12).AutoFit();
        //                                ws.Cells[dataRow, 13].Value = itemm.S_TI2;
        //                                ws.Column(13).AutoFit();
        //                                ws.Cells[dataRow, 14].Value = itemm.S_NUM_DOC2;
        //                                ws.Column(14).AutoFit();
        //                            }
        //                            dataRow += 1;
        //                        }
        //                    }
        //                }

        //                #endregion SubData
        //            }
        //            else if (Convert.ToInt32(item.Split(',')[2]) == 2)
        //            {
        //                #region Header Transaction

        //                int subheaderColumns = 1;
        //                foreach (var itemTrans in sublistData)
        //                {
        //                    if (itemTrans.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                    {
        //                        foreach (var itemHeadTras in itemTrans.ListCombinatedSectionActivity)
        //                        {
        //                            ws.Cells[1, subheaderColumns].Style.Font.Bold = true;
        //                            ws.Cells[1, subheaderColumns].Style.Font.Size = 12;
        //                            ws.Cells[1, subheaderColumns].Value = itemHeadTras.NAME;
        //                            ws.Cells[1, subheaderColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                            ws.Cells[1, subheaderColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                            ws.Column(subheaderColumns).AutoFit();
        //                            subheaderColumns += 1;
        //                        }
        //                    }
        //                }

        //                using (ExcelRange subrng = ws.Cells[1, 1, 1, subheaderColumns - 1])
        //                {
        //                    subrng.Style.Font.Bold = true;
        //                    subrng.Style.Font.Size = 10;
        //                    subrng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    subrng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    subrng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                }

        //                #endregion Header Transaction

        //                #region Body

        //                int dataRow = 2;
        //                foreach (var itemTrans in sublistData)
        //                {
        //                    if (itemTrans.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                    {
        //                        foreach (var itemTrans_ in itemTrans.ListCombinatedSectionGeneric)
        //                        {
        //                            ws.Cells[dataRow, 1].Value = itemTrans_.COLUMNA1; ws.Column(1).AutoFit();
        //                            ws.Cells[dataRow, 2].Value = itemTrans_.COLUMNA2; ws.Column(2).AutoFit();
        //                            ws.Cells[dataRow, 3].Value = itemTrans_.COLUMNA3; ws.Column(3).AutoFit();
        //                            ws.Cells[dataRow, 4].Value = itemTrans_.COLUMNA4; ws.Column(4).AutoFit();
        //                            ws.Cells[dataRow, 5].Value = itemTrans_.COLUMNA5; ws.Column(5).AutoFit();
        //                            ws.Cells[dataRow, 6].Value = itemTrans_.COLUMNA6; ws.Column(6).AutoFit();
        //                            ws.Cells[dataRow, 7].Value = itemTrans_.COLUMNA7; ws.Column(7).AutoFit();
        //                            ws.Cells[dataRow, 8].Value = itemTrans_.COLUMNA8; ws.Column(8).AutoFit();
        //                            ws.Cells[dataRow, 9].Value = itemTrans_.COLUMNA9; ws.Column(9).AutoFit();
        //                            ws.Cells[dataRow, 10].Value = itemTrans_.COLUMNA10; ws.Column(10).AutoFit();
        //                            ws.Cells[dataRow, 11].Value = itemTrans_.COLUMNA11; ws.Column(11).AutoFit();
        //                            ws.Cells[dataRow, 12].Value = itemTrans_.COLUMNA12; ws.Column(12).AutoFit();
        //                            ws.Cells[dataRow, 13].Value = itemTrans_.COLUMNA13; ws.Column(13).AutoFit();
        //                            ws.Cells[dataRow, 14].Value = itemTrans_.COLUMNA14; ws.Column(14).AutoFit();
        //                            ws.Cells[dataRow, 15].Value = itemTrans_.COLUMNA15; ws.Column(15).AutoFit();
        //                            ws.Cells[dataRow, 16].Value = itemTrans_.COLUMNA16; ws.Column(16).AutoFit();
        //                            dataRow += 1;
        //                        }
        //                    }
        //                }
        //                #endregion Body
        //            }
        //        }
        //        bytes = pck.GetAsByteArray();
        //    }
        //    return bytes;
        //}
        //public static byte[] CrearArchivoExcelActivoFijo(List<string> sheet, List<string> listHeaders = null, List<string> ListUsuarioSRA = null, List<string> sublistHeaders = null, List<string> ListUsuarioAMT = null, List<string> ListUsuarioSISME = null, List<CriticalActivity_CombinatedActivity> sublistData = null)
        //{
        //    byte[] bytes;
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        foreach (var item in sheet)
        //        {
        //            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(item.Split(',')[0].ToString());

        //            if (Convert.ToInt32(item.Split(',')[2]) == 0)
        //            {
        //                #region Header
        //                if (listHeaders.Count != 0)
        //                {
        //                    int headerColumns = 1;


        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[1, headerColumns].Value = string.Empty;
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }
        //                    headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {
        //                        ws.Cells[2, headerColumns].Value = string.Empty;
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }
        //                    headerColumns = 1;

        //                    foreach (var Hitem in listHeaders)
        //                    {

        //                        ws.Cells[3, headerColumns].Style.Font.Bold = true;
        //                        ws.Cells[3, headerColumns].Style.Font.Size = 12;
        //                        ws.Cells[3, headerColumns].Value = Hitem;
        //                        ws.Cells[3, headerColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                        ws.Cells[3, headerColumns].Style.WrapText = true;
        //                        ws.Cells[3, headerColumns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                        ws.Cells[3, headerColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                        ws.Column(headerColumns).AutoFit();
        //                        headerColumns += 1;
        //                    }


        //                    ExcelRange rndgA = ws.Cells[1, 6, 1, 11];
        //                    rndgA.Merge = true;
        //                    rndgA.Value = Constantes.NamberHeader.UsuarioConflicto;
        //                    rndgA.Style.Font.Bold = true;
        //                    rndgA.Style.Font.Size = 10;
        //                    rndgA.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgA.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgA.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgA.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgA.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgA.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));


        //                    ExcelRange rndgB = ws.Cells[2, 6, 2, 8];
        //                    rndgB.Merge = true;
        //                    rndgB.Value = Constantes.NamberHeader.TI;
        //                    rndgB.Style.Font.Bold = true;
        //                    rndgB.Style.Font.Size = 10;
        //                    rndgB.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgB.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgB.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgB.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgB.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgB.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    ExcelRange rndgC = ws.Cells[2, 9, 2, 11];
        //                    rndgC.Merge = true;
        //                    rndgC.Value = Constantes.NamberHeader.NOTI;
        //                    rndgC.Style.Font.Bold = true;
        //                    rndgC.Style.Font.Size = 10;
        //                    rndgC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));


        //                    ExcelRange rndgCC = ws.Cells[2, 12, 2, 15];
        //                    rndgCC.Merge = true;
        //                    rndgCC.Value = Constantes.NamberHeader.Transacion;
        //                    rndgCC.Style.Font.Bold = true;
        //                    rndgCC.Style.Font.Size = 10;
        //                    rndgCC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rndgCC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rndgCC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    rndgCC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    rndgCC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    rndgCC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                    using (ExcelRange rng = ws.Cells[3, 1, 3, headerColumns - 1])
        //                    {
        //                        rng.Style.Font.Bold = true;
        //                        rng.Style.Font.Size = 10;
        //                        rng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                        rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                        rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                    }
        //                }
        //                #endregion

        //                #region Data
        //                int dataRow = 4;
        //                foreach (var itemDataHeader in sublistData)
        //                {
        //                    ws.Cells[dataRow, 1].Value = itemDataHeader.S_COMPANY_NAME;
        //                    ws.Column(1).AutoFit();
        //                    ws.Cells[dataRow, 2].Value = itemDataHeader.S_PROCESS_NAME;
        //                    ws.Column(2).AutoFit();
        //                    ws.Cells[dataRow, 3].Value = itemDataHeader.S_ACTIVITY_NAME;
        //                    ws.Column(3).Width = 43;
        //                    ws.Cells[dataRow, 4].Value = itemDataHeader.S_CODE;
        //                    ws.Column(4).AutoFit();
        //                    ws.Cells[dataRow, 5].Value = itemDataHeader.S_TYPE_ACTIVITY;
        //                    ws.Column(5).AutoFit();
        //                    ws.Cells[dataRow, 6].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(6).AutoFit();
        //                    ws.Cells[dataRow, 7].Value = itemDataHeader.S_CONFLICT_TI;
        //                    ws.Column(7).AutoFit();
        //                    ws.Cells[dataRow, 8].Value = itemDataHeader.S_NRO_CONFLICT_TI;
        //                    ws.Column(8).AutoFit();
        //                    ws.Cells[dataRow, 9].Value = itemDataHeader.S_ANEXO;
        //                    ws.Column(9).AutoFit();
        //                    ws.Cells[dataRow, 10].Value = itemDataHeader.S_CONFLICT_NOTI;
        //                    ws.Column(10).AutoFit();
        //                    ws.Cells[dataRow, 11].Value = itemDataHeader.S_NRO_CONFLICT_NOTI;
        //                    ws.Column(11).AutoFit();
        //                    ws.Cells[dataRow, 12].Value = itemDataHeader.S_ANEXO_TRANSACTION;
        //                    ws.Column(12).AutoFit();
        //                    ws.Cells[dataRow, 13].Value = itemDataHeader.S_FLAG_TRANSACTION;
        //                    ws.Column(13).AutoFit();
        //                    ws.Cells[dataRow, 14].Value = itemDataHeader.S_NRO_USER_TRANSACTION;
        //                    ws.Column(14).AutoFit();
        //                    ws.Cells[dataRow, 15].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    ws.Column(15).AutoFit();
        //                    //ws.Cells[dataRow, 16].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                    //ws.Column(16).AutoFit();
        //                    //ws.Cells[dataRow, 6].Value = itemDataHeader.S_NEW_FLAG;
        //                    //ws.Column(6).AutoFit();
        //                    dataRow += 1;
        //                }
        //                #endregion
        //            }
        //            else if(item.Split(',')[1].ToString() == "00")
        //            {
        //                ws.Cells[2, 2].Value = Constantes.OpeningAF.Apertura_AF;
        //                ws.Column(2).AutoFit();
        //            }
        //            else if (Convert.ToInt32(item.Split(',')[2]) == 1)
        //            {
        //                #region SubHeader


        //                foreach (var itemType in sublistData)
        //                {

        //                    if (itemType.S_TYPE_ACTIVITY == Constantes.TypeActivity.MANUAL)
        //                    {
        //                        if (itemType.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                        {
        //                            ws = Type_Activity_Manual_UserSRA(ws, ListUsuarioSRA, itemType);
        //                            break;
        //                        }
        //                    }
        //                    else if (itemType.S_TYPE_ACTIVITY == Constantes.TypeActivity.AUTOMATICO_MANUAL)
        //                    {
        //                        if (itemType.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                        {
        //                        ws = Type_Activity_AutomaticoManual(ws, ListUsuarioSRA, sublistHeaders, ListUsuarioSISME,ListUsuarioAMT, itemType);
        //                        break;
        //                        }
        //                    }
        //                }

        //                #endregion

        //                #region SubData

        //                #endregion
        //            }
        //        }
        //        bytes = pck.GetAsByteArray();
        //    }
        //    return bytes;
        //}
        //public static byte[] CrearArchivoExcelIngresos(List<string> sheet, List<string> listHeaders = null, List<string> ListUsuarioSRA = null, List<string> sublistHeaders = null, List<string> ListUsuarioAMT = null, List<string> ListUsuarioSISME = null, List<CriticalActivity_CombinatedActivity> sublistData = null)
        //{
        //    byte[] bytes;
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheet.First().Split(',')[0].ToString());

        //        if (Convert.ToInt32(sheet.First().Split(',')[2]) == 0)
        //        {
        //            #region Header
        //            if (listHeaders.Count != 0)
        //            {
        //                int headerColumns = 1;


        //                foreach (var Hitem in listHeaders)
        //                {
        //                    ws.Cells[1, headerColumns].Value = string.Empty;
        //                    ws.Column(headerColumns).AutoFit();
        //                    headerColumns += 1;
        //                }
        //                headerColumns = 1;

        //                foreach (var Hitem in listHeaders)
        //                {
        //                    ws.Cells[2, headerColumns].Value = string.Empty;
        //                    ws.Column(headerColumns).AutoFit();
        //                    headerColumns += 1;
        //                }
        //                headerColumns = 1;

        //                foreach (var Hitem in listHeaders)
        //                {

        //                    ws.Cells[3, headerColumns].Style.Font.Bold = true;
        //                    ws.Cells[3, headerColumns].Style.Font.Size = 12;
        //                    ws.Cells[3, headerColumns].Value = Hitem;
        //                    ws.Cells[3, headerColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                    ws.Cells[3, headerColumns].Style.WrapText = true;
        //                    ws.Cells[3, headerColumns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                    ws.Cells[3, headerColumns].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));
        //                    ws.Column(headerColumns).AutoFit();
        //                    headerColumns += 1;
        //                }


        //                ExcelRange rndgA = ws.Cells[1, 6, 1, 11];
        //                rndgA.Merge = true;
        //                rndgA.Value = Constantes.NamberHeader.UsuarioConflicto;
        //                rndgA.Style.Font.Bold = true;
        //                rndgA.Style.Font.Size = 10;
        //                rndgA.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                rndgA.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                rndgA.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                rndgA.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                rndgA.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                rndgA.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));


        //                ExcelRange rndgB = ws.Cells[2, 6, 2, 8];
        //                rndgB.Merge = true;
        //                rndgB.Value = Constantes.NamberHeader.TI;
        //                rndgB.Style.Font.Bold = true;
        //                rndgB.Style.Font.Size = 10;
        //                rndgB.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                rndgB.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                rndgB.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                rndgB.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                rndgB.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                rndgB.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                ExcelRange rndgC = ws.Cells[2, 9, 2, 11];
        //                rndgC.Merge = true;
        //                rndgC.Value = Constantes.NamberHeader.NOTI;
        //                rndgC.Style.Font.Bold = true;
        //                rndgC.Style.Font.Size = 10;
        //                rndgC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                rndgC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                rndgC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                rndgC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                rndgC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                rndgC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));


        //                ExcelRange rndgCC = ws.Cells[2, 12, 2, 15];
        //                rndgCC.Merge = true;
        //                rndgCC.Value = Constantes.NamberHeader.Transacion;
        //                rndgCC.Style.Font.Bold = true;
        //                rndgCC.Style.Font.Size = 10;
        //                rndgCC.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                rndgCC.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                rndgCC.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                rndgCC.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //                rndgCC.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                rndgCC.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#f0f3f5"));

        //                using (ExcelRange rng = ws.Cells[3, 1, 3, headerColumns - 1])
        //                {
        //                    rng.Style.Font.Bold = true;
        //                    rng.Style.Font.Size = 10;
        //                    rng.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        //                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                }
        //            }
        //            #endregion

        //            #region Data
        //            int dataRow = 4;
        //            foreach (var itemDataHeader in sublistData)
        //            {
        //                ws.Cells[dataRow, 1].Value = itemDataHeader.S_COMPANY_NAME;
        //                ws.Column(1).AutoFit();
        //                ws.Cells[dataRow, 2].Value = itemDataHeader.S_PROCESS_NAME;
        //                ws.Column(2).AutoFit();
        //                ws.Cells[dataRow, 3].Value = itemDataHeader.S_ACTIVITY_NAME;
        //                ws.Column(3).Width = 43;
        //                ws.Cells[dataRow, 4].Value = itemDataHeader.S_CODE;
        //                ws.Column(4).AutoFit();
        //                ws.Cells[dataRow, 5].Value = itemDataHeader.S_TYPE_ACTIVITY;
        //                ws.Column(5).AutoFit();
        //                ws.Cells[dataRow, 6].Value = itemDataHeader.S_ANEXO;
        //                ws.Column(6).AutoFit();
        //                ws.Cells[dataRow, 7].Value = itemDataHeader.S_CONFLICT_TI;
        //                ws.Column(7).AutoFit();
        //                ws.Cells[dataRow, 8].Value = itemDataHeader.S_NRO_CONFLICT_TI;
        //                ws.Column(8).AutoFit();
        //                ws.Cells[dataRow, 9].Value = itemDataHeader.S_ANEXO;
        //                ws.Column(9).AutoFit();
        //                ws.Cells[dataRow, 10].Value = itemDataHeader.S_CONFLICT_NOTI;
        //                ws.Column(10).AutoFit();
        //                ws.Cells[dataRow, 11].Value = itemDataHeader.S_NRO_CONFLICT_NOTI;
        //                ws.Column(11).AutoFit();
        //                ws.Cells[dataRow, 12].Value = itemDataHeader.S_ANEXO_TRANSACTION;
        //                ws.Column(12).AutoFit();
        //                ws.Cells[dataRow, 13].Value = itemDataHeader.S_FLAG_TRANSACTION;
        //                ws.Column(13).AutoFit();
        //                ws.Cells[dataRow, 14].Value = itemDataHeader.S_NRO_USER_TRANSACTION;
        //                ws.Column(14).AutoFit();
        //                ws.Cells[dataRow, 15].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                ws.Column(15).AutoFit();
        //                //ws.Cells[dataRow, 16].Value = itemDataHeader.S_NRO_TRANSACTION;
        //                //ws.Column(16).AutoFit();
        //                //ws.Cells[dataRow, 6].Value = itemDataHeader.S_NEW_FLAG;
        //                //ws.Column(6).AutoFit();
        //                dataRow += 1;
        //            }
        //            #endregion
        //        }
        //        sheet.Remove(sheet.First());
        //        foreach (var item in sheet)
        //        {
        //            ExcelWorksheet ws2 = pck.Workbook.Worksheets.Add(item.Split(',')[0].ToString());

        //            if (Convert.ToInt32(item.Split(',')[2]) == 1)
        //            {
        //                #region SubHeader

        //                foreach (var itemType in sublistData)
        //                {
        //                    if (itemType.N_ID_COMBINATED_ACTIVITY == Convert.ToInt32(item.Split(',')[1]))
        //                    {
        //                        if (itemType.S_TYPE_ACTIVITY == Constantes.TypeActivity.MANUAL)
        //                        {
        //                            ws2 = Type_Activity_Manual_UserSRA(ws2, ListUsuarioSRA, itemType);
        //                            break;
        //                        }
        //                        else if (itemType.S_TYPE_ACTIVITY == Constantes.TypeActivity.AUTOMATICO_MANUAL)
        //                        {
        //                            ws2 = Type_Activity_AutomaticoManual(ws2, ListUsuarioSRA, sublistHeaders, ListUsuarioSISME, ListUsuarioAMT, itemType);
        //                            break;
        //                        }
        //                        else if (itemType.S_TYPE_ACTIVITY == Constantes.TypeActivity.AUTOMATICO)
        //                        {
        //                            ws2 = Type_Activity_Automatico(ws2, sublistHeaders, itemType);
        //                            break;
        //                        }
        //                    }
        //                }

        //                #endregion

        //                #region SubData

        //                #endregion
        //            }
        //        }
        //        bytes = pck.GetAsByteArray();
        //    }
        //    return bytes;
        //}            
    }
}
