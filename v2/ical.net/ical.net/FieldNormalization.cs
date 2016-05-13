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
            var firstChunk = summary
                .Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(w => !String.IsNullOrWhiteSpace(w));
            return firstChunk ?? string.Empty;
        }

        public static ISet<string> NormalizeCategories(IEnumerable<string> categories)
        {
            var cats = categories == null
                ? new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(categories, StringComparer.OrdinalIgnoreCase);
            return cats.ToImmutableSortedSet();
        } 
    }
}
