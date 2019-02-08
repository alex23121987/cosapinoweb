using Cosapi.ElCosapino.Dominio.Security.Aggregates.LoginAgg;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.LoginService
{
    public class LoginAppService : ILoginAppService
    {
        readonly ILoginRepository _LoginRepository;

        public LoginAppService()
        {
            _LoginRepository = new LoginRepository();
        }

        public Usuario Verificar_Usuario(Login login)
        {
            return _LoginRepository.Verificar_Usuario(login);
        }

        public Administrador Verificar_Usuario_Admin(Login login)
        {
            return _LoginRepository.Verificar_Usuario_Admin(login);
        }

        public Usuario Verificar_Usuario_Externo(Login login)
        {
            return _LoginRepository.Verificar_Usuario_Externo(login);
        }

        public Usuario Verificar_Usuario_Por_UserNameValidado(string UserName)
        {
            return _LoginRepository.Verificar_Usuario_Por_UserNameValidado(UserName);
        }

        public List<PermisoUsuario> Listar_PermisosUsuario(string S_UserName)
        {
            return _LoginRepository.Listar_PermisosUsuario(S_UserName);
        }

    }
}
