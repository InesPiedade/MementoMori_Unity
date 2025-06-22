using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] GameObject dog;

    [SerializeField] private UiManager uiManager;
    [SerializeField] private SaveController saveController;
    [SerializeField] private AudioClip barkSoundClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (collision)
        {
            Debug.Log("DOG");
            dog.SetActive(true);
            MusicManager.instance.PlayRunMusic();
            MusicManager.instance.StopForestMusic();
            uiManager.ObjectiveDog();
            // ui manager run tip
            saveController.SaveGame();
            // dog sound play 
            SoundFXManager.instance.PlaySoundFXClip(barkSoundClip, transform, 1f);
            // corroutine to play again after certain time 
            StartCoroutine(BarkTimer());
        }
        else
        {
            MusicManager.instance.StopRunMusic();
            MusicManager.instance.PlayForestMusic();
            uiManager.ObjectiveFlower();
            dog.SetActive(false);
        }
    }
    
    private IEnumerator BarkTimer()
    {
        yield return new WaitForSeconds(7f);
        SoundFXManager.instance.PlaySoundFXClip(barkSoundClip, transform, 1f);
    }
}
