using System;
using System.ComponentModel.DataAnnotations;

namespace InternalMoneyTransfer.Core.DataModel
{
    public class Transaction : BaseEntity
    {
        #region Properties

        public DateTime Created { get; set; }

        public decimal Amount { get; set; }

        public UserAccount Creditor { get; set; }

        public UserAccount Debtor { get; set; }

        #endregion
    }
}