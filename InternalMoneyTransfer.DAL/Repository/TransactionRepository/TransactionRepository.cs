using System;
using System.Collections.Generic;
using System.Linq;
using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL.Repository.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository<Transaction>
    {
        #region Fields

        private readonly IDbContext _appContext;

        #endregion

        #region Constructor

        public TransactionRepository(IDbContext appContext)
        {
            _appContext = appContext;
        }

        #endregion

        #region Methods

        public Transaction Get(int id)
        {
            return _appContext.Transactions
                .Include(c => c.Creditor).ThenInclude(c => c.User)
                .Include(c => c.Debtor).ThenInclude(c => c.User)
                .FirstOrDefault(entity => entity.Id == id);
        }
        
        public IEnumerable<Transaction> GetAll(int accountId)
        {
            return _appContext.Transactions
                .Include(c => c.Creditor).ThenInclude(c => c.User)
                .Include(c => c.Debtor).ThenInclude(c => c.User)
                .Where(t => t.Creditor.Id == accountId || t.Debtor.Id == accountId).ToList();
        }

        public void Insert(Transaction entity)
        {
            using (var context = new ApplicationContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        _appContext.Transactions.Add(entity);

                        entity.Creditor.AvailableAmount -= entity.Amount;
                        entity.Debtor.AvailableAmount += entity.Amount;

                        _appContext.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Save transaction error: {ex.Message}");
                    }
                }
            }
        }

        #endregion
    }
}