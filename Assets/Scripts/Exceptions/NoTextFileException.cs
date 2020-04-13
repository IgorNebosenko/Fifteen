using System;
using System.Runtime.Serialization;

namespace Game.Exceptions
{
    /// <summary>
    /// Exception, which generates when game can't find localization file at
    /// Assets/Recourses/Text
    /// </summary>
    public class NoTextFileException : ApplicationException
    {
        public NoTextFileException() : base()
        { }

        public NoTextFileException(string message) : base(message)
        { }

        public NoTextFileException(string message, Exception innerException) :
            base(message, innerException)
        { }

        protected NoTextFileException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        { }
    }
}