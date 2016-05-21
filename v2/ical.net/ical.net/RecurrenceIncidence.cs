using System;
using NodaTime;

namespace ical.net
{
    public class RecurrenceIncidence
    {
        public string ParentUuid { get; private set; }
        private readonly Guid _uuid;
        public string Uuid => _uuid.ToString();
        public ZonedDateTime Start { get; private set; }
        public ZonedDateTime End { get; private set; }

        public RecurrenceIncidence(string parentUuid, ZonedDateTime start, ZonedDateTime end)
        {
            ParentUuid = parentUuid;
            Start = start;
            End = end;
            _uuid = Guid.NewGuid();
        }

        public bool IsEquivalentTo(RecurrenceIncidence other) => other.Start == Start && other.End == End;

        protected bool Equals(RecurrenceIncidence other)
        {
            return _uuid.Equals(other._uuid)
                && string.Equals(ParentUuid, other.ParentUuid)
                && Start.Equals(other.Start)
                && End.Equals(other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((RecurrenceIncidence) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _uuid.GetHashCode();
                hashCode = (hashCode * 397) ^ (ParentUuid != null
                    ? ParentUuid.GetHashCode()
                    : 0);
                hashCode = (hashCode * 397) ^ Start.GetHashCode();
                hashCode = (hashCode * 397) ^ End.GetHashCode();
                return hashCode;
            }
        }
    }
}