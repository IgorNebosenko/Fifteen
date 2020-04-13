using System;
using System.Runtime.Serialization;

namespace Game.Exceptions
{
    /// <summary>
    /// Exception which generates whengame can't find language in files
    /// of game assets. If this exception generates - this signals about
    /// troubels whith file system of game\device
    /// </summary>
    public class CantFindLangInAssets : ApplicationException
    {
        public CantFindLangInAssets() : base()
        { }

        public CantFindLangInAssets(string message) : base(message)
        { }

        public CantFindLangInAssets(string message, Exception innerException) :
            base(message, innerException)
        { }

        protected CantFindLangInAssets(SerializationInfo info, StreamingContext context) :
            base(info, context)
        { }
    }
}