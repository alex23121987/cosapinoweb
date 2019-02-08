using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosapi.ElCosapino.CrossCutting.Adapter
{
    public static class TypeAdapterFactory
    {
        #region Members

        static ITypeAdapterFactory _currentTypeAdapterFactory;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Setear el tipo actual  adapter factory
        /// </summary>
        /// <param name="adapterFactory">el adapter factory a setear</param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            _currentTypeAdapterFactory = adapterFactory;
        }
        /// <summary>
        /// Crear un nuevo tipo adapter para el actual factory
        /// </summary>
        /// <returns>Crear type adapter</returns>
        public static ITypeAdapter CreateAdapter()
        {
            return _currentTypeAdapterFactory.Create();
        }
        #endregion
    }
}
