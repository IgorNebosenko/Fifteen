using System;
using System.Runtime.Serialization;

namespace Game.Exceptions
{
    /// <summary>
    /// Exception, which generates if same script can't find GlogalObject
    /// </summary>
    public class CantFindGlobalObj : ApplicationException
    {
        public CantFindGlobalObj() : base()
        { }

        public CantFindGlobalObj(string message) : base(message)
        { }

        public CantFindGlobalObj(string message, Exception innerException) :
            base(message, innerException)
        { }

        protected CantFindGlobalObj(SerializationInfo info, StreamingContext context) :
            base(info, context)
        { }
    }
}