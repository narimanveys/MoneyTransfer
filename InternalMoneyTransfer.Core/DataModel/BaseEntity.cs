using System.ComponentModel.DataAnnotations;

namespace InternalMoneyTransfer.Core.DataModel
{
    public class BaseEntity
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        #endregion
    }
}