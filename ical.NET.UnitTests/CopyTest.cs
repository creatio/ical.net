using System.Collections.Generic;
using System.IO;
using System.Text;
using Ical.Net;
using Ical.Net.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ical.NET.UnitTests
{
    [TestFixture]
    public class CopyTest
    {
        [Test, TestCaseSource(nameof(CopyCalendarTest_TestCases)), Category("Copy tests")]
        public void CopyCalendarTest(string calendarString)
        {
            var iCal1 = Calendar.LoadFromStream(new StringReader(calendarString))[0];
            var iCal2 = iCal1.Copy<ICalendar>();
            SerializationTest.CompareCalendars(iCal1, iCal2);
        }

        public static IEnumerable<ITestCaseData> CopyCalendarTest_TestCases()
        {
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Attachment3)).SetName("Attachment3");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Bug2148092)).SetName("Bug2148092");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.CaseInsensitive1)).SetName("CaseInsensitive1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.CaseInsensitive2)).SetName("CaseInsensitive2");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.CaseInsensitive3)).SetName("CaseInsensitive3");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Categories1)).SetName("Categories1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Duration1)).SetName("Duration1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Encoding1)).SetName("Encoding1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Event1)).SetName("Event1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Event2)).SetName("Event2");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Event3)).SetName("Event3");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Event4)).SetName("Event4");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.GeographicLocation1)).SetName("GeographicLocation1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Language1)).SetName("Language1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Language2)).SetName("Language2");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.Language3)).SetName("Language3");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.TimeZone1)).SetName("TimeZone1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.TimeZone2)).SetName("TimeZone2");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.TimeZone3)).SetName("TimeZone3");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.XProperty1)).SetName("XProperty1");
            yield return new TestCaseData(Encoding.UTF8.GetString(IcsFiles.XProperty2)).SetName("XProperty2");
        }
    }
}
