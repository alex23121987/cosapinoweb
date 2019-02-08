using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.PostulacionService
{
    public class PostulacionAppService : IPostulacionAppService
    {
        readonly IPostulacionRepository _PostulacionRepository;

        public PostulacionAppService()
        {
            _PostulacionRepository = new PostulacionRepository();
        }

        public List<PostulacionBE> List_Postulacion_Paginate(PaginateParams paginateParams)
        {
            return _PostulacionRepository.List_Postulacion_Paginate(paginateParams);
        }

        public DataTable DT_Postulacion(string S_FechaInicio, string S_FechaFin)
        {
            return _PostulacionRepository.DT_Postulacion(S_FechaInicio, S_FechaFin);
        }

        public List<PostulacionBE> List_Postulacion_APP(int UsuarioId)
        {
            return _PostulacionRepository.List_Postulacion_APP( UsuarioId);
        }

        public PostulacionBE Insert(PostulacionBE item)
        {
            return _PostulacionRepository.Insert(item);
        }

    }
}
