using SimpleSaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class AudioSettingsController : MonoBehaviour, ISaveable
    {
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Image _musicToggleImage;
        [SerializeField] private Image _soundToggleImage;
        [SerializeField] private Sprite _onIcon;
        [SerializeField] private Sprite _offIcon;

        private SaveLoadController _saveLoadController;

        public void Init(SaveLoadController saveLoadController)
        {
            _saveLoadController = saveLoadController;
            _saveLoadController.RegisterSaveable(this);
            
            _musicToggle.onValueChanged.AddListener(SetMusicEnabled);
            _soundToggle.onValueChanged.AddListener(SetSoundEnabled);
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            
            LoadData();
        }

        public void DeInit()
        {
            _musicToggle.onValueChanged.RemoveListener(SetMusicEnabled);
            _soundToggle.onValueChanged.RemoveListener(SetSoundEnabled);
            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
            
            _saveLoadController.UnregisterSaveable(this);
        }
        
        private void SetMusicEnabled(bool isOn)
        {
            _musicToggleImage.sprite = isOn ? _onIcon : _offIcon;
            if (isOn)
            {
                if (!_musicSource.isPlaying && _musicSource.time == 0)
                    _musicSource.Play();
                else if (!_musicSource.isPlaying && _musicSource.time > 0)
                    _musicSource.UnPause(); 
            }
            else
            {
                _musicSource.Pause();
            }
        }

        private void SetSoundEnabled(bool isOn)
        {
            _soundToggleImage.sprite = isOn ? _onIcon : _offIcon;
        }

        private void SetVolume(float volume)
        {
            _musicSource.volume = _volumeSlider.value;
        }

        public void SaveData()
        {
            GameData.AudioSettings settings = new GameData.AudioSettings()
            {
                MusicIsEnabled = _musicToggle.isOn,
                SoundIsEnabled = _soundToggle.isOn,
                Volume = _volumeSlider.value
            };
            string json = JsonUtility.ToJson(settings);
            PlayerPrefs.SetString(GameConstants.AUDIO_SETTINGS, json);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            string json = PlayerPrefs.GetString(GameConstants.AUDIO_SETTINGS, JsonUtility.ToJson(new  GameData.AudioSettings()));
            GameData.AudioSettings settings = JsonUtility.FromJson< GameData.AudioSettings>(json);
            _musicToggle.isOn = settings.MusicIsEnabled;
            _soundToggle.isOn = settings.SoundIsEnabled;
            _volumeSlider.value = settings.Volume;
            
            _musicSource.volume = _volumeSlider.value;

            if (_musicToggle.isOn)
            {
                _musicSource.Play();
            }
        }
    }
}