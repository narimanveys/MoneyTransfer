using System.Collections.Generic;

namespace InternalMoneyTransfer.Services.Transaction
{
    public interface ITransactionService
    {
        #region Methods

        Core.DataModel.Transaction GetTransactionById(int id);

        IEnumerable<Core.DataModel.Transaction> GetAllTransactionByAccountId(int accountId);

        bool CanMakeTransaction(int accountId, decimal amount);

        void CreateTransaction(Core.DataModel.Transaction transaction);

        #endregion
    }
}