using UnityEngine;

namespace AIToolkitDemo
{
    class GameStarter : MonoBehaviour
    {
        void Awake()
        {
            UnityAITookit.Init();
        }
        void Start()
        {
            //init timer
            GameTimer.instance.Init();
            //add game updater and trigger update
            gameObject.AddComponent<GameUpdater>();
        }
        void Destroy()
        {
            UnityAITookit.Uninit();
        }
    }
}
