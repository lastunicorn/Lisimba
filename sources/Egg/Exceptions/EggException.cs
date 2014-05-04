using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DustInTheWind.Lisimba.Egg
{
    public class EggException : ApplicationException
    {
        public EggException()
            : base()
        {
        }

        public EggException(string message)
            : base(message)
        {
        }

        public EggException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EggException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
