using System.ComponentModel.DataAnnotations;

namespace InternalMoneyTransfer.Core.DataModel
{
    public class User : BaseEntity
    {
        #region Properties

        public string FullName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserAccount Account { get; set; }

        #endregion
    }
}