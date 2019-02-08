using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;

namespace Cosapi.ElCosapino.Aplicacion.Security.LogService
{
    public interface ILogAppService
    {
        Log Insert(Log item);
    }
}
