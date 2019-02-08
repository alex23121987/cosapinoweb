using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.InterfaceWebAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Infrastructure.Data.Base;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Repositorio.Repositories
{
    public class InterfaceWebRepository : IInterfaceWebRepository
    {
        readonly ILogRepository _LogRepository = new LogRepository();

        public InterfaceWebRepository(){}

        public List<InterfaceWeb> List_InterfaceWeb_Paginate(PaginateParams paginateParams,int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWeb_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Orden = DataConvert.ToInt32(dataReader["N_Orden"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWeb_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }          
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebNoticia_Trimestre()
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticia_Sellst_Trimestre", CommandType.StoredProcedure);
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();                        
                        objInterfaceWeb.FiltroNoticia_Anio = DataConvert.ToString(dataReader["Anio"]);
                        objInterfaceWeb.FiltroNoticia_Descripcion = DataConvert.ToString(dataReader["Descripcion"]);
                        objInterfaceWeb.FiltroNoticia_Desde = DataConvert.ToString(dataReader["FechaInicio"]);
                        objInterfaceWeb.FiltroNoticia_Hasta = DataConvert.ToString(dataReader["FechaFin"]);                        
                        list.Add(objInterfaceWeb);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWeb_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebNoticia_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticia_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Orden = DataConvert.ToInt32(dataReader["N_Orden"]);
                        objInterfaceWeb.Destacado = DataConvert.ToInt32(dataReader["N_Destacado"]);
                        objInterfaceWeb.FechaPublicacion = DataConvert.ToString(dataReader["S_FechaPublicacion"]);
                        objInterfaceWeb.Dia = DataConvert.ToString(dataReader["S_Dia"]);
                        objInterfaceWeb.Mes = DataConvert.ToString(dataReader["S_Mes"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebNoticia_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaFiltro_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticia_Sellst_Paginate_Filtro", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("Desde", SqlDbType.VarChar, paginateParams.Desde),
                SQLServer.CreateParameter("Hasta", SqlDbType.VarChar, paginateParams.Hasta),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Orden = DataConvert.ToInt32(dataReader["N_Orden"]);
                        objInterfaceWeb.Destacado = DataConvert.ToInt32(dataReader["N_Destacado"]);
                        objInterfaceWeb.FechaPublicacion = DataConvert.ToString(dataReader["S_FechaPublicacion"]);
                        objInterfaceWeb.Dia = DataConvert.ToString(dataReader["S_Dia"]);
                        objInterfaceWeb.Mes = DataConvert.ToString(dataReader["S_Mes"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebNoticia_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaDestacado_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticiaDestacado_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Orden = DataConvert.ToInt32(dataReader["N_Orden"]);
                        objInterfaceWeb.Destacado = DataConvert.ToInt32(dataReader["N_Destacado"]);
                        objInterfaceWeb.FechaPublicacion = DataConvert.ToString(dataReader["S_FechaPublicacion"]);
                        objInterfaceWeb.Dia = DataConvert.ToString(dataReader["S_Dia"]);
                        objInterfaceWeb.Mes = DataConvert.ToString(dataReader["S_Mes"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebNoticiaDestacado_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebNoticiaUltimos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticiaUltimos_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Orden = DataConvert.ToInt32(dataReader["N_Orden"]);
                        objInterfaceWeb.Destacado = DataConvert.ToInt32(dataReader["N_Destacado"]);
                        objInterfaceWeb.FechaPublicacion = DataConvert.ToString(dataReader["S_FechaPublicacion"]);
                        objInterfaceWeb.Dia = DataConvert.ToString(dataReader["S_Dia"]);
                        objInterfaceWeb.Mes = DataConvert.ToString(dataReader["S_Mes"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebNoticiaUltimos_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }          
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebOficina_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebOficina_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Latitud = DataConvert.ToString(dataReader["S_Latitud"]);
                        objInterfaceWeb.Longitud = DataConvert.ToString(dataReader["S_Longitud"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log { IDCategoria = 2, UsuarioCreacion = "", Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebOficina_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebLineaTiempo_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion,int IDInterface)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebLineaTiempo_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("IDInterface", SqlDbType.Int, IDInterface),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebLineaTiempo_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString() };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebLineaTiempoProyecto_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebLineaTiempoProyecto_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("IDInterface", SqlDbType.Int, IDInterface),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebLineaTiempoProyecto_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }            
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebProgramasConFotos_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebProgramaFotos_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("IDInterface", SqlDbType.Int, IDInterface),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebProgramasConFotos_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }           
            return list;
        }

        public List<InterfaceWeb> List_InterfaceWebProgramasFotosGaleria_Paginate(PaginateParams paginateParams, int IDModulo, int IDSeccion, int IDInterface)
        {
            var list = new List<InterfaceWeb>();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebProgramaGaleriaFotos_Sellst_Paginate", CommandType.StoredProcedure,
                SQLServer.CreateParameter("IDModulo", SqlDbType.Int, IDModulo),
                SQLServer.CreateParameter("IDSeccion", SqlDbType.Int, IDSeccion),
                SQLServer.CreateParameter("IDInterface", SqlDbType.Int, IDInterface),
                SQLServer.CreateParameter("SortDirection", SqlDbType.VarChar, paginateParams.SortDirection),
                SQLServer.CreateParameter("SortColumn", SqlDbType.VarChar, paginateParams.SortColumn),
                SQLServer.CreateParameter("PageIndex", SqlDbType.Int, paginateParams.PageIndex),
                SQLServer.CreateParameter("RowsPerPage", SqlDbType.Int, paginateParams.RowsPerPage),
                SQLServer.CreateParameter("Paginate", SqlDbType.Bit, paginateParams.IsPaginate),
                SQLServer.CreateParameter("Activo", SqlDbType.VarChar, paginateParams.Activo),
                SQLServer.CreateParameter("Filters", SqlDbType.Structured, paginateParams.Filters));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        var objInterfaceWeb = new InterfaceWeb();
                        paginateParams.TotalRows = DataConvert.ToInt(dataReader["List_TotalRegistros"]);
                        objInterfaceWeb.IDInterfaceWeb = DataConvert.ToInt(dataReader["N_IDInterface"]);
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Estado = DataConvert.ToString(dataReader["S_Estado"]).Equals("A") ? "Activo" : "Inactivo";
                        list.Add(objInterfaceWeb);
                    }
                }                
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebProgramasFotosGaleria_Paginate(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }          
            return list;
        }

        public InterfaceWeb List_InterfaceWeb_Unique(InterfaceWeb item)
        {
            var objInterfaceWeb = new InterfaceWeb();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWeb_Sel_Unique", CommandType.StoredProcedure,
                SQLServer.CreateParameter("N_IDModulo", SqlDbType.VarChar, item.IDModulo),
                SQLServer.CreateParameter("N_IDSeccion", SqlDbType.VarChar, item.IDSeccion));
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {                        
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWeb_Unique(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objInterfaceWeb;
        }

        public InterfaceWeb List_InterfaceWebNoticia_Unique(InterfaceWeb item)
        {
            var objInterfaceWeb = new InterfaceWeb();
            try
            {
                SQLServer.OpenConection();
                SQLServer.CreateCommand("up_InterfaceWebNoticia_Sel_Unique", CommandType.StoredProcedure,
                SQLServer.CreateParameter("N_IDModulo", SqlDbType.VarChar, item.IDModulo),
                SQLServer.CreateParameter("N_IDSeccion", SqlDbType.VarChar, item.IDSeccion),
                SQLServer.CreateParameter("N_IDInterfaceWeb", SqlDbType.VarChar, item.IDInterfaceWeb)
                );
                using (var dataReader = SQLServer.GetDataReader(CommandBehavior.CloseConnection))
                {
                    while (dataReader.Read())
                    {
                        objInterfaceWeb.Titulo = DataConvert.ToString(dataReader["S_Titulo"]);
                        objInterfaceWeb.SubTitulo = DataConvert.ToString(dataReader["S_SubTitulo"]);
                        objInterfaceWeb.Imagen = DataConvert.ToString(dataReader["S_Imagen"]);
                        objInterfaceWeb.Dia = DataConvert.ToString(dataReader["S_Dia"]);
                        objInterfaceWeb.Mes = DataConvert.ToString(dataReader["S_Mes"]);
                    }
                }
                SQLServer.CloseConection();
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:List_InterfaceWebNoticia_Unique(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            finally
            {
                SQLServer.CloseConection();
            }
            return objInterfaceWeb;
        }

        public InterfaceWeb Update(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWeb_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Update(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb UpdateNoticia(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebNoticia_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_FechaPublicacion", DbType.String, item.FechaPublicacion);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@N_Destacado", DbType.Int32, item.Destacado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:UpdateNoticia(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb UpdateDireccion(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebDireccion_Upd");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen.Trim());
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);                                
                oDatabase.AddInParameter(oDbCommand, "@S_Latitud", DbType.String, item.Latitud);
                oDatabase.AddInParameter(oDbCommand, "@S_Longitud", DbType.String, item.Longitud);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:UpdateDireccion(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb Insert(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWeb_Ins");
                oDatabase.AddInParameter(oDbCommand, "@N_IDModulo", DbType.Int32, item.IDModulo);
                oDatabase.AddInParameter(oDbCommand, "@N_IDSeccion", DbType.Int32, item.IDSeccion);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo);
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo);
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen);
                oDatabase.AddInParameter(oDbCommand, "@N_IDRelacion", DbType.Int32, item.IDRelacion);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Insert(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb InsertOficina(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebOficina_Ins");
                oDatabase.AddInParameter(oDbCommand, "@N_IDModulo", DbType.Int32, item.IDModulo);
                oDatabase.AddInParameter(oDbCommand, "@N_IDSeccion", DbType.Int32, item.IDSeccion);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo);
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo);
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen);
                oDatabase.AddInParameter(oDbCommand, "@S_Latitud", DbType.String, item.Latitud);
                oDatabase.AddInParameter(oDbCommand, "@S_Longitud", DbType.String, item.Longitud);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:InsertOficina(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb InsertUnique(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWeb_Ins_Unique");
                oDatabase.AddInParameter(oDbCommand, "@N_IDModulo", DbType.Int32, item.IDModulo);
                oDatabase.AddInParameter(oDbCommand, "@N_IDSeccion", DbType.Int32, item.IDSeccion);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String,item.Titulo);
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo);
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:InsertUnique(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb InsertNoticia(InterfaceWeb item)
        {
            var objResult = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebNoticias_Ins");
                oDatabase.AddInParameter(oDbCommand, "@N_IDModulo", DbType.Int32, item.IDModulo);
                oDatabase.AddInParameter(oDbCommand, "@N_IDSeccion", DbType.Int32, item.IDSeccion);
                oDatabase.AddInParameter(oDbCommand, "@S_Titulo", DbType.String, item.Titulo);
                oDatabase.AddInParameter(oDbCommand, "@S_SubTitulo", DbType.String, item.SubTitulo);
                oDatabase.AddInParameter(oDbCommand, "@S_Imagen", DbType.String, item.Imagen);
                oDatabase.AddInParameter(oDbCommand, "@N_IDRelacion", DbType.Int32, item.IDRelacion);
                oDatabase.AddInParameter(oDbCommand, "@N_Destacado", DbType.Int32, item.Destacado);
                oDatabase.AddInParameter(oDbCommand, "@N_Orden", DbType.Int32, item.Orden);
                oDatabase.AddInParameter(oDbCommand, "@S_FechaPublicacion", DbType.String, item.FechaPublicacion);
                oDatabase.AddInParameter(oDbCommand, "@S_Estado", DbType.String, item.Estado);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioCreacion", DbType.String, item.UsuarioCreacion.Trim());
                objResult.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:InsertNoticia(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return objResult;
        }

        public InterfaceWeb Delete(InterfaceWeb item)
        {
            var Resultado = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWeb_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:Delete(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public InterfaceWeb DeleteNoticia(InterfaceWeb item)
        {
            var Resultado = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebNoticia_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:DeleteNoticia(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }

        public InterfaceWeb DeleteDireccion(InterfaceWeb item)
        {
            var Resultado = new InterfaceWeb();
            try
            {
                var oDatabase = DatabaseFactory.CreateDatabase();
                var oDbCommand = oDatabase.GetStoredProcCommand("up_InterfaceWebDireccion_Del");
                oDatabase.AddInParameter(oDbCommand, "@N_IDInterfaceWeb", DbType.Int32, item.IDInterfaceWeb);
                oDatabase.AddInParameter(oDbCommand, "@S_UsuarioModificacion", DbType.String, item.UsuarioModificacion);
                Resultado.IDInterfaceWeb = DataConvert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                Log _log = new Log
                {
                    IDCategoria = 2,
                    UsuarioCreacion = "",
                    Mensaje = "Origen:REPOSITORY - Método:DeleteDireccion(Repository InterfaceWeb) - Error:" + ex.GetBaseException().ToString()
                };
                _LogRepository.Insert(_log);
            }
            return Resultado;
        }
    }
}
