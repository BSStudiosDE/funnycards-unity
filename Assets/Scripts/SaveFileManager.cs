using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    public Player player; // Reference to the player object
    private static string _saveFilePath;

    private void Awake()
    {
        _saveFilePath = Path.Combine(Application.persistentDataPath, "playerSave.json");
    }

    public void SaveGame()
    {
        Debug.Log("SaveFileManager.SaveGame: Saving game...");
        // Create a SaveData object from the player's data
        SaveData data = new()
        {
            inventory = player.playerInventory,
            gold = player.playerInventory.gold,
            gems = player.playerInventory.gems
        };

        // Serialize the data to JSON
        string json = JsonUtility.ToJson(data, true); // true for pretty printing

        // Write the JSON to a file
        File.WriteAllText(_saveFilePath, json);
        Debug.Log($"SaveFileManager.SaveGame: Game saved to {_saveFilePath}");
    }

    public void LoadGame()
    {
        Debug.Log("SaveFileManager.LoadGame: Loading game...");
        // Check if the save file exists
        if (File.Exists(_saveFilePath))
        {
            // Read the JSON from the file
            string json = File.ReadAllText(_saveFilePath);

            // Deserialize the JSON back into a SaveData object
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Apply the loaded data to the player
            player.playerInventory = data.inventory;
            player.playerInventory.gold = data.gold;
            player.playerInventory.gems = data.gems;
            Debug.Log($"SaveFileManager.LoadGame: Game loaded from {_saveFilePath}");
        }
        else
        {
            Debug.LogWarning("SaveFileManager.LoadGame: No save file found.");
        }
    }
}