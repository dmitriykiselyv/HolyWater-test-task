using System.Collections.Generic;

namespace SimpleSaveSystem
{
    public class SaveLoadController
    {
        private readonly List<ISaveable> _saveables = new List<ISaveable>();
        
        public void RegisterSaveable(ISaveable saveable)
        {
            if (!_saveables.Contains(saveable))
                _saveables.Add(saveable);
        }

        public void UnregisterSaveable(ISaveable saveable)
        {
            _saveables.Remove(saveable);
        }

        public void SaveAll()
        {
            foreach (var saveable in _saveables)
            {
                saveable.SaveData();
            }
        }

        public void LoadAll()
        {
            foreach (var saveable in _saveables)
            {
                saveable.LoadData();
            }
        }
    }
}