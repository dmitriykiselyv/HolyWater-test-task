using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherCards
{
    public class WeatherUpdate : MonoBehaviour
    {
        [SerializeField] private Button _updateBttn;

        private WeatherDataFetcher _weatherDataFetcher;
        private readonly float _cooldown = 12f;
        
        public void Init(WeatherDataFetcher weatherDataFetcher)
        {
            _weatherDataFetcher = weatherDataFetcher;
            _updateBttn.onClick.AddListener(UpdateWeatherContent);
        }

        public void DeInit()
        {
            _updateBttn.onClick.RemoveListener(UpdateWeatherContent);
        }

        private void UpdateWeatherContent()
        {
            StartCoroutine(_weatherDataFetcher.RefreshWeatherData());
            StartCoroutine(StartCooldown());
        }
        
        private IEnumerator StartCooldown()
        {
            _updateBttn.interactable = false;
            yield return new WaitForSeconds(_cooldown);
            _updateBttn.interactable = true;
        }
    }
}
