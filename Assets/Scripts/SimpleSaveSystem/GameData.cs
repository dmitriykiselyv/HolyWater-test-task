using System;
using System.Collections.Generic;

namespace SimpleSaveSystem
{
    public class GameData
    {
        [Serializable]
        public class AudioSettings
        {
            public bool MusicIsEnabled = true;
            public bool SoundIsEnabled = true;
            public float Volume = 0.2f;
        }

        [Serializable]
        public class WeatherCardData
        {
            public string City;
            public string Icon;
            public float Temp;
            public bool IsActive;
        }

        [Serializable]
        public class WeatherCardsContainer
        {
            public List<WeatherCardData> WeatherCards = new List<WeatherCardData>();
        }
    }
}