using LandscapingTR.Core.Entities;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected LandscapingTRDbContext DataContext { get; }


        public BaseRepository(LandscapingTRDbContext dbContext)
        {
            DataContext = dbContext;
        }

        /// <summary>
        /// Gets an entity with a given id.
        /// </summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The entity with the corresponding id.</returns>
        public async Task<TEntity> GetAsync(TKey id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }

           var entity = await DataContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(id), "Entity was not found");
            }

            return entity;
        }

        /// <summary>
        /// Gets a tracked entity with a given id.
        /// </summary>
        /// <param name="id">The id of the entity to return.</param>
        /// <returns>The tracked entity with the corresponding id.</returns>
        public async Task<TEntity> GetTrackedAsync(TKey id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");
            }

            var entity = await DataContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                // Attach the entity to the context without modifying its state
                DataContext.Attach(entity);
            }
            else
            {
                throw new ArgumentNullException(nameof(id), "Entity was not found");
            }

            return entity;
        }

        /// <summary>
        /// Gets all entities of type TEntity.
        /// </summary>
        /// <returns>All entities of type TEntity.</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DataContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity.</returns>
        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            if  (entity.Id == null || entity.Id.Equals(default(TKey)))
            {
                // If the entity has a default (unset) key, it's a new entity to be inserted
                await DataContext.Set<TEntity>().AddAsync(entity);
            }
            else
            {
                // If the entity has a non-default key, it should be updated
                DataContext.Set<TEntity>().Update(entity);
            }

            await DataContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Saves a list of entities.
        /// </summary>
        /// <param name="entities">The list of entities to save.</param>
        /// <returns>The list of saved entities.</returns>
        public async Task<List<TEntity>> SaveRangeAsync(List<TEntity> entities)
        {
            foreach ( var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                if (entity.Id == null || entity.Id.Equals(default(TKey)))
                {
                    // If the entity has a default (unset) key, it's a new entity to be inserted
                    await DataContext.Set<TEntity>().AddAsync(entity);
                }
                else
                {
                    // If the entity has a non-default key, it should be updated
                    DataContext.Set<TEntity>().Update(entity);
                }
            }

            await DataContext.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Deletes an entitiy.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The task.</returns>
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            DataContext.Set<TEntity>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a list of entities.
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>The task.</returns>
        public async Task DeleteRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                }

                DataContext.Set<TEntity>().Remove(entity);
            }

            await DataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Detaches all entities.
        /// </summary>
        public void DetachEntities()
        {
            foreach (var entry in DataContext.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
