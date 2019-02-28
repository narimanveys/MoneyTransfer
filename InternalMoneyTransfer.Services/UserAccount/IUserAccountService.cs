using System.Collections.Generic;

namespace InternalMoneyTransfer.Services.UserAccount
{
    public interface IUserAccountService
    {
        #region Methods

        Core.DataModel.UserAccount GetAccountById(int id);

        IEnumerable<Core.DataModel.UserAccount> GetAllAccountsWithoutExcludedId(int excludedId);

        #endregion
    }
}