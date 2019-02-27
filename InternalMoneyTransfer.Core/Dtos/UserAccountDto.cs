namespace InternalMoneyTransfer.Core.Dtos
{
    public class UserAccountDto
    {
        #region Properties

        public int Id { get; set; }

        public decimal AvailableAmount { get; set; }

        public UserDto User { get; set; }

        #endregion
    }
}