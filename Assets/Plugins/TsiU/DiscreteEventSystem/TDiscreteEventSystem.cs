using System.Collections.Generic;

namespace TsiU
{
    enum EDiscreteEventPriority : int
    {
        Default = 0,
    };
    enum EDiscreteEventSystemState : int
    {
        Empty = 0,
        NotEmpty = 1,
    }
    class TDiscreteEventSystem
    {
        private LinkedList<TDiscreteEvent> _events;

        public TDiscreteEventSystem()
        {
            _events = new LinkedList<TDiscreteEvent>();
        }
        public void Clear()
        {
            _events.Clear();
        }
        public void ClearAfter(TTimeAbs gameTime)
        {
            LinkedListNode<TDiscreteEvent> evt = _events.Last;
            while (evt != null)
            {
                if (evt.Value.TriggeredTime >= gameTime)
                {
                    _events.RemoveLast();
                    evt = _events.Last;
                }
                else
                {
                    break;
                }
            }
        }
        public EDiscreteEventSystemState Process(TAny workingData, ref TTimeAbs curTime)
        {
            bool hasPoped = false;
            while (true)
            {
                if (_events.Count == 0)
                {
                    return EDiscreteEventSystemState.Empty;
                }
                TDiscreteEvent evt = _events.First.Value;
                if (hasPoped == false || curTime == evt.TriggeredTime)
                {
                    _events.RemoveFirst();
                    curTime = evt.TriggeredTime;
                    evt.EventAction(curTime, workingData, evt.Param);

                    hasPoped = true;
                }
                else
                {
                    break;
                }
            }
            return EDiscreteEventSystemState.NotEmpty;
        }
        public bool PushEvent(TTimeAbs occurredTime, TDiscreteEvent.DiscreteEventAction action, object param = null, int priority = (int)EDiscreteEventPriority.Default)
        {
            LinkedListNode<TDiscreteEvent> result = _events.Last;
            while (result != null)
            {
                if (occurredTime > result.Value.TriggeredTime || (occurredTime == result.Value.TriggeredTime && priority < result.Value.Priority))
                {
                    break;
                }
                result = result.Previous;
            }
            TDiscreteEvent newEvt = new TDiscreteEvent();
            newEvt.TriggeredTime = occurredTime;
            newEvt.EventAction = action;
            newEvt.Priority = priority;
            newEvt.Param = param;
            if (result == null)
            {
                _events.AddFirst(newEvt);
            }
            else
            {
                _events.AddAfter(result, newEvt);
            }
            return true;
        }
        public bool PushEvent(TTimeAbs currentTime, TTimeRel timeAfter, TDiscreteEvent.DiscreteEventAction action, object param = null, int priority = (int)EDiscreteEventPriority.Default)
        {
            TTimeAbs occurredTime = currentTime + timeAfter;
            return PushEvent(occurredTime, action, param, priority);
        }
    }
}
