using System.Collections.Generic;

namespace InternalMoneyTransfer.Services.User
{
    public interface IUserService
    {
        #region Methods

        Core.DataModel.User Authenticate(string email, string password);

        Core.DataModel.User CreateUser(Core.DataModel.User user, string password);

        Core.DataModel.User GetUserById(int id);

        IEnumerable<Core.DataModel.User> GetAll();

        #endregion
    }
}