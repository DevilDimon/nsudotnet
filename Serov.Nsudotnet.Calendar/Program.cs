using System;

namespace Serov.Nsudotnet.Calendar {
    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Enter a date:");
            DateTime dateTime;

            var parsed = DateTime.TryParse(Console.ReadLine(), out dateTime);
            if (!parsed) {
                Console.WriteLine("Wrong date format. Terminating...");
                return;
            }

            var calendar = new Calendar(dateTime);
            Console.WriteLine(calendar);
        }
    }
}