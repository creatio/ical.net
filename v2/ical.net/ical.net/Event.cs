using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;

namespace ical.net
{
    /// <summary>
    /// A model of the VEVENT
    /// </summary>
    public class Event
    {
        public string Uid { get; private set; }
        public ZonedDateTime DtStamp { get; private set; }
        /// <summary>
        /// Inclusive start of the event. In the event that this represents a date without a time, AND no DtEnd property, the event's non-
        /// inclusive end is the end of the calendar date specified by this DtStart property. In effect, the end is 1 tick before midnight.
        /// </summary>
        public ZonedDateTime DtStart { get; private set; }
        /// <summary>
        /// Non-inclusive end of the event
        /// </summary>
        public ZonedDateTime DtEnd { get; private set; }
        public Duration Duration => DtEnd.ToInstant() - DtStart.ToInstant();

        /// <summary>
        /// A one-line summary of the event that corresponds to the SUMMARY icalendar property. Text after the first line break is ignored.
        /// </summary>
        public string Summary { get; private set; }
        /// <summary>
        /// A short description of the event's classification, most often used to describe visibility. Corresponds to the CLASS property.
        /// </summary>
        public string Classification { get; private set; }
        public ISet<string> Categories { get; private set; } 

        private Event(string uid, ZonedDateTime dtStamp, ZonedDateTime dtStart, string summary, string classification = Visibility.Public, IEnumerable<string> categories = null)
        {
            if (dtStart.TimeOfDay != LocalTime.Midnight)
            {
                throw new ArgumentException("An event without a DtEnd property must not use times");
            }

            Uid = uid;
            DtStamp = dtStamp;
            DtStart = dtStart;
            var oneTickBeforeMidnight = Duration.FromStandardDays(1).Minus(Duration.FromTicks(1));
            DtEnd = ZonedDateTime.Add(dtStart, oneTickBeforeMidnight);
            Summary = FieldNormalization.NormalizeSummary(summary);
            Classification = classification;
            Categories = FieldNormalization.NormalizeCategories(categories);
        }

        public static Event GetSingleDayEvent(string uid, ZonedDateTime dtStamp, ZonedDateTime dtStart, string summary, string classification = Visibility.Public)
        {
            return new Event(uid, dtStamp, dtStart, summary, classification);
        }

        public static Event GetSingleDayEvent(ZonedDateTime dtStamp, ZonedDateTime dtStart, string summary, string classification = Visibility.Public)
        {
            return GetSingleDayEvent(Guid.NewGuid().ToString(), dtStamp, dtStart, summary, classification);
        }

        public static Event GetSingleDayEvent(ZonedDateTime dtStart, string summary, string classification = Visibility.Public)
        {
            return GetSingleDayEvent(NodaUtilities.NowTimeWithSystemTimeZone(), dtStart);
        }

        private Event(string uid, ZonedDateTime dtStamp, ZonedDateTime dtStart, ZonedDateTime dtEnd, string summary, string classification = Visibility.Public, IEnumerable<string> categories)
        {
            if (dtEnd <= dtStart)
            {
                throw new ArgumentException($"Event start ({dtStart}) must come before event end ({dtEnd})");
            }

            Uid = uid;
            DtStamp = dtStamp;
            DtStart = dtStart;
            DtEnd = dtEnd;
            Summary = FieldNormalization.NormalizeSummary(summary);
            Classification = classification;
            Categories = FieldNormalization.NormalizeCategories(categories);
        }

        public static Event GetEvent(string uid, ZonedDateTime dtStamp, ZonedDateTime dtStart, ZonedDateTime dtEnd, string summary, string visibility = Visibility.Public)
        {
            return new Event(uid, dtStamp, dtStart, dtEnd, summary, visibility);
        }

        public static Event GetEvent(ZonedDateTime dtStamp, ZonedDateTime dtStart, ZonedDateTime dtEnd, string summary, string visibility = Visibility.Public)
        {
            return GetEvent(Guid.NewGuid().ToString(), dtStamp, dtStart, dtEnd, summary, visibility);
        }

        public static Event GetEvent(ZonedDateTime dtStart, ZonedDateTime dtEnd, string summary, string visibility = Visibility.Public)
        {
            return GetEvent(NodaUtilities.NowTimeWithSystemTimeZone(), dtStart, dtEnd, summary, visibility);
        }
    }
}
