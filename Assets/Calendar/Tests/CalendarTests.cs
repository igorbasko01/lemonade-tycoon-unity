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
    }
}