# Program Function Analysis

The program begins by grabbing the response of the url "https://random-word-api.herokuapp.com/word?number=1". The response is returned as a json object in the form "["word"]". Rather than attempt to parse this object, it is easier and more efficient to just trim off the unnecessary characters using a character array and `string.Trim`. We can store the trimmed string to another string variable, `wordChosen` We now have our word.

To play the game, we need to have placeholder text to represent the words we have not guessed, and we need to change the dashes as the use guesses the letters. Individual characters cannot be changed like `string[x] = 0;` in C#, so the easiest way to accomplish this is using a char array. We can create this using the statement: `char[] dashes = new char[wordChosen.Length];`. However, we cannot compare a char array to a string, so we need to convert it to a string so we can compare it. This is trivial. `string dashesToString = new string(dashes);` We now have our dashes prepared.

Some more things we need to do are define a few variables, like the string we utilize for console input (`charBeforeParse`), the char we use to parse it (`userGuess`), and the incorrect guesses remaining (`guessesLeft`). We'll set everything here to null to avoid any unnecessary exceptions, except for the incorrect guesses remaining integer, which we'll set to 10.

The last thing my program does before we begin the while loop is search the word for the characters RSTLNE and replace them as necessary. We can accomplish this using this chunk of code:

```cs
string rstlne = "rstlne"

for (int i = 0; i < rstlne.Length; i++) {
    
    for (int x = 0; i < wordChosen.Length; x++) {
        
        if (rstlne[i] = wordChosen[x]) {
            
            dashes[x] = rstlne[i];
        
        }
    
    }

}

dashesToString = new string(dashes);
```

For every letter in the string RSTLNE, we will check every letter in the word chosen for said word. Compare, replace if necessary.

This is the last step of prep before we prompt the user for input, and display the dashes the way they are.

When running a stopwatch class to calculate the time elapsed, it shows approximately 818ms to perform all of the tasks above, most of which I presume is occupied by the `client.GetAsync`

After this, we can begin our while loop. I just use a `while (true)` and then break when a certain condition is met. I do this because I don't want the while loop running all the way through when a user guesses the word correctly.

The first thing we do is grab the user input using `Console.ReadLine()` because I want the player to have the option of typing in the entire word and guessing it then, and also to exit by typing "exit". We can accomplish this using this chunk of code:

```cs
charBeforeParse = Console.ReadLine();

if (guessBeforeParse == wordChosen) {
    
    dashesToString = wordChosen;
    break;

} else if (charBeforeParse == "exit") {
    
    System.Environment.Exit(0);

} else if (charBeforeParse.Length != 1) {
    
    Console.Clear()
    Console.WriteLine("Please enter one character!");
    continue;

}

userGuess = char.Parse(guessBeforeParse);
```

The idea here is to enable the user to perform multiple actions from the terminal whilst preventing `char.Parse` from throwing an exception.

If the user guesses the entire word correctly, we set the dashesToString var equal to the wordChosen (you'll see why this is important later) and break the loop.

If the user types exit, exit the program with code 0.

If the user types anything other than the above and the length of the string is not equal to 1, inform the user and loop back.

Now we compute the dashes. We do this in a similar fashion to the RSTLNE method from before.

```cs
for (int i = 0; i < wordChosen.Length; i++) {
    if (wordChosen[i] == userGuess) 
        dashes[i] = userGuess;
}

dashesToString = new string(dashes);
```

To determine whether or not to subtract from the guess count, we check to see whether the user-inputted character exists in the string at all.
```cs
if (!wordChosen.Contains(userGuess)) 
    guessesLeft -= 1;
```

Now, if the user has run out of guesses *or* the word has been guessed, break.
```cs
if (dashesToString == wordChosen || guessesLeft == 0) break;
```

Now we clear the console screen, display the dashes thus far, and display the amount of incorrect guesses left, and loop back because we've reached the end of the while loop.

But what if the while loop is broken?

There's one final bit of code outside the while loop that we run if this happens.

If the user has sucessfully guessed the word, show the final word, congratulate the user.

If else, show the final word to the user and say that they ran out of guesses.