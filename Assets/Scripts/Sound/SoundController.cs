using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public AudioSource Music;
    public AudioSource ButtonSound;

    private void Awake()
    {
        Instance = GetComponent<SoundController>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            ButtonSound.volume = PlayerPrefs.GetFloat("Sound");
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            Music.volume = PlayerPrefs.GetFloat("Music");
        }

    }


    public void ChangeMusicVolume(float volume)
    {
        Music.volume = volume;
        PlayerPrefs.SetFloat("Music", Music.volume);
    }

    public void ChangeSoundVolume(float volume)
    {
        ButtonSound.volume = volume;
        PlayerPrefs.SetFloat("Sound", ButtonSound.volume );
    }

    public void PlayButtonClick()
    {
        ButtonSound.Play();
    }
}
