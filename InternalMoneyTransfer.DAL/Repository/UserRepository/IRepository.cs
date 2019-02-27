using System.Collections.Generic;
using InternalMoneyTransfer.Core.DataModel;

namespace InternalMoneyTransfer.DAL.Repository.UserRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region Methods

        T Get(int id);

        IEnumerable<T> GetAll();

        void Insert(T entity);

        #endregion
    }
}