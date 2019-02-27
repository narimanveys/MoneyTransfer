using System.Collections.Generic;
using InternalMoneyTransfer.Core.DataModel;

namespace InternalMoneyTransfer.DAL.Repository.TransactionRepository
{
    public interface ITransactionRepository<T> where T : BaseEntity
    {
        #region Methods

        T Get(int id);

        IEnumerable<T> GetAllTransactionByAccountId(int id);

        IEnumerable<T> GetAll(int accountId);

        void Insert(T entity);

        void Update(T entity);

        #endregion
    }
}