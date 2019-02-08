using System.Data;

namespace Cosapi.ElCosapino.Dominio
{
    public class PaginateParams
    {
        /// <summary>
        /// 
        /// </summary>
        public DataTable Filters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPaginate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RowsPerPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalRows { get; set; }

        public int UsuarioId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SortDirection { get; set; }

        public string Activo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TypeGrid { get; set; }

        public string Desde { get; set; }

        public string Hasta { get; set; }
    }
}
