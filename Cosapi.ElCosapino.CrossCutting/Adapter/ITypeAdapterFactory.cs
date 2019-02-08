using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.CrossCutting.Adapter
{
    /// <summary>
    /// Clase base para adapter factory
    /// </summary>
    public interface ITypeAdapterFactory
    {
        ITypeAdapter Create();
    }
}
