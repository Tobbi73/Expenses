using Expense.Domain.GroupAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Tests
{
    public class GroupBuilder
    {
        private readonly Group group;

        public GroupBuilder()
        {
            group = new Group("fakeTitle", "fakeDescription");
        }

        public GroupBuilder AddOneMember(string name)
        {
            group.AddMember(name);
            return this;
        }

        public Group Build()
        {
            return group;
        }
    }
}
