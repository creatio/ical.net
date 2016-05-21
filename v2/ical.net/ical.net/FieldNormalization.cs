using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ical.net
{
    public static class FieldNormalization
    {
        public static string NormalizeSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
            {
                return string.Empty;
            }

            var firstChunk = summary
                .Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(w => !String.IsNullOrWhiteSpace(w));
            return firstChunk ?? string.Empty;
        }

        public static ISet<string> NormalizeCategories(IEnumerable<string> categories)
        {
            return categories == null || !categories.Any()
                ? ImmutableSortedSet<string>.Empty
                : new HashSet<string>(categories, StringComparer.OrdinalIgnoreCase).ToImmutableSortedSet();
        }

        public static string NormalizeClassification(string classification)
        {
            return string.IsNullOrWhiteSpace(classification)
                ? string.Empty
                : classification.Trim();
        }

        public static string NormalizeOrCreateUuid(string uuid)
        {
            return string.IsNullOrWhiteSpace(uuid)
                ? Guid.NewGuid().ToString()
                : uuid.Trim();
        }
    }
}
