using System;

namespace Serov.Nsudotnet.Calendar {
    public class Calendar {
        private DateTime _dateTime;

        public Calendar(DateTime dateTime) {
            _dateTime = dateTime;
        }

        public void Print() {
            var knownWeekDay = new DateTime(2017, 03, 06);
            for (var i = 0; i < 7; i++) {
                Console.Write("  {0}", knownWeekDay.ToString("ddd").Substring(0, 2));
                knownWeekDay = knownWeekDay.AddDays(1);
            }
            Console.WriteLine();

            var workdays = 0;
            var cur = new DateTime(_dateTime.Year, _dateTime.Month, 1);
            for (var i = 0; i < (cur.DayOfWeek == DayOfWeek.Sunday ? 6 : (int) cur.DayOfWeek - 1); i++) {
                Console.Write("    ");
                if (cur.DayOfWeek != DayOfWeek.Saturday && cur.DayOfWeek != DayOfWeek.Sunday) {
                    workdays++;
                }
            }
            while (cur.Month == _dateTime.Month) {
                if (cur.DayOfWeek == DayOfWeek.Saturday || cur.DayOfWeek == DayOfWeek.Sunday) {
                    Console.BackgroundColor = ConsoleColor.Red;
                } else {
                    workdays++;
                }
                if (cur.Year == _dateTime.Year && cur.DayOfYear == _dateTime.DayOfYear) {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                if (cur.Year == DateTime.Now.Year && cur.DayOfYear == DateTime.Now.DayOfYear) {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.Write("  {0, 2}", cur.Day);
                if (cur.DayOfWeek == DayOfWeek.Sunday) {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                }
                Console.BackgroundColor = ConsoleColor.Black;
                cur = cur.AddDays(1);
            }
            Console.WriteLine();

            Console.WriteLine($"Workdays: {workdays}");
        }
    }
}