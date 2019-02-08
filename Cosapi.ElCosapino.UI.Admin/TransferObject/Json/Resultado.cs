namespace Cosapi.ElCosapino.UI.Admin.TransferObject.Json
{
    public class Resultado
    {
        public bool EsExito { get; set; }
        public string Mensaje { get; set; }
        public string[] datos { get; set; }
        public string Redirect { get; set; }
        public int Codigo { get; set; }
    }
}