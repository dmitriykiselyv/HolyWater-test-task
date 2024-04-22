using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootScene
{
    public class SceneLoader
    {
        public delegate void SceneLoading(Action sceneReadyCallback);
        public event SceneLoading OnSceneLoading;
        
        private AsyncOperation _asyncLoad;

        public void SceneToLoad(SceneNames sceneName)
        {
            _asyncLoad = SceneManager.LoadSceneAsync(sceneName.ToString());
            _asyncLoad.allowSceneActivation = false;
            
            OnSceneLoading?.Invoke(ActivateScene);
        }
        
        private void ActivateScene()
        {
            _asyncLoad.allowSceneActivation = true;
        }
    }
}