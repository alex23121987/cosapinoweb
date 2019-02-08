namespace Cosapi.ElCosapino.CrossCutting.Adapter
{
    /// <summary>
    /// Clase base para mapeo
    /// <remarks>
    /// Contrato para trabajar con "auto" mappers 
    /// </remarks>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>
        /// Adapta un objecto origen a una instancia de tipo <paramref>
        ///         <name>TTarget</name>
        ///     </paramref>
        /// </summary>
        /// <typeparam name="TSource">Tipo de item origen</typeparam>
        /// <typeparam name="TTarget">Tipo de item destino</typeparam>
        /// <param name="source">Instanca a adaptar</param>
        /// <returns><paramref name="source"/> adaptado a <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TTarget : class, new()
            where TSource : class;


        /// <summary>
        /// Adapta un objecto origen a una instancia de tipo <paramref>
        ///         <name>TTarget</name>
        ///     </paramref>
        /// </summary>
        /// <typeparam name="TTarget">Tipo de item destino</typeparam>
        /// <param name="source">Instance a adaptar</param>
        /// <returns><paramref name="source"/> adaptado a <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TTarget>(object source)
            where TTarget : class, new();
    }
}
