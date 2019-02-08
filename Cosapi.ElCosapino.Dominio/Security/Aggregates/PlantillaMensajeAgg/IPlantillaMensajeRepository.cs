using System.Collections.Generic;

namespace Cosapi.ElCosapino.Dominio.Security.Aggregates.PlantillaMensajeAgg
{
    public interface IPlantillaMensajeRepository
    {
        List<PlantillaMensaje> List_PlantillaMensaje_Paginate(PaginateParams paginateParams);

        PlantillaMensaje Update(PlantillaMensaje item);

        PlantillaMensaje Insert(PlantillaMensaje item);

        PlantillaMensaje Delete(PlantillaMensaje item);

        PlantillaMensaje Activar(PlantillaMensaje item);

        PlantillaMensaje Desactivar(PlantillaMensaje item);      
    }
}
