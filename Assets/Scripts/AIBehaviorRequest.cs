using UnityEngine;

namespace AIToolkitDemo
{
    class AIBehaviorRequest
    {
        public AIBehaviorRequest(float timeStamp, Vector3 nextMovingTarget)
        {
            this.timeStamp = timeStamp;
            this.nextMovingTarget = nextMovingTarget;
        }
        public float timeStamp          { get; private set; }
        public Vector3 nextMovingTarget { get; private set; }
    }
}
