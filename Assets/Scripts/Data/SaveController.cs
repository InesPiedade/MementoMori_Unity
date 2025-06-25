using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveController : MonoBehaviour
{
    public static SaveController instance;
    private string saveLocation;
    private InventoryController inventoryController;
    [SerializeField] private Transform spawnPoint;
    SaveData currentSaveData;

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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
            return;
        }

        if (currentSaveData != null)
        {
            currentSaveData.playerPosition = player.transform.position;
            currentSaveData.inventorySaveData = InventoryController.instance.GetInventoryItems();
        }

        else
            currentSaveData = new SaveData
            {
                playerPosition = player.transform.position,
                inventorySaveData = inventoryController.GetInventoryItems(),
            };
        if(string.IsNullOrEmpty(saveLocation))
            saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        File.WriteAllText(saveLocation, JsonUtility.ToJson(currentSaveData));
        Debug.Log("Game Saved.");

    }

    public void CheckForInventory()
    {
        if (inventoryController==null)
        {
            inventoryController=GetComponent<InventoryController>();
        }
    }
    public void LoadGame()
    {
        if(File.Exists(saveLocation))
        {
            print(saveLocation);
            currentSaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = currentSaveData.playerPosition;
            CheckForInventory();

            InventoryController.instance.SetInventoryItems(currentSaveData.inventorySaveData);

            //SoundMixerManager mixer = SoundMixerManager.instance;
            //mixer.SetMasterVolume(saveData.masterVolume);
            //mixer.SetMusicVolume(saveData.musicVolume);
            //mixer.SetSoundFXVolume(saveData.soundFXVolume);

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
        if (File.Exists(saveLocation))
        {
            currentSaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint.position;
            inventoryController.ClearInventory();
            Cursor.visible = false;


            Debug.Log("RESET GAME");
        }
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

    //save music and volume


}