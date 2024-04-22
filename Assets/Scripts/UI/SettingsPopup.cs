using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPopup : MonoBehaviour, IPopup
    {
        public event Action OnPopupClose;
        public event Action OnClickIntroduction;
    
        [SerializeField] private Button _closeBttn;
        [SerializeField] private Button _introductionBttn;
    
        public void Awake()
        {
            _closeBttn.onClick.AddListener(PopupClose);
            _introductionBttn.onClick.AddListener(ClickIntroduction);
        }

        public void OnDestroy()
        {
            _closeBttn.onClick.RemoveListener(PopupClose);
            _introductionBttn.onClick.RemoveListener(ClickIntroduction);
        }
    
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    
        private void PopupClose()
        {
            OnPopupClose?.Invoke();
        }

        private void ClickIntroduction()
        {
            OnClickIntroduction?.Invoke();
        }
    }
}