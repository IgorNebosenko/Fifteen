using System;
using System.Runtime.Serialization;

namespace Game.Exceptions
{
    /// <summary>
    /// Exception, which generates when scrip can't find same object
    /// </summary>
    public class CantFindObject : ApplicationException
    {
        public CantFindObject() : base()
        { }
        public CantFindObject(string message) : base(message)
        { }
        public CantFindObject(string message, Exception innerException) : 
            base(message, innerException)
        { }
        protected CantFindObject(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}