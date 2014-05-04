using System;
using System.Runtime.Serialization;

namespace DustInTheWind.Lisimba.Egg.Exceptions
{
    public class EggIncorrectVersionException : EggException
    {
        public EggIncorrectVersionException()
            : base()
        {
        }

        public EggIncorrectVersionException(string message)
            : base(message)
        {
        }

        public EggIncorrectVersionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EggIncorrectVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
