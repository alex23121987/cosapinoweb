using Cosapi.ElCosapino.Dominio.Base;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.OpcionSistemaAgg
{
    public interface IOpcionSistemaRepository
    {
        List<OpcionSistema> List_OpcionSistema_Paginate(PaginateParams paginateParams, int IDUserUnidad);
        OpcionSistema Update(OpcionSistema item);
        OpcionSistema Insert(OpcionSistema item);
        OpcionSistema Delete(OpcionSistema item);
        OpcionSistema Activar(OpcionSistema item);
        List<DDLOptionsText> ListaAsignacionGrupo();
    }
}
