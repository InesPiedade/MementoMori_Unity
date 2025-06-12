using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject floatingPlatforms;
    [SerializeField] private GameObject shadowCave;

    private void Start()
    {
        floatingPlatforms.SetActive(false);
        shadowCave.SetActive(true);
    }
    public void VisionOn()
    {
        floatingPlatforms.SetActive(true);
        shadowCave.SetActive(false);
    }

    public void VisionOff()
    {
        floatingPlatforms.SetActive(false);
        shadowCave.SetActive(true);
    }
}
