using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace WeatherCards
{
    public class WeatherDataFetcher : MonoBehaviour
    {
        public event Action<List<WeatherData>> WeatherDataLoaded;
        private CitiesData _citiesData;
        private readonly float _timeout = 10f;
        private readonly List<WeatherData> _weatherDatas = new List<WeatherData>();

        public void Init()
        {
            LoadCitiesJson();

            if (!PlayerPrefs.HasKey(GameConstants.WEATHER_CARDS_KEY))
            {
                StartCoroutine(RefreshWeatherData());
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void LoadCitiesJson()
        {
            var citiesJson = Resources.Load<TextAsset>("Cities");
            if (citiesJson != null)
            {
                _citiesData = JsonUtility.FromJson<CitiesData>(citiesJson.text);
            }
            else
            {
                Debug.LogError("Failed to load Cities.json");
            }
        }

        public IEnumerator RefreshWeatherData()
        {
            _weatherDatas.Clear();
            yield return StartCoroutine(GetWeatherDataAsync());
        }

        private IEnumerator GetWeatherDataAsync()
        {
            foreach (var city in _citiesData.Cities)
            {
                yield return StartCoroutine(GetWeatherData(city)); //expensive method..
                //Is the simplest way, alternatively you can use parallel queries.
                // It is also necessary to make  separate error handlers in case of network problems or content not being available
            }
            WeatherDataLoaded?.Invoke(_weatherDatas);
        }

        private IEnumerator GetWeatherData(string city)
        {
            string url = $"{GameConstants.WEATHER_BASE_URL}?q={city}&appid={GameConstants.WEATHER_API_KEY}&units=metric";
            using UnityWebRequest request = UnityWebRequest.Get(url);
            Coroutine timeoutCoroutine = StartCoroutine(RequestTimeout(request, _timeout));
            yield return request.SendWebRequest();
            StopCoroutine(timeoutCoroutine);

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                Debug.LogError($"Failed to get weather data for {city}: {request.error}");
                break;
                case UnityWebRequest.Result.Success:
                {
                    string json = request.downloadHandler.text;
                    WeatherData data = JsonUtility.FromJson<WeatherData>(json);
                    _weatherDatas.Add(data);
                    break;
                }
            }
        }

        private IEnumerator RequestTimeout(UnityWebRequest request, float timeout)
        {
            yield return new WaitForSeconds(timeout);
            if (request.isDone)
                yield break;

            request.Abort();
            Debug.LogError("Request timed out.");
        }
    }
}