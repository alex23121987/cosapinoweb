namespace Cosapi.ElCosapino.UI.Admin.Models
{
    public class JsTreeModel
    {
        public string data;
        public JsTreeAttribute attr;
        public JsTreeModel[] children;
    }

    public class JsTreeAttribute
    {
        public string id;
        public bool selected;
    }
}