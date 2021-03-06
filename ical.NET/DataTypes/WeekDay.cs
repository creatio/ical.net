using System;
using System.IO;
using Ical.Net.Interfaces.DataTypes;
using Ical.Net.Interfaces.General;
using Ical.Net.Serialization.iCalendar.Serializers.DataTypes;

namespace Ical.Net.DataTypes
{
    /// <summary>
    /// Represents an RFC 5545 "BYDAY" value.
    /// </summary>
    [Serializable]
    public class WeekDay : EncodableDataType, IWeekDay
    {
        private int _mNum = int.MinValue;
        private DayOfWeek _mDayOfWeek;

        public virtual int Offset
        {
            get { return _mNum; }
            set { _mNum = value; }
        }

        public virtual DayOfWeek DayOfWeek
        {
            get { return _mDayOfWeek; }
            set { _mDayOfWeek = value; }
        }

        public WeekDay()
        {
            Offset = int.MinValue;
        }

        public WeekDay(DayOfWeek day) : this()
        {
            DayOfWeek = day;
        }

        public WeekDay(DayOfWeek day, int num) : this(day)
        {
            Offset = num;
        }

        public WeekDay(DayOfWeek day, FrequencyOccurrence type) : this(day, (int) type) {}

        public WeekDay(string value)
        {
            var serializer = new WeekDaySerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }

        public override bool Equals(object obj)
        {
            if (obj is WeekDay)
            {
                var ds = (WeekDay) obj;
                return ds.Offset == Offset && ds.DayOfWeek == DayOfWeek;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Offset.GetHashCode() ^ DayOfWeek.GetHashCode();
        }

        public override void CopyFrom(ICopyable obj)
        {
            base.CopyFrom(obj);
            if (obj is IWeekDay)
            {
                var bd = (IWeekDay) obj;
                Offset = bd.Offset;
                DayOfWeek = bd.DayOfWeek;
            }
        }

        public int CompareTo(object obj)
        {
            IWeekDay bd = null;
            if (obj is string)
            {
                bd = new WeekDay(obj.ToString());
            }
            else if (obj is IWeekDay)
            {
                bd = (IWeekDay) obj;
            }

            if (bd == null)
            {
                throw new ArgumentException();
            }
            var compare = DayOfWeek.CompareTo(bd.DayOfWeek);
            if (compare == 0)
            {
                compare = Offset.CompareTo(bd.Offset);
            }
            return compare;
        }
    }
}