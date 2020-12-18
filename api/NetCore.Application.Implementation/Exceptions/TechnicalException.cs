using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Application.Implementation
{
    public class TechnicalException : BusinessException
    {
        public static BusinessException DbFails = new BusinessException("TCEX01", "Database transactions fails.");
    }
}
