namespace CSF.MPDIS.API.Services.Repositories
{
    using System.Collections.Generic;
    /// <summary>
    /// Defines a repository for retrieving data.
    /// </summary>
    /// <typeparam name="TEntity">Defines the entity in the repository</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Adds the specific entity to the repository.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// Gets all entities from the repository.
        /// </summary>
        /// <returns>A list of <see cref="TEntity"/>.</returns>
        IEnumerable<TEntity> GetAll();
    }
}
