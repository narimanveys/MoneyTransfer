namespace InternalMoneyTransfer.Core.Dtos
{
    public class RegisterUserDto
    {
        #region Properties

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        #endregion
    }
}