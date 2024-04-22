using BootScene;
using UnityEngine;

public class BootSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private NextSceneButton _nextSceneButton;
    [SerializeField] private PreloadProgressAnimation _preloadProgressAnimation;
        
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = new SceneLoader();
        _nextSceneButton.Init(_sceneLoader);
        _preloadProgressAnimation.Init(_sceneLoader);
    }

    private void OnDestroy()
    {
        _nextSceneButton.DeInit();
        _preloadProgressAnimation.DeInit();
    }
}
