namespace TsiU
{
    public struct TTimeAbs
    {
        private const ulong S_TO_MS = 1000;
        private const float MS_TO_S = 0.001f;

        private ulong _time;

        public TTimeAbs(float t)
        {
            if (t < 0)
            {
                t = 0;
            }
            _time = (ulong)(t * S_TO_MS + 0.5f);
        }
        public TTimeAbs(TTimeAbs t)
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
        public void SetRawMilliseconds(ulong t)
        {
            if (t < 0)
            {
                t = 0;
            }
            _time = t;
        }
        public static bool operator ==(TTimeAbs a, TTimeAbs b) { return a._time == b._time; }
        public static bool operator !=(TTimeAbs a, TTimeAbs b) { return a._time != b._time; }
        public static bool operator >(TTimeAbs a, TTimeAbs b) { return a._time > b._time; }
        public static bool operator <(TTimeAbs a, TTimeAbs b) { return a._time < b._time; }
        public static bool operator >=(TTimeAbs a, TTimeAbs b) { return a._time >= b._time; }
        public static bool operator <=(TTimeAbs a, TTimeAbs b) { return a._time <= b._time; }
        public static bool operator ==(TTimeAbs a, float b) { return a.ToSeconds() == b; }
        public static bool operator !=(TTimeAbs a, float b) { return a.ToSeconds() != b; }
        public static bool operator >(TTimeAbs a, float b) { return a.ToSeconds() > b; }
        public static bool operator <(TTimeAbs a, float b) { return a.ToSeconds() < b; }
        public static bool operator >=(TTimeAbs a, float b) { return a.ToSeconds() >= b; }
        public static bool operator <=(TTimeAbs a, float b) { return a.ToSeconds() <= b; }
        public static bool operator ==(float b, TTimeAbs a) { return a.ToSeconds() == b; }
        public static bool operator !=(float b, TTimeAbs a) { return a.ToSeconds() != b; }
        public static bool operator >(float b, TTimeAbs a) { return a.ToSeconds() > b; }
        public static bool operator <(float b, TTimeAbs a) { return a.ToSeconds() < b; }
        public static bool operator >=(float b, TTimeAbs a) { return a.ToSeconds() >= b; }
        public static bool operator <=(float b, TTimeAbs a) { return a.ToSeconds() <= b; }

        public static TTimeAbs operator +(TTimeAbs a, TTimeRel b)
        {
            TTimeAbs timeAbs = new TTimeAbs();
            timeAbs.SetRawMilliseconds(a._time + b.ToMilliseconds());
            return timeAbs;
        }
        public static TTimeAbs operator -(TTimeAbs a, TTimeRel b)
        {
            if (a._time > b.ToMilliseconds())
            {
                TTimeAbs timeAbs = new TTimeAbs();
                timeAbs.SetRawMilliseconds(a._time - b.ToMilliseconds());
                return timeAbs;
            }
            return new TTimeAbs(0);
        }
        public static TTimeRel operator -(TTimeAbs a, TTimeAbs b)
        {
            if (a._time > b._time)
            {
                TTimeRel timeRel = new TTimeRel();
                timeRel.SetRawMilliseconds(a._time - b._time);
                return timeRel;
            }
            return new TTimeRel(0);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            return this == (TTimeAbs)obj;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
