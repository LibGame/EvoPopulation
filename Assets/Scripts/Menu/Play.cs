using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public static Play Instance;

    [HideInInspector] public string LevelName;
    public bool IsOpen = true;
    public CurtainControll curtain;

    private void Awake()
    {
        Instance = GetComponent<Play>();

    }

    public void StartPlay()
    {
        if (Menu.Instance.Stamina > 0)
        {
            curtain.gameObject.SetActive(true);
            curtain.on = true;
        }
    }
}
