using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource AudioSource;
    private float musicVolume = 1f;
    public Slider volumeSlider;
    public GameObject ObjectMusic;
    void Start()
    {
        
        ObjectMusic = GameObject.FindWithTag("GameMusic");  //AudioSource hat den Tag "GameMusic" 
        AudioSource = ObjectMusic.GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("volume"); //beim start wird der gespeicherte Float abgerufen
        AudioSource.volume = musicVolume; //für den scenenwechsel, wird 1x durchgelaufen in der neuen scene
        volumeSlider.value = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume); //speichert in der aktuellen scene den Float für Volume
    }
    public void updateVolume(float volume)
    {
        musicVolume = volume;
    }
}
