using System.Collections.Generic;
using System.Linq;

namespace baskorp.Calendars.Runtime
{
    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    
    public class Date
    {
        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public WeekDay WeekDay => (WeekDay) ((_daysInYear * (Year - 1) + SumOfDaysUpToMonth(Month) + (Day - 1)) % 7);
        public static readonly Dictionary<int, int> DaysInMonth = new()
        {
            {1, 31},
            {2, 28},
            {3, 31},
            {4, 30},
            {5, 31},
            {6, 30},
            {7, 31},
            {8, 31},
            {9, 30},
            {10, 31},
            {11, 30},
            {12, 31}
        };
        private static readonly int _daysInYear = DaysInMonth.Values.Sum();
        private static int SumOfDaysUpToMonth(int month)
        {
            return DaysInMonth.Values.Take(month - 1).Sum();
        }
        

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }

    /**
     * A simple calendar class that stores the current date, and doesn't handle leap years.
     */
    public class Calendar
    {
        private Date _date;
        public Date CurrentDate => _date;
        public Calendar()
        {
            _date = new Date(1, 1, 1);
        }

        public Calendar(int day, int month, int year)
        {
            if (!IsValidDate(day, month, year))
            {
                throw new System.ArgumentException("Invalid date");
            }
            _date = new Date(day, month, year);
        }

        public void ProgressOneDay()
        {
            if (_date.Day == 31 && _date.Month == 12)
            {
                _date = new Date(1, 1, _date.Year + 1);
            }
            else if (_date.Day == Date.DaysInMonth[_date.Month])
            {
                _date = new Date(1, _date.Month + 1, _date.Year);
            }
            else
            {
                _date = new Date(_date.Day + 1, _date.Month, _date.Year);
            }
        }

        private bool IsValidDate(int day, int month, int year)
        {
            if (day <= 0 || month <= 0 || year <= 0)
            {
                return false;
            }
            if (month > 12)
            {
                return false;
            }
            if (day > 31)
            {
                return false;
            }
            if (month == 2 && day > 28)
            {
                return false;
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
            {
                return false;
            }
            return true;
        }
    }
}