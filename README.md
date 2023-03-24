TASK #3
--------
Requirements:
Using the language of your choice — C# — write a script that implements a generalized rock-paper-scissors game (with the supports of arbitrary odd number of arbitrary combinations).
When launched with command line parameters (arguments to the Main method in the case of C#) it accepts an odd number >=3 non-repeating strings (if the arguments are incorrect, you must display a neat error message - what exactly is wrong and an example of how to do it right). All messages should be in English. These passed strings are moves (for example, Rock Paper Scissors or Rock Paper Scissors Lizard Spock or 1 2 3 4 5 6 7 8 9).

Important: moves are passed as command line arguments, you don't parse them from the input stream (for example, a move may contain a space, but it shouldn't matter to your code).
The victory is defined as follows - half of the next moves in the circle wins, half of the previous moves in the circle lose (the semantics of the strings-moves is not important, he plays by the rules build upon the moves order the user used, even if the stone loses to scissors in its order - the contents of the strings-moves are not important for you).
The script generates a cryptographically strong random key (SecureRandom, RandomNumberGenerator, etc. - mandatory!) with a length of at least 256 bits, makes computes move, calculates HMAC (based on SHA2 or SHA3) from the own move with the generated key, displayed the HMAC to the user. After that the user gets "menu" 1 - Stone, 2 - Scissors, ...., 0 - Exit. The user makes his choice (in case of incorrect input, the "menu" is displayed again). The script shows who won, the move of the computer and the original key.

Re-read the paragraph above, the sequence is critical (it simply doesn't make sense to do it differently, for example, showing the key before the user's turn or HMAC instead of the key).
Thus the user can check that the computer plays fair (did not change its move after the user's move).

When you select the "help" option in the terminal, you need to display a table (ASCII-graphic) that determines which move wins.

The table generation should be in a separate class, the definition of the "rules" who won should be in a separate class, the key generation and HMAC functions should be in a separate class (at least 4 classes in total). You should use the core class libraries and third-party libraries to the maximum, and not reinvent the wheel. Help should be formatted as an N + 1 by N + 1 table, where N is the number of moves (determined by the number of arguments passed to the script). +1 to add a title for the rows and a title for the columns (contain the title of the move). Cells can contain Win/Lose/Draw.

THE NUMBER OF MOVES CAN BE ARBITRARY (odd and > 1, depending on the passed parameters), it is not hardwired into the code. 
Example:

java -jar game.jar rock paper scissors lizard Spock
HMAC: FAAC40C71B4B12BF0EF5556EEB7C06925D5AE405D447E006BB8A06565338D411
Available moves:
1 - rock
2 - paper
3 - scissors
4 - lizard
5 - Spock
0 - exit
? - help
Enter your move: 2
Your move: paper
Computer move: rock
You win!
HMAC key: BD9BE48334BB9C5EC263953DA54727F707E95544739FCE7359C267E734E380A2

--------

The steps of the script:
1. Inside the class, the Main method is defined with the args parameter.
2. The program checks whether the number of arguments passed through the command line is valid, i.e., whether there are at least 3 arguments, the number of arguments is    odd, and all arguments are unique. If the condition is not satisfied, an error message is displayed, and the program exits.
3. If the arguments are valid, the program generates a 32-byte random key using the RandomNumberGenerator class.
4. The program generates a random number between 0 and the number of arguments minus one, and uses it to select the computer's move from the list of available moves.
5. The program uses the generated key to compute the HMAC-SHA256 for the computer's move.
6. The program enters an infinite loop to allow the user to play the game multiple times.
7. Inside the loop, the program displays the available moves for the user and prompts them to enter their move.
8. The program validates the user's input and retrieves their move index.
9. If the user enters 0, the program exits the loop and the program ends.
10. If the user enters a valid move index, the program retrieves the corresponding move and computes the game result using the GetGameResult method.
11. The program displays the user's move and the computer's move, as well as the result of the game.
12. The program displays the HMAC key used for the game.
13. The loop repeats until the user decides to exit.
14. The program defines the ComputeHmac method, which computes the HMAC-SHA256 for a given key and message using the HMACSHA256 class.
15. The program defines the GetGameResult method, which computes the result of the game for a given set of moves, computer move index, and user move index. The method      returns 0 for tie, 1 for user win, and -1 for computer win.
