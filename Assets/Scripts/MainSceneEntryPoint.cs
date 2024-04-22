using Settings;
using SimpleSaveSystem;
using UI;
using UnityEngine;
using WeatherCards;

public class MainSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private AudioSettingsController _audioSettingsController;
    [SerializeField] private WeatherUpdate _weatherUpdate;
    [SerializeField] private WeatherDataFetcher _weatherDataFetcher;
    [SerializeField] private ResetButton _resetButton;
    
    private SaveLoadController _saveLoadController;
    private PopupController _popupController;


    private void Awake()
    {
        _saveLoadController = new SaveLoadController();
        _audioSettingsController.Init(_saveLoadController);
        _popupController = new PopupController();
        _uiController.Init(_popupController, _resetButton, _saveLoadController);
        _cardGenerator.Init(_weatherDataFetcher, _resetButton, _saveLoadController);
        _weatherDataFetcher.Init();
        _weatherUpdate.Init(_weatherDataFetcher);
        
        _saveLoadController.LoadAll();
    }

    private void OnDestroy()
    {
        _audioSettingsController.DeInit();
        _popupController.Cleanup();
        _uiController.DeInit();
        _cardGenerator.DeInit();
        _weatherUpdate.DeInit();
    }
}