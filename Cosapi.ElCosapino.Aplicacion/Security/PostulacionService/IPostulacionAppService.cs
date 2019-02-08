using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg;
using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Aplicacion.Security.PostulacionService
{
    public interface IPostulacionAppService
    {
        List<PostulacionBE> List_Postulacion_Paginate(PaginateParams paginateParams);

        List<PostulacionBE> List_Postulacion_APP(int UsuarioId);

        DataTable DT_Postulacion(string S_FechaInicio, string S_FechaFin);

        PostulacionBE Insert(PostulacionBE item);
    }
}
