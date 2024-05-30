using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HorizontalScrollPanel
{
    public class SnapToNearest : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private GameObject[] _items;
        [SerializeField] private RectTransform _anchorObject;
        [SerializeField] private float _maxSnapSpeed = 150f;
        [SerializeField] private float _minSnapSpeed = 3f;
        [SerializeField] private AutoScroll _autoScroll;

        private bool _dragging;

        private void Update()
        {
            if (_dragging || _autoScroll.IsAutoScrolling)
                return;

            float closestDistance = float.MaxValue;
            int closestIndex = 0;

            for (int i = 0; i < _items.Length; i++)
            {
                float distance = Mathf.Abs(_anchorObject.position.x - _items[i].transform.position.x);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }

            float targetX = _anchorObject.position.x - _items[closestIndex].transform.position.x;
            var currentPosition = _panel.anchoredPosition.x;
            var desiredPosition = currentPosition + targetX;

            var maxSpeed = _maxSnapSpeed * Time.deltaTime;
            var speed = Mathf.Max(_minSnapSpeed, Mathf.Min(Math.Abs(targetX), maxSpeed));
            var xLerpPos = Mathf.MoveTowards(currentPosition, desiredPosition, speed);
            _panel.anchoredPosition = new Vector2(xLerpPos, _panel.anchoredPosition.y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;
            _autoScroll.ToggleAutoScroll(false);
            StopCoroutine(_autoScroll.RestartAutoScrollAfterDelay());
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false;
            StartCoroutine(_autoScroll.RestartAutoScrollAfterDelay());
        }

        private void OnDestroy()
        {
            StopCoroutine(_autoScroll.RestartAutoScrollAfterDelay());
        }
    }
}