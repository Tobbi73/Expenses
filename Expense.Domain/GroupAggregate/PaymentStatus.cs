using Expense.Domain.Common;

namespace Expense.Domain.GroupAggregate
{
    public class PaymentStatus : Enumeration
    {
        public static PaymentStatus Initiated = new PaymentStatus(1, nameof(Initiated).ToLowerInvariant());
        public static PaymentStatus Paid = new PaymentStatus(3, nameof(Paid).ToLowerInvariant());
        
        public PaymentStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}
