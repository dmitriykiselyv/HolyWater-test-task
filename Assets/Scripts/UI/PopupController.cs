using System.Collections.Generic;

namespace UI
{
    public class PopupController
    {
        private readonly Dictionary<PopupNames, IPopup> _popups = new Dictionary<PopupNames, IPopup>();
        private readonly Stack<IPopup> _popupStack = new Stack<IPopup>();

        public void RegisterPopup(PopupNames key, IPopup popup)
        {
            if (!_popups.ContainsKey(key))
            {
                _popups[key] = popup;
            }
        }

        public void ShowPopup(PopupNames key, bool closeCurrent = false)
        {
            if (!_popups.TryGetValue(key, out var popup)) return;
        
            if (closeCurrent && _popupStack.Count > 0)
            {
                IPopup currentPopup = _popupStack.Pop();
                currentPopup.Hide();
            }

            popup.Show();
            _popupStack.Push(popup);
        }

        public void HidePopup()
        {
            if (_popupStack.Count <= 0) return;
        
            IPopup currentPopup = _popupStack.Pop();
            currentPopup.Hide();
        }

        public void Cleanup()
        {
            _popups.Clear();
            _popupStack.Clear();
        }
    }
}