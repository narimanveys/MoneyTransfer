using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL
{
    public interface IDbContext
    {
        #region Methods

        DbSet<T> Set<T>() where T : BaseEntity;

        int SaveChanges();

        #endregion
    }
}