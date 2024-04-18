/*
 * This file (HangClient.cs) is part of Hangman.
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

#nullable enable annotations
using Hangman.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman {
    public class HangClient {

        
        public static async Task<string> GetWord(string? url) {
            if (url == null || url == "") {
                url = "https://random-word-api.herokuapp.com/word?number=1";
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            try {
                response = await client.GetAsync(url);
            } catch (Exception) {
                throw new BadImageFormatException("Expected HTTP 200 OK using URL " + url + " , got " + (int)response.StatusCode);
            }
            int statusCode = (int)response.StatusCode;
            if (response.IsSuccessStatusCode) {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            } else {
                throw new BadReponseCodeException("Expected HTTP 200 OK using URL " + url + " , got " + statusCode);
            }
        }
    }
}
