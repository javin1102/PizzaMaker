# Save/Load Dialogue system

Saving dialogue state:

            var s = PersistentDataManager.GetSaveData();
            PlayerPrefs.SetString(Constants.SaveData, s);

Load dialogue state:

            PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(Constants.SaveData));
            var s = PersistentDataManager.GetSaveData();
