using System.Collections.Generic;
using InternalMoneyTransfer.Core.DataModel;

namespace InternalMoneyTransfer.DAL.Repository.AccountRepository
{
    public interface IAccountRepository<T> where T : BaseEntity
    {
        #region Methods

        T Get(int id);

        IEnumerable<T> GetAllWithoutExcludedId(int id);

        IEnumerable<T> GetAll();

        void Insert(T entity);

        void Update(T entity);

        #endregion
    }
}