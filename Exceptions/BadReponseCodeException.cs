/*
 * This file (BadResponseCodeException.cs) is part of Hangman.
 *
 * Hangman is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Hangman is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Hangman. If not, see <https://www.gnu.org/licenses/>.
 */

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
