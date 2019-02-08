using Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.LoginService
{
    public interface ILoginAppService
    {
        Usuario Verificar_Usuario(Login usuario);

        Usuario Verificar_Usuario_Por_UserNameValidado(string UserName);

        Usuario Verificar_Usuario_Externo(Login login);

        Administrador Verificar_Usuario_Admin(Login login);

        List<PermisoUsuario> Listar_PermisosUsuario(string S_UserName);
    }
}
