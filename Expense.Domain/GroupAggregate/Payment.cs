using Expense.Domain.Common;
using Expense.Domain.Exceptions;

namespace Expense.Domain.GroupAggregate
{
    public class Payment : Entity
    {
        private DateTime _date;
        private decimal _amount;
        private string _note;
        private int _toMemberId;

        public PaymentStatus PaymentStatus { get; private set; }
        private int _paymentStatusId;

        protected Payment() { }

        public Payment(DateTime date, decimal amount, string note, int toMemberId)
        {
            if (amount <= 0)
            {
                throw new ExpenseDomainException("Invalid amount number");
            }

            _date = date;
            _amount = amount;
            _note = note;
            _toMemberId = toMemberId;

            _paymentStatusId = PaymentStatus.Initiated.Id;
        }

        public decimal GetAmount()
        {
            return _amount;
        }

        public void SetPaidStatus()
        {
            if (_paymentStatusId == PaymentStatus.Initiated.Id)
            {
                _paymentStatusId = PaymentStatus.Paid.Id;
            }
        }
    }
}
