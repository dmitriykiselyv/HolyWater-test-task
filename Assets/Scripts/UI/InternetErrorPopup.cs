using DG.Tweening;
using Settings;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InternetErrorPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private int _numberOfFlashes = 3;
        [SerializeField] private UrlOpener _urlOpener;
    
        private bool _isAnimating;

        private void Awake()
        {
            _urlOpener.NoInternetConnection += FlashText; //TODO: There should be a popup here, but to simplify it, I made the text animation
        }

        private void OnDestroy()
        {
            _urlOpener.NoInternetConnection -= FlashText;
            DOTween.Kill(_statusText);
        }

        private void FlashText()
        {
            if(_isAnimating) return;
        
            _isAnimating = true;
            DOTween.Kill(_statusText);
            _statusText.DOFade(1, _fadeDuration)
                .SetLoops(_numberOfFlashes * 2, LoopType.Yoyo) 
                .OnComplete(StopAnimation);
        }
    
        private void StopAnimation()
        {
            DOTween.Kill(_statusText);
            _statusText.alpha = 0;
            _isAnimating = false;
        }
    }
}
