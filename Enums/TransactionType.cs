using System.ComponentModel.DataAnnotations;

namespace DollarProject.Enums
{
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Purchase,
        Refund,
        AdminAdjust,
        CompleteProduct
    }
}