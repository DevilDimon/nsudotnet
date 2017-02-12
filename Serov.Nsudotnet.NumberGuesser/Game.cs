using System;

namespace Serov.Nsudotnet.NumberGuesser {
    public class Game {

        public enum Result {
            More, Less, Equal
        }

        private int _number;

        public int Tries { get; private set; }
        public string UserName { get; set; }
        public int[] GuessHistory { get; } = new int[1000];
        public Result[] ResultHistory { get; } = new Result[1000];
        public DateTime StartTime { get; private set; }

        public Game() {
            var random = new Random();
            _number = random.Next(101);
            StartTime = DateTime.Now;
        }

        public Result CheckGuess(int guess) {
            GuessHistory[Tries] = guess;
            Result result;
            if (guess < _number) {
                result = Result.Less;
            } else if (guess > _number) {
                result = Result.More;
            } else {
                result = Result.Equal;
            }

            ResultHistory[Tries] = result;
            Tries++;
            return result;
        }
    }
}