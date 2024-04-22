using System.Collections.Generic;
using SimpleSaveSystem;
using UI;
using UnityEngine;

namespace WeatherCards
{
    public class CardGenerator : MonoBehaviour, ISaveable
    {
        [SerializeField] private WeatherCard _cardPrefab;
        [SerializeField] private Transform _cardsParent;

        private readonly Dictionary<string, WeatherCard> _existingCards = new Dictionary<string, WeatherCard>();
        private readonly List<GameData.WeatherCardData> _weatherData = new List<GameData.WeatherCardData>();
        private WeatherDataFetcher _weatherDataFetcher;
        private ResetButton _resetButton;
        private SaveLoadController _saveLoadController;

        public void Init(WeatherDataFetcher weatherDataFetcher, ResetButton resetButton, SaveLoadController saveLoadController)
        {
            _weatherDataFetcher = weatherDataFetcher;
            _weatherDataFetcher.WeatherDataLoaded += HandleWeatherDataLoaded;

            _saveLoadController = saveLoadController;
            _saveLoadController.RegisterSaveable(this);
            
            _resetButton = resetButton;
            _resetButton.ResetClicked += ActivateDisabledCards;
        }

        public void DeInit()
        {
            _weatherDataFetcher.WeatherDataLoaded -= HandleWeatherDataLoaded;
            _resetButton.ResetClicked -= ActivateDisabledCards;
            _saveLoadController.UnregisterSaveable(this);
        }

        private void HandleWeatherDataLoaded(List<WeatherData> weatherData)
        {
            foreach (var data in weatherData)
            {
                if (_existingCards.TryGetValue(data.name, out var card))
                {
                    card.SetData(data);
                }
                else
                {
                    var newCard = CreateCard(data);
                    _existingCards[data.name] = newCard;
                }
            }
        }
        
        
        private WeatherCard CreateCard(WeatherData weatherData)
        {
            var weatherCard = Instantiate(_cardPrefab, _cardsParent);
            weatherCard.SetData(weatherData);
            return weatherCard;
        }

        private WeatherCard CreateCard(GameData.WeatherCardData weatherData)
        {
            var weatherCard = Instantiate(_cardPrefab, _cardsParent);
            weatherCard.SetData(weatherData);
            return weatherCard;
        }
        
        private void ActivateDisabledCards()
        {
            foreach (KeyValuePair<string, WeatherCard> entry in _existingCards)
            {
                if (!entry.Value.gameObject.activeSelf)
                {
                    entry.Value.SetActivateStatus(true);
                }
            }
        }

        public void SaveData()
        {
            _weatherData.Clear();
            foreach (KeyValuePair<string, WeatherCard> entry in _existingCards)
            {
                _weatherData.Add(new GameData.WeatherCardData
                {
                    City = entry.Value.CityName,
                    Icon = entry.Value.Icon,
                    Temp = entry.Value.Temp,
                    IsActive = entry.Value.IsActive
                });
            }

            GameData.WeatherCardsContainer container = new GameData.WeatherCardsContainer { WeatherCards = new List<GameData.WeatherCardData>(_weatherData) };
            string json = JsonUtility.ToJson(container);
            PlayerPrefs.SetString(GameConstants.WEATHER_CARDS_KEY, json);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            string json = PlayerPrefs.GetString(GameConstants.WEATHER_CARDS_KEY, "{}");
            GameData.WeatherCardsContainer container = JsonUtility.FromJson<GameData.WeatherCardsContainer>(json);
            foreach (var cardData in container.WeatherCards)
            {
                if (_existingCards.TryGetValue(cardData.City, out var card))
                {
                    card.SetData(cardData);
                }
                else
                {
                    var newCard = CreateCard(cardData);
                    _existingCards[cardData.City] = newCard;
                }
            }
        }
    }
}