using System.ComponentModel.DataAnnotations;

namespace InternalMoneyTransfer.Core.DataModel
{
    public class UserAccount : BaseEntity
    {
        #region Properties

        public decimal AvailableAmount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        #endregion
    }
}