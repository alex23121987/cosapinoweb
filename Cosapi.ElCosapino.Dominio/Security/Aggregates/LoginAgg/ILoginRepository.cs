using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg
{
    public interface ILoginRepository
    {
        Usuario Verificar_Usuario(Login usuario);

        Administrador Verificar_Usuario_Admin(Login login);

        List<PermisoUsuario> Listar_PermisosUsuario(string S_UserName);

        Usuario Verificar_Usuario_Por_UserNameValidado(string UserName);

        Usuario Verificar_Usuario_Externo(Login login);
    }
}
