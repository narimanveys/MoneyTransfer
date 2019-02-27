using System.Collections.Generic;

namespace InternalMoneyTransfer.Services.UserAccount
{
    public interface IUserAccountService
    {
        #region Methods

        void CreateAccount(Core.DataModel.UserAccount account);

        Core.DataModel.UserAccount GetAccountById(int id);

        IEnumerable<Core.DataModel.UserAccount> GetAllAccountsWithoutExcludedId(int excludedId);

        #endregion
    }
}