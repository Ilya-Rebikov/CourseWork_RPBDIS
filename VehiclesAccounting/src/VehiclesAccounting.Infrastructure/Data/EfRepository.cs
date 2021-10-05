﻿using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehiclesAccounting.Core.Interfaces;

namespace VehiclesAccounting.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly VehiclesContext _vehiclesContext;
        public EfRepository(VehiclesContext vehiclesContext)
        {
            _vehiclesContext = vehiclesContext;
        }

        public Task<T> GetByIdAsync<T>(int id) where T : class, IEntity
        {
            return _vehiclesContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<T>> ListAsync<T>() where T : class, IEntity
        {
            return _vehiclesContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : class, IEntity
        {
            await _vehiclesContext.Set<T>().AddAsync(entity);
            await _vehiclesContext.SaveChangesAsync();
            return entity;
        }
        public Task UpdateAsync<T>(T entity) where T : class, IEntity
        {
            _vehiclesContext.Entry(entity).State = EntityState.Modified;
            return _vehiclesContext.SaveChangesAsync();
        }

        public Task DeleteAsync<T>(T entity) where T : class, IEntity
        {
            _vehiclesContext.Set<T>().Remove(entity);
            return _vehiclesContext.SaveChangesAsync();
        }
    }
}