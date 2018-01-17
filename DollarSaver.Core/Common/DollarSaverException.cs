using System;
using System.Data;
using System.Configuration;

namespace DollarSaver.Core.Common {

    /// <summary>
    /// Summary description for DollarSaverException
    /// </summary>
    public class DollarSaverException : System.Exception {
        public DollarSaverException() : base() { }

        public DollarSaverException(string message) : base(message) { }

        public DollarSaverException(String message, Exception innerException) : base(message, innerException) { }

    }


    public class ForeignKeyException : System.Exception {

        public ForeignKeyException() : base() { }

        public ForeignKeyException(string message) : base(message) { }

        public ForeignKeyException(String message, Exception innerException) : base(message, innerException) { }

    }

}