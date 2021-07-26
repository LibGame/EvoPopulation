using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsControll : MonoBehaviour
{

    [SerializeField] private Slider _soundScroll;
    [SerializeField] private Slider _musicScroll;

    private void Start()
    {
        _soundScroll.onValueChanged.AddListener(delegate { ChangeSoundScroll(); });
        _musicScroll.onValueChanged.AddListener(delegate { ChangeMusicScroll(); });

    }

    public void ChangeSoundScroll()
    {
        SoundController.Instance.ChangeSoundVolume(_soundScroll.value);
    }

    public void ChangeMusicScroll()
    {
        SoundController.Instance.ChangeMusicVolume(_musicScroll.value);
    }

    public void Like()
    {
        Application.OpenURL("http://unity3d.com/");

    }
    public void Rate()
    {
        Application.OpenURL("http://unity3d.com/");

    }
    public void Support()
    {
        Application.OpenURL("http://unity3d.com/");

    }
}
