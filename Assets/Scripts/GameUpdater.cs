using UnityEngine;

namespace AIToolkitDemo
{
    class GameUpdater : MonoBehaviour
    {
        private AIEnityManager.AIEntityUpdater _aiUpdater;
        private AIEnityManager.AIEntityUpdater _requestUpdater;
        private AIEnityManager.AIEntityUpdater _behaviorUpdater;
        void Awake()
        {
            _aiUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateAI(gameTime, deltaTime);
                };
            _requestUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateReqeust(gameTime, deltaTime);
                };
            _behaviorUpdater =
                delegate(AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateBehavior(gameTime, deltaTime);
                };
        }
        void Update()
        {
            //update global timer
            GameTimer.instance.UpdateTime();
            
            float deltaTime = GameTimer.instance.deltaTime;
            float gameTime  = GameTimer.instance.gameTime;

            AIEnityManager entityMgr = AIEnityManager.instance;
            //Update AI
            entityMgr.IteratorDo(_aiUpdater, gameTime, deltaTime);
            //Update Request
            entityMgr.IteratorDo(_requestUpdater, gameTime, deltaTime);
            //Update Behaivor
            entityMgr.IteratorDo(_behaviorUpdater, gameTime, deltaTime);
        }
        //----------------------------------------------------------
        //Debug UI
        //----------------------------------------------------------
        void OnGUI()
        {
            //speed up/slow down
            Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0, 2);
            //add unity
            if (GUILayout.Button("Add Entity"))
            {
                GameObject go = GameResourceManager.instance.LoadResource("Zombie");
                if(go != null){
                    AIEnityManager.instance.AddEntity(go.AddComponent<AIEntity>().Init());
                }
            }
        }
    }
}
