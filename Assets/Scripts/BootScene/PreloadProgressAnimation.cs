using DG.Tweening;
using System;
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
        [SerializeField] private float _targetFillAmount = 1.0f;
        [SerializeField] private float _fillSpeed = 1f;

        private Action _activateNextScene;
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
            _activateNextScene = activateNextScene;
            _preloaderCanvasGroup.alpha = 1;

            PreloadAnimation();
            FakeProgressBarFilling();
        }

        private void PreloadAnimation()
        {
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(_preloader.DOFillAmount(1, _fillSpeed).SetEase(Ease.Linear)
                .OnComplete(OnPreloaderFillComplete));
            mySequence.Append(_preloader.DOFillAmount(0, _fillSpeed).SetEase(Ease.Linear)
                .OnRewind(OnPreloaderFillRewind));

            mySequence.SetLoops(-1);
        }

        private void OnPreloaderFillComplete()
        {
            _preloader.fillClockwise = false;
        }

        private void OnPreloaderFillRewind()
        {
            _preloader.fillClockwise = true;
        }

        private void FakeProgressBarFilling()
        {
            DOTween.To(GetFillAmount, SetFillAmount, _targetFillAmount, _fakeLoadDuration)
                   .OnUpdate(UpdatePercentageText)
                   .OnComplete(OnComplete);
        }

        private float GetFillAmount()
        {
            return _progressBar.fillAmount;
        }

        private void SetFillAmount(float value)
        {
            _progressBar.fillAmount = value;
        }

        private void UpdatePercentageText()
        {
            _percentageText.text = $"{(int)(_progressBar.fillAmount * 100)}%";
        }

        private void OnComplete()
        {
            DOTween.KillAll();
            _activateNextScene?.Invoke();
        }
    }
}
