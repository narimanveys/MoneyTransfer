using System;
using System.Collections.Generic;
using InternalMoneyTransfer.DAL.Repository.AccountRepository;
using InternalMoneyTransfer.DAL.Repository.TransactionRepository;

namespace InternalMoneyTransfer.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        #region Constructor

        public TransactionService(ITransactionRepository<Core.DataModel.Transaction> transactionRepository,
            IAccountRepository<Core.DataModel.UserAccount> userAccountRepository)
        {
            _transactionRepository = transactionRepository;
            _userAccountRepository = userAccountRepository;
        }

        #endregion

        #region Fields

        private readonly ITransactionRepository<Core.DataModel.Transaction> _transactionRepository;

        private readonly IAccountRepository<Core.DataModel.UserAccount> _userAccountRepository;

        #endregion

        #region Methods

        public IEnumerable<Core.DataModel.Transaction> GetAllTransactionByAccountId(int accountId)
        {
            return _transactionRepository.GetAll(accountId);
        }

        public Core.DataModel.Transaction GetTransactionById(int id)
        {
            return _transactionRepository.Get(id);
        }

        public bool CanMakeTransaction(int accountId, decimal amount)
        {
            var creditor = _userAccountRepository.Get(accountId);
            return amount <= creditor.AvailableAmount;
        }

        public void CreateTransaction(Core.DataModel.Transaction transaction)
        {
            transaction.Created = DateTime.UtcNow;
            _transactionRepository.Insert(transaction);
        }

        #endregion
    }
}