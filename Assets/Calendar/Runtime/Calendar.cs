namespace baskorp.Calendars.Runtime
{
    public class Date
    {
        public int Day { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }
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
            _date = new Date(day, month, year);
        }
    }
}