using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.DistritoAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.EspecialidadAgg;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg
{
    public class Usuario: EntidadBase
    {
        #region Propiedades       
        public int Resultado { get; set; }
        #endregion

        #region Constructor
        public Usuario()
        {
        }
        #endregion

        public int UsuarioId { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }
        public string NumeroContacto1 { get; set; }
        public string NumeroContacto2 { get; set; }
        public bool EsTrabajador { get; set; }
        public bool TieneEstudios { get; set; }
        public int EspecialidadId { get; set; }
        public int DistritoId { get; set; }
        public int CategoriaId { get; set; }
        public int DepartamentoId { get; set; }
        public int ProvinciaId { get; set; }
        
        public string Estado { get; set; }
        public string strFechaNacimiento { get; set; }
        public string DeviceToken { get; set; }

        public string Origen { get; set; }

        public string UsuarioIDExterno { get; set; }

        public DistritoBE Distrito { get; set; }
        public EspecialidadBE Especialidad { get; set; }

        public string EspecialidadNombre { get; set; }

        public string CategoriaNombre { get; set; }

        public string DepartamentoNombre { get; set; }

        public string Filtro { get; set; }
    }
}
