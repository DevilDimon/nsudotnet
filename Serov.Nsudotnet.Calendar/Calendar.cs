using System;
using System.Text;

namespace Serov.Nsudotnet.Calendar {
    public class Calendar {
        private DateTime _dateTime;

        public Calendar(DateTime dateTime) {
            _dateTime = dateTime;
        }

        public override string ToString() {
            var sb = new StringBuilder();

            var knownWeekDay = new DateTime(2017, 03, 06);
            for (var i = 0; i < 7; i++) {
                sb.AppendFormat("  {0}", knownWeekDay.ToString("ddd").Substring(0, 2));
                knownWeekDay = knownWeekDay.AddDays(1);
            }
            sb.Append(Environment.NewLine);

            var workdays = 0;
            var cur = new DateTime(_dateTime.Year, _dateTime.Month, 1);
            for (var i = 0; i < (cur.DayOfWeek == DayOfWeek.Sunday ? 6 : (int) cur.DayOfWeek - 1); i++) {
                sb.Append("    ");
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
                if (cur.Year == DateTime.Now.Year && cur.DayOfYear == DateTime.Now.Year) {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                sb.AppendFormat("  {0, 2}", cur.Day);
                if (cur.DayOfWeek == DayOfWeek.Sunday) {
                    sb.Append(Environment.NewLine);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                cur = cur.AddDays(1);
            }
            sb.Append(Environment.NewLine);

            sb.Append($"Workdays: {workdays}{Environment.NewLine}");
            return sb.ToString();
        }
    }
}