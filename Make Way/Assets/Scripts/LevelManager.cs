using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Player player;
    private AudioManager audioManager;

    [SerializeField]
    private GameObject[] signalPoints = new GameObject[1];

    [SerializeField]
    private Volume volume;

    [SerializeField]
    private GameObject keyToggle;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        audioManager = FindObjectOfType<AudioManager>();

        keyToggle.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistances();

        if(player.GetKeyStatus()) keyToggle.SetActive(true);
        else keyToggle.SetActive(false);
    }

    private void CheckDistances(){
        float min = 1;
        Vector3 playerPosition = Camera.main.ScreenToWorldPoint(player.transform.position);

        for(int i = 0; i < signalPoints.Length; i++){
            Vector3 signalPosition = Camera.main.ScreenToWorldPoint(signalPoints[i].transform.position);
            float distance = Vector2.Distance(playerPosition, signalPosition);
            if(distance < min) min = distance;
        } 

        float value = min*5;

        FilmGrain filmGrain;
        volume.profile.TryGet(out filmGrain);
        filmGrain.intensity.value = value;

        audioManager.ChangeVolume("Static", value-0.5f);

        value = min*7;

        ColorAdjustments colorAdj;
        volume.profile.TryGet(out colorAdj);
        colorAdj.colorFilter.value = new Color(1-value, 1-value, 1-value, 1);
    }
}
