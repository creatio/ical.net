using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NodaTime;

namespace ical.net
{
    internal class RecurrenceCalculator
    {
        private readonly ZonedDateTime _startTime;
        private readonly RecurrenceRule _repetitionRules;
        private readonly RecurrenceRule _exceptions;
        public RecurrenceCalculator(ZonedDateTime startTime, RecurrenceRule repetitionRepetitionRules, RecurrenceRule exceptions)
        {
            _startTime = startTime;
            _repetitionRules = repetitionRepetitionRules;
            _exceptions = exceptions;
        }

        public RecurrenceCalculator(ZonedDateTime startTime, RecurrenceRule repetitionRepetitionRules) : this(startTime, repetitionRepetitionRules, null) {}

        /// <summary>
        /// Returns an ordered collection of recurrence incidents, from soonest in time to latest in time. (Note: this method is lazy; you may with to memoize
        /// the values it returns instead of continually re-computing them.)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RecurrenceIncidence> GetRecurrences()
        {
            throw new NotImplementedException();
        }
    }
}
