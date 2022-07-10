using Expense.Domain.GroupAggregate;
using NUnit.Framework;
using System;

namespace Expense.Tests
{
    public class GroupTest
    {

        [Test]
        public void Create_group_success()
        {
            //Arrange    
            var title = "Summer vacay 2022";
            var description = "Lets go!";

            //Act 
            var fakeGroupItem = new Group(title, description);

            //Assert
            Assert.NotNull(fakeGroupItem);
        }

        [Test]
        public void Create_group_invalid_params()
        {
            //Arrange    
            var title = string.Empty;
            var description = string.Empty;

            //Act - assert
            Assert.Throws<ArgumentNullException>(() => new Group(title, description));
        }
    }
}