using System;
using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL
{
    public class ApplicationContext : DbContext, IDbContext
    {
        #region Constructor

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        #endregion

        #region Fields

        private const string ConnectionStr = @"Data Source=DESKTOP-19QB1CJ;Initial Catalog=MoneyTransfer;Integrated Security=True;";
        
        #endregion

        #region Properties

        public DbSet<User> Users { get; set; }

        public DbSet<UserAccount> UsersAccounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStr);
        }

        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        #endregion
    }
}
