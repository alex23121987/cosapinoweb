using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using System.Collections.Generic;

namespace Cosapi.ElCosapino.Aplicacion.Security.CategoriaService
{
    public interface ICategoriaAppService
    {
        List<Categoria> List_Categoria_Paginate(PaginateParams paginateParams);

        List<CategoriaBE> List_Categoria_APP();

        Categoria Update(Categoria item);

        Categoria Insert(Categoria item);

        Categoria Delete(Categoria item);
    }
}
