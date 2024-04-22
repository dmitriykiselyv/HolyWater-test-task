using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace HorizontalScrollPanel
{
    public class AutoScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private RectTransform _viewportRect;
        [SerializeField] private float _scrollSpeed = 5.0f;
        [SerializeField] private float _autoScrollRestartDelay = 5f;
        public bool IsAutoScrolling { get; private set; }

        private float _viewportWidth;
        private float _contentWidth;
        private int _direction = 1;

        private void Awake()
        {
            IsAutoScrolling = true;
            _viewportWidth = _viewportRect.rect.xMax;
            _contentWidth = _contentRect.rect.width;
        }

        private void Update()
        {
            if (!IsAutoScrolling) return;
            
            Vector2 moveDelta = new Vector2(_scrollSpeed * Time.deltaTime * _direction, 0);
            _contentRect.anchoredPosition += moveDelta;
            
            if (_direction == 1 && _contentRect.anchoredPosition.x >= _viewportWidth)
            {
                _direction = -1;
            }
            else if (_direction == -1 && _contentRect.anchoredPosition.x < 0)
            {
                _direction = 1;
            }
        }

        public void ToggleAutoScroll(bool state)
        {
            IsAutoScrolling = state;
        }

        public IEnumerator RestartAutoScrollAfterDelay()
        {
            yield return new WaitForSeconds(_autoScrollRestartDelay);
            ToggleAutoScroll(true);
        }
    }
}