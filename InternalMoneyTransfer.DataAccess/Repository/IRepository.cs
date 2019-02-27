using System.Collections.Generic;
using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region Properties

        DbSet<T> Entities { get; }

        #endregion

        #region Methods

        T Get(int id);

        IEnumerable<T> GetAll();

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        #endregion
    }
}