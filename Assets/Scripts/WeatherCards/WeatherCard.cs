using System;
using SimpleSaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WeatherCards
{
    public class WeatherCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _cityName;
        [SerializeField] private TextMeshProUGUI _temperature;
        [SerializeField] private Image _weatherIcon;
        
        public string CityName { get; private set; }
        public string Icon { get; private set; }
        public float Temp { get; private set; }
        public bool IsActive { get; private set; }

        public void SetData(WeatherData data)
        {
            _cityName.text = data.name;
            var roundedTemperature  = (int)Math.Round(data.main.temp, MidpointRounding.AwayFromZero);
            _temperature.text = $"{roundedTemperature}°C";
            _weatherIcon.sprite = Resources.Load<Sprite>($"{GameConstants.WEATHER_ICONS_PATH}{data.weather[0].icon}");

            CityName = data.name;
            Icon = data.weather[0].icon;
            Temp = data.main.temp;
            IsActive = gameObject.activeSelf;
        }

        public void SetData(GameData.WeatherCardData data)
        {
            _cityName.text = data.City;
            var roundedTemperature  = (int)Math.Round(data.Temp, MidpointRounding.AwayFromZero);
            _temperature.text = $"{roundedTemperature}°C";
            _weatherIcon.sprite = Resources.Load<Sprite>($"{GameConstants.WEATHER_ICONS_PATH}{data.Icon}");
            gameObject.SetActive(data.IsActive);

            CityName = data.City;
            Icon = data.Icon;
            Temp = data.Temp;
            IsActive = data.IsActive;
        }

        public void SetActivateStatus(bool isActive)
        {
            gameObject.SetActive(isActive);
            IsActive = isActive;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetActivateStatus(false);
        }
    }
}