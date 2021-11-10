﻿namespace VehiclesAccounting.Core.Interfaces
{
    /// <summary>
    /// Interface for CRUD operations
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IServiceAsync<T> where T : class, IEntity
    {
        /// <summary>
        /// Read all entities from database async
        /// </summary>
        /// <returns>All entites</returns>
        Task<IEnumerable<T>> ReadAllAsync();
        /// <summary>
        /// Update entity in database async
        /// </summary>
        /// <param name="entity">Updating entity</param>
        /// <returns>New entity</returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Delete entity in database async
        /// </summary>
        /// <param name="entity">Deleting entity</param>
        /// <returns>Old entity</returns>
        Task<T> DeleteAsync(T entity);
        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <returns>Added entity</returns>
        Task<T> AddAsync(T entity);
    }
}