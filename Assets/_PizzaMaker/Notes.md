# Save/Load Dialogue system

Saving dialogue state:

            var s = PersistentDataManager.GetSaveData();
            PlayerPrefs.SetString(Constants.SaveData, s);

Load dialogue state:

            PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(Constants.SaveData));
            var s = PersistentDataManager.GetSaveData();

# 3rd Party Package Used
- [Dialogue System](https://assetstore.unity.com/packages/tools/gui/ink-dialogue-system-123456)
- Zenject
- PrimeTween
- MPUI
- Amplify
- Bakery
- Odin Serializer & Inspector
- Save System Data Serializer
- Beautify
- SSR
- vHierarchy