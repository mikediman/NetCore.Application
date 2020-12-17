using System;
using System.Runtime.Serialization;

namespace NetCore.Application.Implementation
{
    [Serializable]
    public class BusinessException : Exception
    {
        public string Code { get; set; }

        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BusinessException(string code, string msg) : base(msg)
        {
            this.Code = code;
        }

        public BusinessException(string code, string msg, Exception innerException) : base(msg, innerException)
        {
            this.Code = code;
        }
    }
}
