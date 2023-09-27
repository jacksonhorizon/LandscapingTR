using LandscapingTR.Core.Entities;

namespace LandscapingTR.Core.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// Gets an entity with a given id.
        /// </summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The entity with the corresponding id.</returns>
        Task<TEntity> GetAsync(TKey id);

        /// <summary>
        /// Gets a tracked entity with a given id.
        /// </summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The tracked entity with the corresponding id.</returns>
        Task<TEntity> GetTrackedAsync(TKey id);

        /// <summary>
        /// Gets all entities of type TEntity.
        /// </summary>
        /// <returns>All entities of type TEntity.</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity.</returns>
        Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Saves a list of entities.
        /// </summary>
        /// <param name="entities">The list of entities to save.</param>
        /// <returns>The list of saved entities.</returns>
        Task<List<TEntity>> SaveRangeAsync(List<TEntity> entities);

        /// <summary>
        /// Deletes an entitiy.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The task.</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes a list of entities.
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The task.</returns>
        Task DeleteRangeAsync(List<TEntity> entities);

        /// <summary>
        /// Detaches all entities.
        /// </summary>
        void DetachEntities();
    }
}
