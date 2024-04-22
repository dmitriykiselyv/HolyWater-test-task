using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BootScene
{
    public class PreloadProgressAnimation : MonoBehaviour
    {
        [SerializeField] private Image _preloader;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _percentageText;
        [SerializeField] private CanvasGroup _preloaderCanvasGroup;
        [SerializeField] private float _fakeLoadDuration = 5f;
        [SerializeField] private float _fillSpeed = 1f;
        
        private SceneLoader _sceneLoader;

        public void Init(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _sceneLoader.OnSceneLoading += SceneLoading;
            SetDefaultValues();
        }

        public void DeInit()
        {
            _sceneLoader.OnSceneLoading -= SceneLoading;
        }

        private void SetDefaultValues()
        {
            _preloader.fillAmount = 0;
            _preloader.fillClockwise = true; 
            _progressBar.fillAmount = 0;
            _percentageText.text = string.Empty;
        }

        private void SceneLoading(Action activateNextScene)
        {
            _preloaderCanvasGroup.alpha = 1;
            
            PreloadAnimation();
            FakeProgressBarFilling(activateNextScene);
        }
        
        private void PreloadAnimation()
        {
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(_preloader.DOFillAmount(1, _fillSpeed).SetEase(Ease.Linear)
                .OnComplete(() => { _preloader.fillClockwise = false; }));
            mySequence.Append(_preloader.DOFillAmount(0, _fillSpeed).SetEase(Ease.Linear)
                .OnRewind(() => { _preloader.fillClockwise = true; }));
            
            mySequence.SetLoops(-1);
        }

        private void FakeProgressBarFilling(Action activateNextScene)
        {
            DOTween.To(() => _progressBar.fillAmount, x => _progressBar.fillAmount = x, 1, _fakeLoadDuration)
                .OnUpdate(() => { _percentageText.text = $"{(int)(_progressBar.fillAmount * 100)}%"; })
                .OnComplete(() =>
                {
                    DOTween.KillAll();
                    activateNextScene?.Invoke();
                });
        }
    }
}
