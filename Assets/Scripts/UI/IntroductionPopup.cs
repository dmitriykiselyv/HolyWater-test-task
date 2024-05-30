using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IntroductionPopup : MonoBehaviour, IPopup
    {
        public event Action OnPopupClose;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _animationDuration = 0.5f;

        private bool _popupIsVisible;

        public void Awake()
        {
            _closeButton.onClick.AddListener(PopupClose);
        }

        public void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(PopupClose);
        }

        private void PopupClose()
        {
            OnPopupClose?.Invoke();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            SetPopupVisibility(true);
        }

        public void Hide()
        {
            SetPopupVisibility(false);
        }

        private void SetPopupVisibility(bool isVisible)
        {
            _canvasGroup.DOKill();
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
            SetAlphaChannel(isVisible);
        }

        private void SetAlphaChannel(bool isVisible)
        {
            float targetAlpha = isVisible ? 1f : 0f;

            _popupIsVisible = isVisible;
            _canvasGroup.DOFade(targetAlpha, _animationDuration)
                .OnComplete(OnFadeComplete);
        }

        private void OnFadeComplete()
        {
            if (!_popupIsVisible)
            {
                gameObject.SetActive(false);
            }
            Debug.Log(_popupIsVisible ? $"{name} shown" : $"{name} hidden");
        }
    }
}
