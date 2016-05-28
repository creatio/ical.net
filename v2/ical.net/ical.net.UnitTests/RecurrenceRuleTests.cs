using System;
using System.Collections.Generic;
using NodaTime;
using NUnit.Framework;

namespace ical.net.UnitTests
{
    [TestFixture]
    class RecurrenceRuleTests
    {
        [Test, TestCaseSource(nameof(UntilConstructorTestCases))]
        public bool UntilConstructorTests(Frequency frequency, int interval, ZonedDateTime until)
        {
            var rrule = new RecurrenceRule(frequency, interval, until);
            Assert.IsNotNull(rrule);
            return true;
        }

        public IEnumerable<ITestCaseData> UntilConstructorTestCases()
        {
            var now = ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now);
            yield return new TestCaseData(Frequency.Daily, 5, now).Returns(true).SetName("All good values are OK");
            yield return new TestCaseData((Frequency)int.MaxValue, -5, now).Throws(typeof(ArgumentException)).SetName("Bad Frequency and interval throw ArgumentException");
            yield return new TestCaseData((Frequency)int.MaxValue, 5, 5).Throws(typeof(ArgumentException)).SetName("Bad Frequency throws ArgumentException");
            yield return new TestCaseData(Frequency.Daily, -5, 5).Throws(typeof(ArgumentException)).SetName("Bad interval throws ArgumentException");
        }

        [Test, TestCaseSource(nameof(CountConstructorTestCases))]
        public bool CountConstructorTests(Frequency frequency, int interval, long count)
        {
            var rrule = new RecurrenceRule(frequency, interval, count);
            Assert.IsNotNull(rrule);
            return true;
        }

        public IEnumerable<ITestCaseData> CountConstructorTestCases()
        {
            yield return new TestCaseData(Frequency.Daily, 5, 5).Returns(true).SetName("All good values are OK");
            yield return new TestCaseData((Frequency)int.MaxValue, -5, -5).Throws(typeof(ArgumentException)).SetName("All bad values throw ArgumentException");
            yield return new TestCaseData((Frequency)int.MaxValue, 5, 5).Throws(typeof(ArgumentException)).SetName("Bad Frequency throws ArgumentException");
            yield return new TestCaseData(Frequency.Daily, -5, 5).Throws(typeof(ArgumentException)).SetName("Bad interval throws ArgumentException");
            yield return new TestCaseData(Frequency.Daily, 5, -5).Throws(typeof(ArgumentException)).SetName("Bad count throws ArgumentException");
        }
    }
}
