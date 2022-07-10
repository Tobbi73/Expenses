using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense.Domain.Exceptions
{
    public class ExpenseDomainException : Exception
    {
        public ExpenseDomainException()
        { }

        public ExpenseDomainException(string message)
            : base(message)
        { }

        public ExpenseDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
