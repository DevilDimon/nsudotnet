using System;
using System.Globalization;
using System.Text;
using System.Threading;

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

            var cur = new DateTime(_dateTime.Year, _dateTime.Month, 1);
            for (var i = 0; i < (cur.DayOfWeek == DayOfWeek.Sunday ? 6 : (int) cur.DayOfWeek - 1); i++) {
                sb.Append("    ");
            }
            while (cur.Month == _dateTime.Month) {
                sb.AppendFormat("  {0, 2}", cur.Day);
                if (cur.DayOfWeek == DayOfWeek.Sunday) {
                    sb.Append(Environment.NewLine);
                }
                cur = cur.AddDays(1);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}