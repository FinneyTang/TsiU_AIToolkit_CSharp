using UnityEngine;
using TsiU;

namespace AIToolkitDemo
{
    class AIEntity : MonoBehaviour
    {
        //-----------------------------------------------
        public const string BBKEY_NEXTMOVINGPOSITION = "NextMovingPosition";
        //-----------------------------------------------
        private TBTAction _behaviorTree;
        private AIEntityWorkingData _behaviorWorkingData;
        private TBlackBoard _blackboard;

        private AIBehaviorRequest _currentRequest;
        private AIBehaviorRequest _nextRequest;

        private GameObject _targetDummyObject;

        private float _nextTimeToGenMovingTarget;
        private string _lastTriggeredAnimation;

        private bool _isDead;
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                _isDead = value;
            }
        }
        public AIEntity Init()
        {
            _behaviorTree = AIEntityBehaviorTreeFactory.GetBehaviorTreeDemo1();

            _behaviorWorkingData = new AIEntityWorkingData();
            _behaviorWorkingData.entity = this;
            _behaviorWorkingData.entityTF = this.transform;
            _behaviorWorkingData.entityAnimator = GetComponent<Animator>();

            _blackboard = new TBlackBoard();

            _nextTimeToGenMovingTarget = 0f;
            _lastTriggeredAnimation = string.Empty;

            _isDead = false;

            _targetDummyObject = GameResourceManager.instance.LoadResource("AttackTarget");

            return this;
        }
        public T GetBBValue<T>(string key, T defaultValue)
        {
            return _blackboard.GetValue<T>(key, defaultValue);
        }
        public void PlayAnimation(string name)
        {
            if(_lastTriggeredAnimation == name)
            {
                return;
            }
            _lastTriggeredAnimation = name;
            _behaviorWorkingData.entityAnimator.SetTrigger(name);
        }
        public int UpdateAI(float gameTime, float deltaTime)
        {
            if (gameTime > _nextTimeToGenMovingTarget)
            {
                _nextRequest = new AIBehaviorRequest(gameTime, new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
                _nextTimeToGenMovingTarget = gameTime + 20f + Random.Range(-5f, 5f);
            }
            return 0;
        }
        public int UpdateReqeust(float gameTime, float deltaTime)
        {
            if (_nextRequest != _currentRequest)
            {
                //reset bev tree
                _behaviorTree.Transition(_behaviorWorkingData);
                //assign to current
                _currentRequest = _nextRequest;

                //reposition and add a little offset
                Vector3 targetPos = _currentRequest.nextMovingTarget + TMathUtils.GetDirection2D(_currentRequest.nextMovingTarget, transform.position) * 0.2f;
                Vector3 startPos = new Vector3(targetPos.x, -1.4f, targetPos.z);
                _targetDummyObject.transform.position = startPos;
                LeanTween.move(_targetDummyObject, targetPos, 1f);
            }
            return 0;
        }
        public int UpdateBehavior(float gameTime, float deltaTime)
        {
            if (_currentRequest == null)
            {
                return 0;
            }
            //update working data
            _behaviorWorkingData.entityAnimator.speed = GameTimer.instance.timeScale;
            _behaviorWorkingData.gameTime  = gameTime;
            _behaviorWorkingData.deltaTime = deltaTime;

            //test bb usage
            _blackboard.SetValue(BBKEY_NEXTMOVINGPOSITION, _currentRequest.nextMovingTarget);

            if (_behaviorTree.Evaluate(_behaviorWorkingData))
            {
                _behaviorTree.Update(_behaviorWorkingData);
            }
            else
            {
                _behaviorTree.Transition(_behaviorWorkingData);
            }
            return 0;
        }
    }
}
