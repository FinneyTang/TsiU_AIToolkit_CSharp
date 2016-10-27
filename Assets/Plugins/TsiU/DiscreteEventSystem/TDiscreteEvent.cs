namespace TsiU
{
    class TDiscreteEvent
    {
        internal delegate void DiscreteEventAction(TTimeAbs curTime, TAny workingData, object param);

        internal TTimeAbs TriggeredTime { get; set; }
        internal int Priority { get; set; }
        internal DiscreteEventAction EventAction { get; set; }
        internal object Param { get; set; }
    }
}
