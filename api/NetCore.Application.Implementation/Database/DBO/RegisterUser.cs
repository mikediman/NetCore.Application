using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Application.Implementation
{
    public class RegisterUser
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
