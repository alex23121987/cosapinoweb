using Cosapi.ElCosapino.Dominio;
using Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.Aplicacion.Security.ParametroGeneralService
{   
    public interface IParametroGeneralAppService
    {
        List<ParametroGeneral> List_ParametroGeneral_Paginate(PaginateParams paginateParams, int IDUserUnidad);
        ParametroGeneral Update(ParametroGeneral item);
        ParametroGeneral Insert(ParametroGeneral item);
    }
}
