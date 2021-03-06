using System;
using System.Threading.Tasks;

namespace LiquidProjections.RavenDB
{
    /// <summary>
    /// Defines a write-through cache that the <see cref="RavenProjector{TProjection}"/> can use to avoid unnecessary loads
    /// of the projection.
    /// </summary>
    public interface IProjectionCache<TProjection>
    {
        /// <summary>
        /// Adds a new item to the cache. 
        /// </summary>
        /// <remarks>
        /// This method must be safe in multi-threaded scenarios.
        /// </remarks>
        void Add(TProjection projection);

        /// <summary>
        /// Attempts to get the item identified by <paramref name="key"/> from the cache, or creates a new one. 
        /// </summary>
        /// <remarks>
        /// This method must be safe in multi-threaded scenarios. So multiple concurrent requests for the same key must always 
        /// return the same object.
        /// </remarks>
        Task<TProjection> Get(string key, Func<Task<TProjection>> createProjection);

        /// <summary>
        /// Attempts to get the item identified by <paramref name="key"/> from the cache.
        /// Returns <c>null</c> if the item is not in the cache.
        /// </summary>
        /// <remarks>
        /// This method must be safe in multi-threaded scenarios. So multiple concurrent requests for the same key must always 
        /// return the same object.
        /// </remarks>
        Task<TProjection> TryGet(string key);

        /// <summary>
        /// Removes the item identified by <paramref name="key"/> from the cache.
        /// </summary>
        /// <remarks>
        /// This method must be safe in multi-threaded scenarios and be idempotent. 
        /// </remarks>
        void Remove(string key);
    }
}