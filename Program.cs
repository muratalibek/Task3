using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check if there are at least 3 arguments, the number of arguments is odd, and all arguments are unique.
            if (args.Length < 3 || args.Length % 2 == 0 || args.Distinct().Count() != args.Length)// if condition accepts for the odd number < 3 inside the Main args
            {
                Console.WriteLine("Error: Invalid arguments. Please enter an odd number of unique strings separated by spaces.");
                Console.WriteLine("Example: dotnet run rock paper scissors lizard spock phoenix match");
                return;
            }

            // Generate a random 32-byte key.
            using (var rng = RandomNumberGenerator.Create())
            {
                var key = new byte[32];// a new array with the name "key".
                rng.GetBytes(key);// The object "rgn" gets a 32-bytes key, which is then used as a key in the HMAC-SHA256 algorithm for message authentication in the program.

                // Generate a random computer move index.
                var computerMoveIndex = new Random().Next(args.Length);
                var computerMove = args[computerMoveIndex];

                // Compute the HMAC for the computer move using the generated key.
                var hmac = ComputeHmac(key, computerMove);

                Console.WriteLine($"HMAC: {BitConverter.ToString(hmac).Replace("-", "")}");

                while (true)
                {
                    Console.WriteLine("Available moves:");
                    // Display the available moves for the user
                    for (int i = 0; i < args.Length; i++)
                    {
                        Console.WriteLine($"{i + 1} - {args[i]}");
                    }
                    Console.WriteLine("0 - Exit");
                    Console.WriteLine("? - help");

                    int userMoveIndex;
                    while (true)
                    {
                        Console.Write("Enter your move: ");
                        // Get the user's move index.
                        if (!int.TryParse(Console.ReadLine(), out userMoveIndex) || userMoveIndex < 0 || userMoveIndex > args.Length)
                        {
                            Console.WriteLine("Invalid input. Please enter a number between 0 and {0}.", args.Length);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (userMoveIndex == 0)
                    {
                        break;
                    }

                    // Get the user's move and compute the game result.
                    var userMove = args[userMoveIndex - 1];
                    var result = GetGameResult(args, computerMoveIndex, userMoveIndex - 1);

                    Console.WriteLine($"Your move: {userMove}");
                    Console.WriteLine($"Computer's move: {computerMove}");

                    // Display the game result.
                    if (result == 0)
                    {
                        Console.WriteLine("Tie!");
                    }
                    else if (result == 1)
                    {
                        Console.WriteLine("You win!");
                    }
                    else
                    {
                        Console.WriteLine("Computer wins!");
                    }

                    Console.WriteLine($"HMAC key: {BitConverter.ToString(key).Replace("-", "")}\n");
                }
            }
        }

        // Compute the HMAC-SHA256 for the given key and message.
        static byte[] ComputeHmac(byte[] key, string message)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
        }

        // Compute the game result for the given moves, computer move index, and user move index.
        // Return 0 for tie, 1 for user win, and -1 for computer win.
        static int GetGameResult(string[] moves, int computerMoveIndex, int userMoveIndex)
        {
            int halfLength = moves.Length / 2;
            // Compute the distance between the computer and user moves.
            int distance = (userMoveIndex - computerMoveIndex + moves.Length) % moves.Length;
            if (distance == 0)
            {
                return 0;
            }
            else if (distance <= halfLength)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
