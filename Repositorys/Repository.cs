﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zadatak1.Interfaces;

namespace Zadatak1.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShopContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ShopContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetById(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task SaveChanges() => await _context.SaveChangesAsync();
        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
