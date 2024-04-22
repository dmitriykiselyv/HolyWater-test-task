using SimpleSaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _exitBttn;
        [SerializeField] private Button _settingsBttn;
        [SerializeField] private IntroductionPopup _introductionPopup;
        [SerializeField] private SettingsPopup _settingsPopup;

        private PopupController _popupController;
        private ResetButton _resetButton;
        private SaveLoadController _saveLoadController;

        public void Init(PopupController popupController, ResetButton resetButton, SaveLoadController saveLoadController)
        {
            _popupController = popupController;
            _resetButton = resetButton;
            _saveLoadController = saveLoadController;

            _resetButton.ResetClicked += OnClickIntroduction;
        
            _popupController.RegisterPopup(PopupNames.Settings, _settingsPopup);
            _popupController.RegisterPopup(PopupNames.Introduction, _introductionPopup);
        
            _exitBttn.onClick.AddListener(ExitGame);
            _settingsBttn.onClick.AddListener(OnSettingsButtonClicked);
        
            _settingsPopup.OnPopupClose += HidePopup;
            _settingsPopup.OnClickIntroduction += OnClickIntroduction;
            _introductionPopup.OnPopupClose += HidePopup;
        

            ShowPopup(PopupNames.Introduction);
        }

        public void DeInit()
        {
            _resetButton.ResetClicked -= OnClickIntroduction;
            _exitBttn.onClick.RemoveListener(ExitGame);
            _settingsBttn.onClick.RemoveListener(OnSettingsButtonClicked);
            _introductionPopup.OnPopupClose -= HidePopup;
            _settingsPopup.OnPopupClose -= HidePopup;
            _settingsPopup.OnClickIntroduction -= OnClickIntroduction;
        }

        private void OnSettingsButtonClicked()
        {
            ShowPopup(PopupNames.Settings);
        }
    
        private void OnClickIntroduction()
        {
            ShowPopup(PopupNames.Introduction, true);
        }
    
        private void ShowPopup(PopupNames popupName, bool closeCurrent = false)
        {
            _popupController.ShowPopup(popupName, closeCurrent);
        }

        private void HidePopup()
        {
            _popupController.HidePopup();
        }
    
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _saveLoadController.SaveAll();
            }
        }

        private void OnApplicationQuit()
        {
            _saveLoadController.SaveAll();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            _saveLoadController.SaveAll();
#elif UNITY_ANDROID || UNITY_IOS
        _saveLoadController.SaveAll();
        Application.Quit();
#endif
        }
    }
}