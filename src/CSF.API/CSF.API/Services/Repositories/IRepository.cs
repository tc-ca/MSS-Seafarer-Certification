namespace CSF.API.Services.Repositories
{
    using System.Collections.Generic;
    /// <summary>
    /// Defines a repository for retrieving data.
    /// </summary>
    /// <typeparam name="TEntity">Defines the entity in the repository</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets all entities from the repository.
        /// </summary>
        /// <returns>A list of <see cref="TEntity"/>.</returns>
        IEnumerable<TEntity> GetAll();
    }
}
