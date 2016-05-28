using System.Collections.Generic;
using NUnit.Framework;

namespace ical.net.UnitTests
{
    [TestFixture]
    class FrequencyUtilTests
    {
        //"Secondly, Minutely, Hourly, Daily, Weekly, Monthly, Yearly"
        [Test, TestCaseSource(nameof(FrequencyContainsTestCases))]
        public bool FrequencyContains(Frequency frequency)
        {
            return FrequencyUtil.Contains(frequency);
        }

        public IEnumerable<ITestCaseData> FrequencyContainsTestCases()
        {
            const Frequency fakeFreq = (Frequency)100;
            yield return new TestCaseData(fakeFreq).Returns(false).SetName("Integer outside the Frequency enum returns false");
            yield return new TestCaseData(Frequency.Secondly).Returns(true).SetName("Valid frequency return true");
            yield return new TestCaseData((Frequency)3).Returns(true).SetName("Integer in the enum set returns true`");
        }

        [Test]
        public void CommaSeparatedFrequencyTests()
        {
            const string expected = "Secondly, Minutely, Hourly, Daily, Weekly, Monthly, Yearly";
            var actual = FrequencyUtil.CommaSeparatedFrequencies();
            Assert.AreEqual(expected, actual);
        }
    }
}
