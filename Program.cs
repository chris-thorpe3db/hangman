/*
	This is a hangman program that grabs a word from the internet and has you guess it. More in-depth explanation in ANALYSIS.md
    Copyright (C) 2023 Christopher Thorpe.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;

namespace Hangman {
	class Program {
		static void Main(string[] args) {
			string responseString = null;
			string WordURL = "https://random-word-api.herokuapp.com/word?number=1";
			bool wordNotExit = false;
			bool breakpointReached = false;

            do {
				try {
					responseString = HangClient.GetWord(WordURL).Result;
					if (responseString != "exit") wordNotExit = true;
				} catch (Exception e) {
					Console.WriteLine($"Exception caught! Are you connected to the internet? \n\nDetails:\n{e}\n\nPress any key to quit.");
					Console.ReadKey();
					Exit(1);
				}
			} while (!wordNotExit);
			

			// We trim off unnecessary characters here: it's much faster than attempting to parse the JSON.
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
			string dashesToString;
			char userGuess;
			List<char> charsGuessed = new List<char>();
			bool containsChar = false;
			bool userWon = false;

			// We use a char array here instead of a string so we have easier control over individual characters
			char[] dashes = new char[wordChosen.Length];

			string rstlne = "rstlne";

			for (int i = 0; i < wordChosen.Length; i++) {
				dashes[i] = '-';
			}

			// Look through char array for any instance of the letters R S T L N or E.
			for (int i = 0; i < wordChosen.Length; i++) {
				for (int x = 0; x < rstlne.Length; x++) {
					if (wordChosen[i] == rstlne[x])
						dashes[i] = rstlne[x];			
				}
			}

			dashesToString = new string(dashes);
			charsGuessed.AddRange(rstlne);

			breakpointReached = false;
			

			// Prompt user & display dashes, last execution before while loop
			Console.Clear();
			Console.WriteLine("Welcome to hangman! \nPlease enter one character at a time, or the entire word. \nThere are no numbers or punctuation. \nYou have already been given the letters RSTLNE. \nTo exit, click the x button on the window, type \"exit\" into the console, or type CTRL + C at any time. \nGood luck, and have fun!");
			

			while (!breakpointReached) {
				containsChar = false;
                Console.WriteLine(dashesToString);
                Console.WriteLine(guessesLeft + " incorrect guesses left.");
                // Grab user input, check if parsing is possible
                charBeforeParse = Console.ReadLine().ToLower();

				// If user enters full word, set dashes equal to chosen word and break;
				// Exit with code 0 when exit is typed into the prompt
				if (charBeforeParse == wordChosen) {
					dashesToString = wordChosen;
					userWon = true;
					break;
				} else if (charBeforeParse == "exit") {
					Exit(0);
				} else if (charBeforeParse.Length != 1) {
					Console.Clear();
					Console.WriteLine("Please enter 1 letter!");
					// Loop back if parsing not possible
					continue;
				} else if (charsGuessed.Contains(char.Parse(charBeforeParse))) {
					Console.Clear();
					Console.WriteLine("You've already guessed that letter!");
					continue;
				}

				// Parse string to char: allows us to compare it to individual characters in a string
				userGuess = char.Parse(charBeforeParse);
				charsGuessed.Add(userGuess);

				// Compare guess to every character in selected word
				for (int i = 0; i < wordChosen.Length; i++) {
					// If input equal to char of chosen word determined by i, change corresponding char in dashes[]
					if (wordChosen[i] == userGuess){
						dashes[i] = userGuess;

						// If the user guessed correctly, we'll set bool this to true.
						containsChar = true;
					}					
				}

				// See if word contains char at all, subtract incorrect guesses as needed
				if (!containsChar)
					guessesLeft -= 1;

				// Convert char array to string so we can compare it to the chosen word
				dashesToString = new string(dashes);

				// if guessing is complete or if you've run out of guesses, break loop
				if (dashesToString == wordChosen) {
					userWon = true;
					breakpointReached = true;
				} else if (guessesLeft == 0) {
					userWon = false;
					breakpointReached = true;
				}

				Console.Clear();
				
			}

			if (userWon) {
				Console.WriteLine("Congratulations! The word was: " + wordChosen + ". You had " + guessesLeft + " incorrect guesses left.\n\nPress any key to exit.");
			} else {
				Console.WriteLine("Sorry, you've run out of incorrect guesses. The word was: " + wordChosen + ".\n\nPress any key to exit.");
			}

			Console.ReadKey();

			Exit(0);
		}

		public static void Exit(int code) {
			System.Environment.Exit(code);
		}
	}
}