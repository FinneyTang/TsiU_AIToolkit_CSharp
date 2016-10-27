using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TsiU
{
    public struct TTimeRel
    {
        private const ulong S_TO_MS = 1000;
        private const float MS_TO_S = 0.001f;

        private ulong _time;

        public TTimeRel(float t)
        {
            _time = (ulong)(t * (float)S_TO_MS + 0.5f);
        }
        public TTimeRel(TTimeRel t)
        {
            _time = t._time;
        }
        public ulong ToMilliseconds()
        {
            return _time;
        }
        public float ToSeconds()
        {
            return _time * MS_TO_S;
        }
        public static bool operator ==(TTimeRel a, TTimeRel b) { return a._time == b._time; }
        public static bool operator !=(TTimeRel a, TTimeRel b) { return a._time != b._time; }
        public static bool operator >(TTimeRel a, TTimeRel b) { return a._time > b._time; }
        public static bool operator <(TTimeRel a, TTimeRel b) { return a._time < b._time; }
        public static bool operator >=(TTimeRel a, TTimeRel b) { return a._time >= b._time; }
        public static bool operator <=(TTimeRel a, TTimeRel b) { return a._time <= b._time; }
        public static bool operator ==(TTimeRel a, float b) { return a.ToSeconds() == b; }
        public static bool operator !=(TTimeRel a, float b) { return a.ToSeconds() != b; }
        public static bool operator >(TTimeRel a, float b) { return a.ToSeconds() > b; }
        public static bool operator <(TTimeRel a, float b) { return a.ToSeconds() < b; }
        public static bool operator >=(TTimeRel a, float b) { return a.ToSeconds() >= b; }
        public static bool operator <=(TTimeRel a, float b) { return a.ToSeconds() <= b; }
        public static bool operator ==(float b, TTimeRel a) { return a.ToSeconds() == b; }
        public static bool operator !=(float b, TTimeRel a) { return a.ToSeconds() != b; }
        public static bool operator >(float b, TTimeRel a) { return a.ToSeconds() > b; }
        public static bool operator <(float b, TTimeRel a) { return a.ToSeconds() < b; }
        public static bool operator >=(float b, TTimeRel a) { return a.ToSeconds() >= b; }
        public static bool operator <=(float b, TTimeRel a) { return a.ToSeconds() <= b; }

        public static TTimeAbs operator +(TTimeRel a, TTimeAbs b)
        {
            return new TTimeAbs(a._time + b.ToMilliseconds());
        }
        public static TTimeRel operator +(TTimeRel a, TTimeRel b)
        {
            return new TTimeRel(a._time + b._time);
        }
        public static TTimeRel operator -(TTimeRel a, TTimeRel b)
        {
            if (a._time > b._time)
            {
                return new TTimeRel(a._time - b._time);
            }
            return new TTimeRel(0);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            return this == (TTimeRel)obj;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
