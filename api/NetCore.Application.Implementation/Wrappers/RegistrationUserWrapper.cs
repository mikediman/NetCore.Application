using NetCore.Application.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Application.Implementation
{
    public class RegistrationUserWrapper
    {
        public UserRegistrationRequest request { get; set; }
        public UserRegistrationResponse response { get; set; }
        public RegisterUser criteria { get; set; }
    }
}
