using System;

namespace Serov.Nsudotnet.NumberGuesser {
    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Enter your name:");

            var name = Console.ReadLine();
            var game = new Game {UserName = name};

            for (var i = 0; i < 1000; ++i) {
                Console.WriteLine("Guess the number!");
                Console.WriteLine("Enter your guess (0-100):");

                int guess;

                while (true) {
                    var input = Console.ReadLine();

                    if ("q" == input) {
                        Console.WriteLine("I am terribly sorry. I'll see myself out.");
                        return;
                    }

                    bool parseResult = int.TryParse(input, out guess);

                    if (parseResult && guess >= 0 && guess <= 100) {
                        break;
                    }
                    Console.WriteLine("Invalid number. Try again:");
                }

                var guessResult = game.CheckGuess(guess);

                switch (guessResult) {
                    case Game.Result.Less:
                    case Game.Result.More:
                        Console.WriteLine("WRONG! Your guess is {0} than the number.",
                            guessResult == Game.Result.Less ? "less" : "more");
                        if (i > 0 && i % 3 == 0) {
                            Console.WriteLine(RandomRemark(), game.UserName);
                        }
                        break;
                    case Game.Result.Equal:
                        Console.WriteLine("CORRECT! You have guessed the number!");
                        Console.WriteLine("You have used {0} tries:", game.Tries);
                        for (var j = 0; j < game.Tries; ++j) {
                            Console.WriteLine("Try #{0}: {1} {2}", j + 1, game.GuessHistory[j], game.ResultHistory[j]);
                        }
                        var timeSpan = DateTime.Now.Subtract(game.StartTime);
                        Console.WriteLine("Time elapsed: {0} minute(s)", Math.Truncate(timeSpan.TotalMinutes));
                        return;
                }
            }
        }

        private static string[] _scoldingRemarks = {
            "{0}, your guesses are bad and you should feel bad.",
            "Your mother, {0}, was a hamster and your father smelt of elderberries!",
            "You have my pity, {0}.",
            "I don't like your jerk off name, {0}.",
            "You, {0}, are an inhuman monster!"
        };

        private static string RandomRemark() {
            var index = new Random().Next(_scoldingRemarks.Length);
            return _scoldingRemarks[index];
        }
    }
}