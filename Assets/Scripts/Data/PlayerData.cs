using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Save Settings")]
    [SerializeField] private string saveFileName = "playerdata";
    [SerializeField] private bool useEncryption = false;

    [Header("Player Progress")]
    [SerializeField] private int currency = 0;
    [SerializeField] private int currentMission = 1;

    public int Currency { get => currency; set => currency = value; }
    public int CurrentMission { get => currentMission; set => currentMission = value; }

    private string SavePath => Path.Combine(Application.persistentDataPath, saveFileName + ".json");

    [System.Serializable]
    public class PlayerDataSave
    {
        public int currency;
        public int currentMission;

        public PlayerDataSave(PlayerData playerData)
        {
            currency = playerData.currency;
            currentMission = playerData.currentMission;
        }

        public PlayerDataSave() { }
    }

    [ContextMenu("Save Data")]
    public void SaveData()
    {
        try
        {
            PlayerDataSave saveData = new PlayerDataSave(this);
            string json = JsonUtility.ToJson(saveData, true);

            if (useEncryption)
            {
                json = EncryptDecrypt(json);
            }

            File.WriteAllText(SavePath, json);
            Debug.Log($"Player data saved to: {SavePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save player data: {e.Message}");
        }
    }

    [ContextMenu("Load Data")]
    public void LoadData()
    {
        try
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("Save file not found. Using default values.");
                return;
            }

            string json = File.ReadAllText(SavePath);

            if (useEncryption)
            {
                json = EncryptDecrypt(json);
            }

            PlayerDataSave saveData = JsonUtility.FromJson<PlayerDataSave>(json);
            ApplyLoadedData(saveData);

            Debug.Log("Player data loaded successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load player data: {e.Message}");
        }
    }

    private void ApplyLoadedData(PlayerDataSave saveData)
    {
        currency = saveData.currency;
        currentMission = saveData.currentMission;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    [ContextMenu("Delete Save File")]
    public void DeleteSaveFile()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                ResetToDefault();
                Debug.Log("Save file deleted.");
            }
            else
            {
                Debug.LogWarning("No save file found to delete.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to delete save file: {e.Message}");
        }
    }

    public bool HasSaveFile()
    {
        return File.Exists(SavePath);
    }

    private string EncryptDecrypt(string data)
    {
        string key = "PlayerDataKey"; // Cambia esto por tu propia clave
        string result = "";

        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ key[i % key.Length]);
        }

        return result;
    }

    [ContextMenu("Reset to Default")]
    public void ResetToDefault()
    {
        currency = 0;
        currentMission = 1;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif

        Debug.Log("Player data reset to default values.");
    }

    private void OnDisable()
    {
        SaveData();
    }
}