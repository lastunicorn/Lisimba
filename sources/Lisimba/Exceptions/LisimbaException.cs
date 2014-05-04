using System;
using System.Runtime.Serialization;

namespace DustInTheWind.Lisimba.Exceptions
{
    class LisimbaException : ApplicationException
    {
        public LisimbaException()
            : base()
        {
        }

        public LisimbaException(string message)
            : base(message)
        {
        }

        public LisimbaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected LisimbaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
