using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ical.net
{
    public enum Frequency
    {
        Secondly,
        Minutely,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly,
    }

    public static class FrequencyUtil
    {
        private static readonly IList<Frequency> _frequencies = Enum.GetValues(typeof(Frequency)).Cast<Frequency>().ToList().AsReadOnly();
        private static readonly ISet<Frequency> _frequencySet = _frequencies.ToImmutableHashSet();

        public static bool Contains(Frequency frequency) => _frequencySet.Contains(frequency);

        public static void CheckFrequency(Frequency frequency)
        {
            if (!_frequencySet.Contains(frequency))
            {
                var msg = $"{frequency} is not a valid Frequency. Possible values are: {CommaSeparatedFrequencies()}";
                throw new ArgumentException(msg);
            }
        }

        public static string CommaSeparatedFrequencies()
        {
            var commasUntil = _frequencies.Count - 2;

            var builder = new StringBuilder();
            for (var i = 0; i < _frequencies.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(" ");
                }

                builder.Append(_frequencies[i]);
                if (i <= commasUntil)
                {
                    builder.Append(",");
                }
            }

            return builder.ToString();
        }
    }
}
