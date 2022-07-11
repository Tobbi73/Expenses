using Expense.Domain.Common;

namespace Expense.Domain.GroupAggregate
{
    public partial class Group : Entity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        private readonly List<Member> _members;
        public IReadOnlyCollection<Member> Members => _members;

        protected Group() 
        {
            _members = new List<Member>();
        }

        public Group(string title, string description) : this()
        {
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
            Description = description;
        }

        public void AddMember(string name)
        {
            _members.Add(new Member(name));
        }

        public void SettleMembers()
        {
            var avgPayment = _members.Sum(x => x.GetBalance()) / _members.Count;
            
            var shouldPay = _members.Where(x => x.GetBalance() < avgPayment);
            var shouldReceive = _members.Except(shouldPay);

            var paymentNote = "group settlement";

            // loop paying members
            foreach (var payingMember in shouldPay)
            {
                // loop receiving members
                foreach(var receivingMember in shouldReceive)
                {
                    if(receivingMember.GetBalance() == avgPayment || payingMember.GetBalance() == avgPayment) 
                    {
                        continue;
                    }

                    var amountToRecieve = receivingMember.GetBalance() - avgPayment;
                    var amountToPay = avgPayment - payingMember.GetBalance();

                    if (amountToPay > amountToRecieve)
                    {
                        payingMember.UpdateBalance(amountToRecieve);
                        receivingMember.UpdateBalance(-amountToRecieve);
                        payingMember.AddPayment(DateTime.Now, amountToPay - amountToRecieve, paymentNote, receivingMember);
                        continue;
                    }

                    payingMember.UpdateBalance(amountToPay);
                    receivingMember.UpdateBalance(-amountToPay);
                    payingMember.AddPayment(DateTime.Now, amountToPay, paymentNote, receivingMember);
                }
            }
        }
    }
}
