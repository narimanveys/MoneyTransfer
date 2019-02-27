using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL
{
    public interface IDbContext
    {
        #region Properties 

        DbSet<User> Users { get; set; }

        DbSet<UserAccount> UsersAccounts { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        #endregion

        #region Methods

        DbSet<T> Set<T>() where T : BaseEntity;

        int SaveChanges();

        #endregion
    }
}