using System.Collections.Generic;
using InternalMoneyTransfer.Core.DataModel;

namespace InternalMoneyTransfer.DAL.Repository.AccountRepository
{
    public interface IAccountRepository<T> where T : BaseEntity
    {
        #region Methods

        T Get(int id);

        IEnumerable<T> GetAllWithoutExcludedId(int id);

        void Insert(T entity);

        #endregion
    }
}