using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio
{
    public class GridDatatableFilter
    {

        public GridDatatableFilter()
        {
            this.NumeroPagina = 0;// PARA QUE FUNCIONE
            this.RegistrosPagina = int.MaxValue;
            this.EstadoRegistro = "1";
        }
        /// <summary>
        /// Pagina solicitada
        /// </summary>
        public int NumeroPagina { get; set; }
        /// <summary>
        /// Registros por Pagina
        /// </summary>
        public int RegistrosPagina { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ColumnaOrden { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TipoOrden { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FiltrosColumna> Filtros { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float NumeroRegistro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TotalRegistro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EstadoRegistro { get; set; }
    }
    public class FiltrosColumna
    {
        public string Columna { get; set; }
        public string Valor { get; set; }
    }

}
