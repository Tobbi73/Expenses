using Expense.Domain.Common;
using Expense.Domain.Exceptions;

namespace Expense.Domain.GroupAggregate
{
    public class ExpenseItem : Entity
    {
        private DateTime _date;
        private string _description;
        private decimal _amount;
        
        protected ExpenseItem() { }

        public ExpenseItem(DateTime date, string description, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ExpenseDomainException("Invalid amount number");
            }

            _date = date;
            _amount = amount;
            _description = description;
        }
    }
}
