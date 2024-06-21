using NUnit.Framework;
using baskorp.Calendars.Runtime;
using NUnit.Framework.Internal;

namespace baskorp.Calendars.Tests
{
    [TestFixture]
    public class CalendarTests
    {
        [Test]
        public void Create_Calendar_Success()
        {
            var calendar = new Calendar();
            Assert.AreEqual(1, calendar.CurrentDate.Day);
            Assert.AreEqual(1, calendar.CurrentDate.Month);
            Assert.AreEqual(1, calendar.CurrentDate.Year);
            Assert.AreEqual(WeekDay.Monday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Create_Calendar_CustomDate_Success()
        {
            var calendar = new Calendar(1, 3, 1000);
            Assert.AreEqual(1, calendar.CurrentDate.Day);
            Assert.AreEqual(3, calendar.CurrentDate.Month);
            Assert.AreEqual(1000, calendar.CurrentDate.Year);
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDay_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(0, 3, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongMonth_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(1, 13, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDayInFebruary_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(30, 2, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDayInApril_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(31, 4, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDayInJune_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(31, 6, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDayInSeptember_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(31, 9, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_WrongDayInNovember_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(31, 11, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_DayLowerThan1_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(-1, 3, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_MonthLowerThan1_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(1, -1, 1000));
        }

        [Test]
        public void Create_Calendar_CustomDate_YearLowerThan1_Exception()
        {
            Assert.Throws<System.ArgumentException>(() => new Calendar(1, 3, -1));
        }

        [Test]
        public void Create_Calendar_CustomDate_WeekDay_Wednesday()
        {
            var calendar = new Calendar(3, 1, 1);
            Assert.AreEqual(WeekDay.Wednesday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Create_Calendar_CustomDate_WeekDay_Sunday()
        {
            var calendar = new Calendar(7, 1, 1);
            Assert.AreEqual(WeekDay.Sunday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Create_Calendar_CustomDate_WeekDay_MondayWeekLater()
        {
            var calendar = new Calendar(8, 1, 1);
            Assert.AreEqual(WeekDay.Monday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Calendar_ProgressOneDay_Success() {
            var calendar = new Calendar();
            calendar.ProgressOneDay();
            Assert.AreEqual(2, calendar.CurrentDate.Day);
            Assert.AreEqual(1, calendar.CurrentDate.Month);
            Assert.AreEqual(1, calendar.CurrentDate.Year);
            Assert.AreEqual(WeekDay.Tuesday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Calendar_ProgressOneDay_EndOfMonth_Success() {
            var calendar = new Calendar(31, 1, 1);
            calendar.ProgressOneDay();
            Assert.AreEqual(1, calendar.CurrentDate.Day);
            Assert.AreEqual(2, calendar.CurrentDate.Month);
            Assert.AreEqual(1, calendar.CurrentDate.Year);
            Assert.AreEqual(WeekDay.Thursday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Calendar_ProgressOneDay_EndOfYear_Success() {
            var calendar = new Calendar(31, 12, 1);
            calendar.ProgressOneDay();
            Assert.AreEqual(1, calendar.CurrentDate.Day);
            Assert.AreEqual(1, calendar.CurrentDate.Month);
            Assert.AreEqual(2, calendar.CurrentDate.Year);
            Assert.AreEqual(WeekDay.Tuesday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Calendar_ProgressOneDay_EndOfFebuary_Success() {
            var calendar = new Calendar(28, 2, 1);
            calendar.ProgressOneDay();
            Assert.AreEqual(1, calendar.CurrentDate.Day);
            Assert.AreEqual(3, calendar.CurrentDate.Month);
            Assert.AreEqual(1, calendar.CurrentDate.Year);
            Assert.AreEqual(WeekDay.Thursday, calendar.CurrentDate.WeekDay);
        }

        [Test]
        public void Calendar_SaturdayAndSunday_AreWeekends_Success() {
            var calendarSaturday = new Calendar(6, 1, 1);
            var calendarSunday = new Calendar(7, 1, 1);
            Assert.AreEqual(DayType.Weekend, calendarSaturday.CurrentDate.DayType);
            Assert.AreEqual(DayType.Weekend, calendarSunday.CurrentDate.DayType);
        }

        [Test]
        public void Calendar_NonWeekendDays_AreWeekdays_Success() {
            var calendarMonday = new Calendar(1, 1, 1);
            var calendarTuesday = new Calendar(2, 1, 1);
            var calendarWednesday = new Calendar(3, 1, 1);
            var calendarThursday = new Calendar(4, 1, 1);
            var calendarFriday = new Calendar(5, 1, 1);
            Assert.AreEqual(DayType.Weekday, calendarMonday.CurrentDate.DayType);
            Assert.AreEqual(DayType.Weekday, calendarTuesday.CurrentDate.DayType);
            Assert.AreEqual(DayType.Weekday, calendarWednesday.CurrentDate.DayType);
            Assert.AreEqual(DayType.Weekday, calendarThursday.CurrentDate.DayType);
            Assert.AreEqual(DayType.Weekday, calendarFriday.CurrentDate.DayType);
        }
    }
}