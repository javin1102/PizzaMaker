using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaMaker
{
    public static class AppController
    {
        public static PlayerController playerController;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnAppStart()
        {
            AutoLoadCoreScene();

        }

        private static void AutoLoadCoreScene()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var sceneName = SceneManager.GetSceneAt(i).name;
                if (sceneName == GlobalVars.SceneCore)
                    return;
            }

            var coreScene = SceneManager.GetSceneByName(GlobalVars.SceneCore);
            var hasCoreScene = coreScene.isLoaded;
            if (!hasCoreScene)
            {
                SceneManager.LoadSceneAsync(GlobalVars.SceneCore, LoadSceneMode.Additive);
            }
        }
    }
}