using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.PurchaseOrderAgg
{
    public class PurchaseOrder : ModificationOrder
    {

        #region Propiedades
        public string EMPRESA { get; set; }
        public string ORGANIZACION { get; set; }
        public string NRO_OC { get; set; }
        public double IMPORTE_OC { get; set; }
        public double IMPORTE_OC_SOLES { get; set; }
        public string ESTADO { get; set; }
        public string PROVEEDOR { get; set; }
        public string NOMBRE_USUARIO_CREADOR { get; set; }
        public string USUARIO_CREADOR { get; set; }
        public string ULTIMO_APROBADOR { get; set; }
        public string PERIODO { get; set; }
        public string MONEADA { get; set; }
        public string JERARQUIA { get; set; }
        public string Estado_Revisado { get; set; }


        //historial de procesos
        public int ID_HISTORY_PROCESSES { get; set; }
        public int SEC { get; set; }
        public DateTime FECHA { get; set; }
        public int REV { get; set; }
        public string ACCION { get; set; }
        public string REALIZADO_POR { get; set; }
        public int PO_HEADER_ID { get; set; }
        public string name { get; set; }

        #endregion Propiedades
        public PurchaseOrder()
        {

        }

    }
}
