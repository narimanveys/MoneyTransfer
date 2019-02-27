using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL.Repository
{
    class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Constructor

        public Repository(IDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        #endregion

        #region Fields

        private readonly IDbContext _context;

        private readonly DbSet<T> _entities;

        #endregion

        #region Properties

        public DbSet<T> Entities => _entities;

        #endregion

        #region Methods

        public T Get(int id)
        {
            return _entities.FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.OrderBy(entity => entity.Id).ToList();
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        #endregion
    }
}
