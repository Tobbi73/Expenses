using Expense.Domain.Common;

namespace Expense.Domain.GroupAggregate
{
    public partial class Member : Entity
    {
        public string Name { get; private set; }

        private decimal _balance;

        private readonly List<ExpenseItem> _expenses;
        public IReadOnlyCollection<ExpenseItem> Expenses => _expenses;

        private readonly List<Payment> _payments;
        public IReadOnlyCollection<Payment> Payments => _payments;

        protected Member()
        {
            _expenses = new List<ExpenseItem>();
            _payments = new List<Payment>();
        }

        public Member(string name) : this()
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            _balance = 0;
        }

        public void AddExpense(string description, decimal amount)
        {
            _expenses.Add(new ExpenseItem(DateTime.Now, description, amount));
        }

        public void AddPayment(DateTime date, decimal amount, string note, Member toMember)
        {
            _payments.Add(new Payment(date, amount, note, toMember));
        }

        public void UpdateBalance(decimal amount)
        {
            _balance += amount;
        }

        public decimal GetBalance()
        {
            return _balance;
        }
    }
}
