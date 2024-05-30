using UnityEngine;
using UnityEngine.UI;

namespace BootScene
{
    public class NextSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneNames _scene = SceneNames.MainScene;

        private SceneLoader _sceneLoader;

        public void Init(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _button.onClick.AddListener(LoadScene);
        }

        public void DeInit()
        {
            _button.onClick.RemoveListener(LoadScene);
        }

        private void LoadScene()
        {
            _button.enabled = false;
            _sceneLoader.SceneToLoad(_scene);
        }
    }
}
