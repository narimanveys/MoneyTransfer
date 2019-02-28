using System.Collections.Generic;
using InternalMoneyTransfer.DAL.Repository.AccountRepository;

namespace InternalMoneyTransfer.Services.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        #region Fields

        private readonly IAccountRepository<Core.DataModel.UserAccount> _accountRepository;

        #endregion

        #region Constructor

        public UserAccountService(IAccountRepository<Core.DataModel.UserAccount> accountRepository)

        {
            _accountRepository = accountRepository;
        }

        #endregion

        #region Methods

        public Core.DataModel.UserAccount GetAccountById(int id)
        {
            return _accountRepository.Get(id);
        }

        public IEnumerable<Core.DataModel.UserAccount> GetAllAccountsWithoutExcludedId(int excludedId)
        {
            return _accountRepository.GetAllWithoutExcludedId(excludedId);
        }

        #endregion
    }
}