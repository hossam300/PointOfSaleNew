using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces
{
    /// <summary>
    /// Defines the interfaces for <see cref="IRePointOfSaleitory{TEntity}"/> interfaces.
    /// </summary>
    public interface IRePointOfSaleitoryFactory
    {
        /// <summary>
        /// Gets the specified rePointOfSaleitory for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomRePointOfSaleitory"><c>True</c> if providing custom rePointOfSaleitry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRePointOfSaleitory{TEntity}"/> interface.</returns>
        IRePointOfSaleitory<TEntity> GetRepository<TEntity>(bool hasCustomRePointOfSaleitory = false) where TEntity : class;
    }
}
