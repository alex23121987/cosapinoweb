using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.Sica.Dominio.Implementacion.Security.Aggregates.PurchaseOrderAgg
{
    public class ModificationOrder : HierarchyApproval
    {
        public int ID_MODIFICATION_OC { get; set; }
        public int LEDGER_ID { get; set; }
        public string EMPRESA { get; set; }
        public string ORGANIZACION { get; set; }
        public string ORDER_NUM { get; set; }
        public string LAST_UPDATE_DATE { get; set; }
        public string USER_NAME { get; set; }
        public string USER_DESCRIPTION { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string NOTE_TO_VENDOR { get; set; }
        public int LINE_NUM { get; set; }
        public int REVISION_NUM { get; set; }
        public decimal QUANTITY { get; set; }
        public decimal UNIT_PRICE { get; set; }
        public string CANCEL_FLAG { get; set; }
        public string ITEM { get; set; }
        public string ITEM_DESCRIPTION { get; set; }
        public string UOM { get; set; }
        public string PRICE_TYPE { get; set; }
        public string AMOUNT { get; set; }
        public string LATEST_EXTERNAL_FLAG { get; set; }
        public string REV { get; set; }

        public ModificationOrder()
        {

        }

    }
}
