using System;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg
{
    public class UsuarioBE
    {
        public UsuarioBE()
        {
            this.bitError = false;
            this.strError = String.Empty;
        }

        public UsuarioBE(String strError)
        {
            this.bitError = true;
            this.strError = strError;
        }

        public int UsuarioId { get; set; }
        public String CorreoElectronico { get; set; }
        public int EspecialidadId { get; set; }
        public int CategoriaId { get; set; }
        public String Password { get; set; }
        public String Nombres { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String strFechaNacimiento { get; set; }
        public String DNI { get; set; }
        public String NumeroContacto1 { get; set; }
        public String NumeroContacto2 { get; set; }
        public bool EsTrabajador { get; set; }
        public bool TieneEstudios { get; set; }
        public int DistritoId { get; set; }
        public int ProvinciaId { get; set; }
        public int DepartamentoId { get; set; }
        public bool bitError { get; set; }
        public String strError { get; set; }
        public String DeviceToken { get; set; }
    }
}
