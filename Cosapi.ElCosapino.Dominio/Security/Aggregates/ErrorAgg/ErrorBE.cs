using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.ErrorAgg
{
    public class ErrorBE
    {
        public ErrorBE(String StrError, bool Result)
        {
            this.StrError = StrError;
            this.Result = Result;
        }
        public ErrorBE()
        {
            this.Error = false;
            this.Descripcion = String.Empty;
        }

        public ErrorBE(String Descripcion)
        {
            this.Error = true;
            this.Descripcion = Descripcion;
        }

        public bool Error { get; set; }
        public String Descripcion { get; set; }
        public string StrError { get; private set; }
        public bool Result { get; private set; }
    }
}
