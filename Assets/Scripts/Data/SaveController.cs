using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;


public class SaveController : MonoBehaviour
{
    public static SaveController instance;
    private string saveLocation;
    private InventoryController inventoryController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        inventoryController = GetComponent<InventoryController>();

        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("GameSaved");
    }

    public void LoadGame()
    {
        if(File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            inventoryController.SetInventoryItems(saveData.inventorySaveData);

            Debug.Log("GameLoaded");
        }
        else
        {
            SaveGame();

        }
    }

    public static string GetSavePath(int slot)
    {
        return Application.persistentDataPath + $"/save{slot}.json";
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");

        player = gameObject.GetComponent<Player>();
        player.transform.position = new Vector3(0f, 0f, 0f);

        inventoryController.ClearInventory();

        SaveGame();

        Debug.Log("New Game");
    }

    //public static void ClearSave(int slot = 1)
    //{
    //    string path = Application.persistentDataPath + $"/save{slot}.json";

    //    if (File.Exists(path))
    //    {
    //        File.Delete(path);
    //        Debug.Log("Save File DELETED");
    //    }

    //    SceneManager.LoadScene("Game");

    //}
}