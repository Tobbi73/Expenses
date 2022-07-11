using Expense.Domain.Exceptions;
using Expense.Domain.GroupAggregate;
using NUnit.Framework;
using System;

namespace Expense.Tests
{
    public class MemberTest
    {

        [Test]
        public void Create_member_success()
        {
            //Arrange    
            var name = "Hans Nielsen";

            //Act 
            var fakeMemberItem = new Member(name);

            //Assert
            Assert.NotNull(fakeMemberItem);
        }

        [Test]
        public void Create_member_invalid_params()
        {
            //Arrange    
            var name = string.Empty;

            //Act - assert
            Assert.Throws<ArgumentNullException>(() => new Member(name));
        }

        [Test]
        public void Create_expense_success()
        {
            //Arrange    
            var amount = 999;
            var description = "fakeDescription";
            var date = DateTime.Now;

            //Act 
            var fakeExpenseItem = new ExpenseItem(date, description, amount);

            //Assert
            Assert.NotNull(fakeExpenseItem);
        }

        [Test]
        public void Create_expense_invalid_params()
        {
            //Arrange    
            var amount = -2;
            var description = "fakeDescription";
            var date = DateTime.Now;

            //Act - Assert
            Assert.Throws<ExpenseDomainException>(() => new ExpenseItem(date, description, amount));
           
        }

        [Test]
        public void Create_payment_success()
        {
            //Arrange    
            var fakeMember = new Member("fakeName");

            var amount = 999;
            var note = "fakeNote";
            var date = DateTime.Now;

            //Act 
            var fakePayment = new Payment(date, amount, note, fakeMember);

            //Assert
            Assert.NotNull(fakePayment);
        }

        [Test]
        public void Create_payment_invalid_params()
        {
            //Arrange    
            var fakeMember = new Member("fakeName");

            var amount = -999;
            var note = "fakeNote";
            var date = DateTime.Now;

            //Act - Assert
            Assert.Throws<ExpenseDomainException>(() => new Payment(date, amount, note, fakeMember));

        }
    }
}