using System;

namespace InternalMoneyTransfer.Core.Dtos
{
    public class TransactionDto
    {
        #region Properties

        public int Id { get; set; }

        public DateTime Created { get; set; }

        public decimal Amount { get; set; }

        public string Creditor { get; set; }

        public string Debtor { get; set; }

        public int CreditorId { get; set; }

        public int DebtorId { get; set; }

        #endregion
    }
}