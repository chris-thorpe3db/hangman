using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{

	public static void Main(string[] args)
	{

		// Get word from server, save response body to string
		string responseString = null;
		using (var client = new HttpClient())
		{
			//This is a way of using HttpClient synchronously that doesn't result in deadlocks
			var response = client.GetAsync("https://random-word-api.herokuapp.com/word?number=1").Result;

			if (response.IsSuccessStatusCode)
			{
				var responseContent = response.Content;
				responseString = responseContent.ReadAsStringAsync().Result;
			}
		}

		// Define chars to trim
		char[] charsToTrim = new char[] {
			'[',
			']',
			'"'
		};

		// Trim
		string wordChosen = responseString.Trim(charsToTrim);

		// Define all the variables we need
		string charBeforeParse = null;
		int guessesLeft = 10;
		string dashesToString = null;
		char userGuess;

		// Generate dashes, convert to string
		char[] dashes = new char[wordChosen.Length];

		// Char array for RSTLNE

		string rstlne = "rstlne";

		for (int i = 0; i < wordChosen.Length; i++)
		{
			dashes[i] = '-';
		}

		for (int i = 0; i < wordChosen.Length; i++)
		{
			for (int x = 0; x < rstlne.Length; x++)
			{
				if (wordChosen[i] == rstlne[x])
				{
					dashes[i] = rstlne[x];
				}
			}
		}

		dashesToString = new string(dashes);

		// Prompt user & display dashes, last execution before while loop
		Console.WriteLine("Welcome to hangman! \nPlease enter one character at a time, or the entire word. \nThere are no numbers or punctuation. \nYou have already been given the letters RSTLNE. \nTo exit, click the x button on the window, or type CTRL + C at any time. \nGood luck, and have fun!");
		Console.WriteLine(dashesToString);

		while (true)
		{
			// Grab user input, check if parsing is possible
			charBeforeParse = Console.ReadLine();

			// If user enters full word, set dashes equal to chosen word and break
			if (charBeforeParse == wordChosen)
			{
				dashesToString = wordChosen;
				break;
			}
			else if (charBeforeParse == "exit")
			{
				System.Environment.Exit(0);
			}
			else if (charBeforeParse.Length != 1)
			{
				Console.Clear();
				Console.WriteLine("Please enter 1 letter!");
				// Loop back if parsing not possible
				continue;
			}

			// Parse string to char
			userGuess = char.Parse(charBeforeParse);

			// Compare guess to every character in selected word
			for (int i = 0; i < wordChosen.Length; i++)
			{
				// If input equal to char of chosen word determined by i, change corresponding char in dashes[]
				if (wordChosen[i] == userGuess)
					dashes[i] = userGuess;
			}

			// See if word contains char at all, subtract incorrect guesses as needed
			if (!wordChosen.Contains(userGuess))
				guessesLeft -= 1;

			// Convert char array to string so we can compare it to the chosen word
			dashesToString = new string(dashes);

			// if guessing is complete or if you've run out of guesses, break loop
			if (dashesToString == wordChosen || guessesLeft == 0) break;

			// Clear console screen
			Console.Clear();

			Console.WriteLine(dashesToString);
			Console.WriteLine(guessesLeft + " incorrect guesses left.");
		}

		if (dashesToString == wordChosen)
		{
			Console.WriteLine("Congratulations! The word was: " + wordChosen + ". You had " + guessesLeft + " incorrect guesses left.");
		}
		else if (guessesLeft == 0)
		{
			Console.WriteLine("Sorry, you've run out of incorrect guesses. The word was: " + wordChosen + ".");
		}
	}
}
