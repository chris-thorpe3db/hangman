using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Exceptions {
    public class BadReponseCodeException : Exception {
        
        public string URL { get; }
        public BadReponseCodeException() { }
        public BadReponseCodeException(string message) 
            : base(message) { }
        public BadReponseCodeException(string message, Exception inner) 
            : base(message, inner) { }
        public BadReponseCodeException(string message, string url) 
            : this(message) {
            URL = url;
        }
    }
}
