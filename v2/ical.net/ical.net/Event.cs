using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace ical.net
{
    /// <summary>
    /// A model of the VEVENT
    /// </summary>
    public class Event
    {
        public string Uuid { get; private set; }
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
        public RecurrenceRule Rrule { get; private set; }
        public bool IsRecurring => Rrule != null;

        private Event(string uuid, ZonedDateTime start, ZonedDateTime createdAt, string summary, string classification, IEnumerable<string> categories)
        {
            if (start.TimeOfDay != LocalTime.Midnight)
            {
                throw new ArgumentException("An event without a DtEnd property must not use times");
            }

            Uuid = FieldNormalization.NormalizeOrCreateUuid(uuid);

            DtStamp = createdAt == default(ZonedDateTime)
                ? NodaUtilities.NowTimeWithSystemTimeZone()
                : createdAt;

            DtStart = start;
            var oneTickBeforeMidnight = Duration.FromStandardDays(1).Minus(Duration.FromTicks(1));
            DtEnd = ZonedDateTime.Add(start, oneTickBeforeMidnight);
            Summary = FieldNormalization.NormalizeSummary(summary);
            Classification = FieldNormalization.NormalizeClassification(classification);
            Categories = FieldNormalization.NormalizeCategories(categories);
        }

        public static Event GetSingleDayEvent(string uuid, ZonedDateTime start, ZonedDateTime createdAt, string summary, string classification = "",
            IEnumerable<string> categories = null)
        {
            return new Event(uuid, start, createdAt, summary, classification, categories);
        }

        public static Event GetSingleDayEvent(ZonedDateTime start, ZonedDateTime createdAt, string summary, string classification = "",
            IEnumerable<string> categories = null)
        {
            return GetSingleDayEvent(string.Empty, start, createdAt, summary, classification, categories);
        }

        private Event(string uuid, ZonedDateTime start, ZonedDateTime end, ZonedDateTime createdAt, string summary = "", string classification = "",
            IEnumerable<string> categories = null)
        {
            if (end <= start)
            {
                throw new ArgumentException($"Event start ({start}) must come before event end ({end})");
            }

            Uuid = FieldNormalization.NormalizeOrCreateUuid(uuid);

            DtStamp = createdAt == default(ZonedDateTime)
                ? NodaUtilities.NowTimeWithSystemTimeZone()
                : createdAt;

            DtStart = start;
            DtEnd = end;
            Summary = FieldNormalization.NormalizeSummary(summary);
            Classification = FieldNormalization.NormalizeClassification(classification);
            Categories = FieldNormalization.NormalizeCategories(categories);
        }

        public static Event CreateEvent(string uuid, ZonedDateTime start, ZonedDateTime end, ZonedDateTime createdAt = default(ZonedDateTime), string summary = "",
            string classification = "", IEnumerable<string> categories = null)
        {
            return new Event(uuid, start, end, createdAt, summary, classification, categories);
        }

        public static Event CreateEvent(ZonedDateTime start, ZonedDateTime end, ZonedDateTime createdAt = default(ZonedDateTime), string summary = "", string classification = "",
            IEnumerable<string> categories = null)
        {
            return CreateEvent(string.Empty, start, end, createdAt, summary, classification, categories);
        }

        public IEnumerable<RecurrenceIncidence> GetRecurrences()
        {
            if (!IsRecurring)
            {
                return Enumerable.Empty<RecurrenceIncidence>();
            }
            //return recurrenceCalculator.Something?

            throw new NotImplementedException();
        }

        public bool EventRecursInRange(ZonedDateTime start, ZonedDateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
