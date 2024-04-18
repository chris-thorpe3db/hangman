using Hangman.Exceptions;

using System.Net.Http;
using System.Threading.Tasks;

namespace Hangman {
    public class HangClient {
        public static async Task<string> GetWord(string? url) {
            if (url == null || url == "") {
                url = "https://random-word-api.herokuapp.com/word?number=1";
            }
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            
            response = await client.GetAsync(url);
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
