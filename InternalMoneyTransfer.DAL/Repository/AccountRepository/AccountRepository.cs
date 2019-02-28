using System.Collections.Generic;
using System.Linq;
using InternalMoneyTransfer.Core.DataModel;
using Microsoft.EntityFrameworkCore;

namespace InternalMoneyTransfer.DAL.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository<UserAccount>
    {
        #region Fields

        private readonly IDbContext _appContext;

        #endregion

        #region Constructor

        public AccountRepository(IDbContext appContext)
        {
            _appContext = appContext;
        }

        #endregion

        #region Methods

        public UserAccount Get(int id)
        {
            return _appContext.UsersAccounts
                .Include(x => x.User)
                .FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<UserAccount> GetAllWithoutExcludedId(int excludeId)
        {
            return _appContext.UsersAccounts.Include(c => c.User).Where(t => t.Id != excludeId).ToList();
        }

        public void Insert(UserAccount entity)
        {
            _appContext.UsersAccounts.Add(entity);
            _appContext.SaveChanges();
        }

        #endregion
    }
}