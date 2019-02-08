using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.CrossCutting.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create();
    }
}
