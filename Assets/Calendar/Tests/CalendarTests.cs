using NUnit.Framework;
using baskorp.Calendars.Runtime;

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
    }
}