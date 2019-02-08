using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.CategoriaAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.Aplicacion.Security.CategoriaService
{    
    public class CategoriaAppService : ICategoriaAppService
    {
        readonly ICategoriaRepository _CategoriaRepository;
        public CategoriaAppService()
        {
            _CategoriaRepository = new CategoriaRepository();
        }
        public List<Categoria> List_Categoria_Paginate(PaginateParams paginateParams)
        {
            return _CategoriaRepository.List_Categoria_Paginate(paginateParams);
        }

        public List<CategoriaBE> List_Categoria_APP()
        {
            return _CategoriaRepository.List_Categoria_APP();
        }

        public Categoria Update(Categoria item)
        {
            return _CategoriaRepository.Update(item);
        }

        public Categoria Insert(Categoria item)
        {
            return _CategoriaRepository.Insert(item);
        }

        public Categoria Delete(Categoria item)
        {
            return _CategoriaRepository.Delete(item);
        }
    }
}
