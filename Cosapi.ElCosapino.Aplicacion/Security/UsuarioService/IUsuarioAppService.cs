using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.UsuarioService
{
    public interface IUsuarioAppService
    {
        List<Usuario> List_Usuario_Paginate(PaginateParams paginateParams);

        DataTable DT_Usuario();

        Usuario Update(Usuario item);

        Usuario UpdateDevice(Usuario item);

        Usuario UpdateEstado(Usuario item);

        Usuario UpdatePassword(Usuario item);

        Usuario Insert(Usuario item);

        Usuario Insert_RedesSociales(Usuario item);

        List<DDLOptionsText> ListarCargo(int N_IDUnidad);

        List<DDLOptionsText> ListarEspecialidad(int N_IDUnidad);

        List<DDLOptionsText> ListarPerfil(int N_IDUnidad);

        Usuario UsuarioPorID(int IDUsuario);

        Usuario UsuarioPorCredenciales(string CorreoElectronico, string Clave);

        Usuario UsuarioPorCorreo(string CorreoElectronico);

        Usuario UsuarioPorCorreo_Sin_RedesSociales(string CorreoElectronico);

        Usuario Delete(Usuario item);

        Usuario UsuarioPorCorreoRS(string CorreoElectronico, string Key);
    }
}
