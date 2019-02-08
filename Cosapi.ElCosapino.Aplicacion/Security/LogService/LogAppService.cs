using Cosapi.ElCosapino.Dominio.Security.Aggregates.LogAgg;
using Cosapi.ElCosapino.Repositorio.Repositories;

namespace Cosapi.ElCosapino.Aplicacion.Security.LogService
{
    public class LogAppService : ILogAppService
    {
        readonly ILogRepository _LogRepository;
        public LogAppService()
        {
            _LogRepository = new LogRepository();
        }

        public Log Insert(Log item)
        {
            return _LogRepository.Insert(item);
        }
    }
}
