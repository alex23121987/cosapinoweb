using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.PurchaseOrderAgg
{
    public class HierarchyApproval
    {

        public string H_USUARIO { get; set; }
        public string H_NOMBRE_USUARIO { get; set; }
        public string H_PROYECTO { get; set; }
        public string H_JERARQUIA { get; set; }
        public string H_FECHA_DESDE { get; set; }
        public string H_FECHA_HASTA { get; set; }
        public string H_POSITION_ID { get; set; }
        public string H_AMOUNT_LIMIT { get; set; }
        public string H_NRO_OC { get; set; }

        public HierarchyApproval()
        {

        }
    }
}
