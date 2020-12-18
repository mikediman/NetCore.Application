using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Application.Implementation
{
    public class ApplicationException : BusinessException
    {
        public static BusinessException RegistrationFails = new BusinessException("APEX01", "There is an issue with the registration. Please try again.");
    }
}
