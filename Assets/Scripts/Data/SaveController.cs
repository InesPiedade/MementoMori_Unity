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
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        //SoundMixerManager mixer = SoundMixerManager.instance;
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = inventoryController.GetInventoryItems(),

            //masterVolume = mixer.GetMasterVolume(),
            //musicVolume = mixer.GetMusicVolume(),
            //soundFXVolume = mixer.GetSoundFXVolume(),

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
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

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