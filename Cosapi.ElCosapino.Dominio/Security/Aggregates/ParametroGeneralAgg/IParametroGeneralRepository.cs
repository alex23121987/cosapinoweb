using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.ParametroGeneralAgg
{  
    public interface IParametroGeneralRepository
    {
        List<ParametroGeneral> List_ParametroGeneral_Paginate(PaginateParams paginateParams, int IDUserUnidad);
        ParametroGeneral Update(ParametroGeneral item);
        ParametroGeneral Insert(ParametroGeneral item);
        ParametroGeneral Delete(ParametroGeneral item);
        ParametroGeneral Activar(ParametroGeneral item);
    }
}
