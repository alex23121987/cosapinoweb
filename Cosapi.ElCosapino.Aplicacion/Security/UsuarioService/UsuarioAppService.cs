using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Base;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.UsuarioAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.UsuarioService
{
    public class UsuarioAppService : IUsuarioAppService
    {
        readonly IUsuarioRepository _UsuarioRepository;

        public UsuarioAppService()
        {
            _UsuarioRepository = new UsuarioRepository();
        }

        public List<Usuario> List_Usuario_Paginate(PaginateParams paginateParams)
        {
            return _UsuarioRepository.List_Usuario_Paginate(paginateParams);
        }

        public DataTable DT_Usuario()
        {
            return _UsuarioRepository.DT_Usuario();
        }      

        public Usuario Update(Usuario item)
        {
            return _UsuarioRepository.Update(item);
        }

        public Usuario UpdateDevice(Usuario item)
        {
            return _UsuarioRepository.UpdateDevice(item);
        }

        public Usuario UpdateEstado(Usuario item)
        {
            return _UsuarioRepository.UpdateEstado(item);
        }

        public Usuario UpdatePassword(Usuario item)
        {
            return _UsuarioRepository.UpdatePassword(item);
        }

        public Usuario Insert(Usuario item)
        {
            return _UsuarioRepository.Insert(item);
        }

        public Usuario Insert_RedesSociales(Usuario item)
        {
            return _UsuarioRepository.Insert_RedesSociales(item);
        }

        public Usuario Delete(Usuario item)
        {
            return _UsuarioRepository.Delete(item);
        }
    
        public List<DDLOptionsText> ListarPerfil(int IDUserUnidad)
        {
            return _UsuarioRepository.ListarPerfil( IDUserUnidad);
        }

        public List<DDLOptionsText> ListarEspecialidad(int IDUserUnidad)
        {
            return _UsuarioRepository.ListarEspecialidad( IDUserUnidad);
        }

        public List<DDLOptionsText> ListarCargo(int IDUserUnidad)
        {
            return _UsuarioRepository.ListarCargo( IDUserUnidad);
        }

        public Usuario UsuarioPorID(int IDUsuario)
        {
            return _UsuarioRepository.UsuarioPorID(IDUsuario);
        }

        public Usuario UsuarioPorCredenciales(string CorreoElectronico, string Clave)
        {
            return _UsuarioRepository.UsuarioPorCredenciales(CorreoElectronico, Clave);
        }

        public Usuario UsuarioPorCorreo(string CorreoElectronico)
        {
            return _UsuarioRepository.UsuarioPorCorreo(CorreoElectronico);
        }

        public Usuario UsuarioPorCorreo_Sin_RedesSociales(string CorreoElectronico)
        {
            return _UsuarioRepository.UsuarioPorCorreo_Sin_RedesSociales(CorreoElectronico);
        }    
        
        public Usuario UsuarioPorCorreoRS(string CorreoElectronico, string Key)
        {
            return _UsuarioRepository.UsuarioPorCorreoRS(CorreoElectronico, Key);
        }
    }
}
