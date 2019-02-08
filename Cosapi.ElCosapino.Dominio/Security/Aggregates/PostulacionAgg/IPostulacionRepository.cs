using System.Collections.Generic;
using System.Data;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PostulacionAgg
{
    public interface IPostulacionRepository
    {
        List<PostulacionBE> List_Postulacion_Paginate(PaginateParams paginateParams);

        DataTable DT_Postulacion(string S_FechaInicio, string S_FechaFin);

        List<PostulacionBE> List_Postulacion_APP(int UsuarioId);

        PostulacionBE Insert(PostulacionBE item);
    }
}
