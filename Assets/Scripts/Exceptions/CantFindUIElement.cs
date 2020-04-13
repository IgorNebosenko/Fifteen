using System;
using System.Runtime.Serialization;

namespace Game.Exceptions
{
    /// <summary>
    /// This exception generates when script can't find UI element
    /// </summary>
    public class CantFindUIElement : ApplicationException
    {
        public CantFindUIElement() : base()
        { }
        public CantFindUIElement(string message) : base(message)
        { }
        public CantFindUIElement(string message, Exception innerException) :
            base(message, innerException)
        { }
        protected CantFindUIElement(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
