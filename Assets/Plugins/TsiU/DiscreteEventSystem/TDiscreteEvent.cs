using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TsiU
{
    class TDiscreteEvent
    {
        internal delegate int DiscreteEventAction(TAny workingData);

        internal TTimeAbs TriggeredTime { get; set; }
        internal int Priority { get; set; }
        internal DiscreteEventAction EventAction { get; set;}
    }
}
