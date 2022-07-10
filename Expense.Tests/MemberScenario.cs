using Expense.Domain.GroupAggregate;
using NUnit.Framework;
using System;
using System.Linq;

namespace Expense.Tests
{
    public class MemberScenarios
    {
        [Test]
        public void Add_expense()
        {
            //Arrange    
            var group = new GroupBuilder()
                .AddOneMember("member1")
                .AddOneMember("member2")
                .AddOneMember("member3")
                .Build();

            var member1 = group.Members.ElementAt(0);
            var member2 = group.Members.ElementAt(1);
            var member3 = group.Members.ElementAt(2);

            var amount1 = 50;
            var amount2 = 59;
            var amount3 = 33;
            var amount4 = 125.5;

            //Act
            member1.AddExpense("fakeExpense1", amount1);
            member1.UpdateBalance(amount1);
            
            member1.AddExpense("fakeExpense2", amount2);
            member1.UpdateBalance(amount2);
            
            member2.AddExpense("fakeExpense3", amount3);
            member2.UpdateBalance(amount3);

            member3.AddExpense("fakeExpense3", (decimal)amount4);
            member3.UpdateBalance((decimal)amount4);

            //Assert
            Assert.AreEqual(2, member1.Expenses.Count);
            Assert.AreEqual(1, member2.Expenses.Count);
            Assert.AreEqual(1, member3.Expenses.Count);

            Assert.AreEqual(109, member1.GetBalance());
            Assert.AreEqual(33, member2.GetBalance());
            Assert.AreEqual(125.5, member3.GetBalance());

            Assert.AreEqual(4, group.Members.Sum(x=>x.Expenses.Count()));
        }

        [Test]
        public void Add_payment()
        {
            //Arrange    
            var group = new GroupBuilder()
                .AddOneMember("member1")
                .AddOneMember("member2")
                .Build();

            var amount = 25;

            var member1 = group.Members.ElementAt(0);
            var member2 = group.Members.ElementAt(1);

            //Act
            member1.AddPayment(DateTime.Now, amount, "fakenote", member2.Id);
            member1.Payments.Last().SetPaidStatus();
            
            member1.UpdateBalance(amount);
            member2.UpdateBalance(-amount);

            //Assert
            Assert.AreEqual(amount, member1.GetBalance());
            Assert.AreEqual(-amount, member2.GetBalance());

            Assert.AreEqual(1, member1.Payments.Count);
        }

        [Test]
        public void Group_settle_4members()
        {
            //Arrange    
            var group = new GroupBuilder()
                .AddOneMember("member1")
                .AddOneMember("member2")
                .AddOneMember("member3")
                .AddOneMember("member4")
                .Build();

            var member1 = group.Members.ElementAt(0);
            var member2 = group.Members.ElementAt(1);
            var member3 = group.Members.ElementAt(2);
            var member4 = group.Members.ElementAt(3);

            var amount1 = 50;
            var amount2 = 59;
            var amount3 = 33;
            var amount4 = 125.5;

            //Act
            member1.AddExpense("fakeExpense1", amount1);
            member1.UpdateBalance(amount1);

            member1.AddExpense("fakeExpense2", amount2);
            member1.UpdateBalance(amount2);

            member2.AddExpense("fakeExpense3", amount3);
            member2.UpdateBalance(amount3);

            member3.AddExpense("fakeExpense4", (decimal)amount4);
            member3.UpdateBalance((decimal)amount4);

            group.SettleMembers();

            //Assert
            Assert.AreEqual(3, group.Members.Sum(m=>m.Payments.Count()));
            
            Assert.AreEqual((decimal)66.875, member1.GetBalance());
            Assert.AreEqual((decimal)66.875, member2.GetBalance());
            Assert.AreEqual((decimal)66.875, member3.GetBalance());
            Assert.AreEqual((decimal)66.875, member4.GetBalance());
        }

        [Test]
        public void Group_settle_3members()
        {
            //Arrange    
            var group = new GroupBuilder()
                .AddOneMember("John")
                .AddOneMember("Mary")
                .AddOneMember("Peter")
                .Build();

            var member1 = group.Members.ElementAt(0);
            var member2 = group.Members.ElementAt(1);
            var member3 = group.Members.ElementAt(2);

            var amount1 = 500;
            var amount2 = 150;
            var amount3 = 100;

            //Act
            member1.AddExpense("hotel", amount1);
            member1.UpdateBalance(amount1);

            member2.AddExpense("restaurant", amount2);
            member2.UpdateBalance(amount2);

            member3.AddExpense("sightseeing", amount3);
            member3.UpdateBalance(amount3);

            group.SettleMembers();

            //Assert
            Assert.AreEqual(2, group.Members.Sum(m => m.Payments.Count()));

            Assert.AreEqual(250, member1.GetBalance());
            Assert.AreEqual(250, member2.GetBalance());
            Assert.AreEqual(250, member3.GetBalance());
        }
    }
}
