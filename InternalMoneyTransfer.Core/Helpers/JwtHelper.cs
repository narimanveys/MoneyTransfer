using System.Text;

namespace InternalMoneyTransfer.Core.Helpers
{
    public static class JwtHelper
    {
        #region Const

        private const string Key = "SjFLi7ONtAemS2O2K32vguF7";

        #endregion

        #region Methods

        public static byte[] GetSecurityKey()
        {
            return Encoding.ASCII.GetBytes(Key);
        }

        #endregion
    }
}