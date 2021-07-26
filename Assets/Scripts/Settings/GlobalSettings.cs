using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public InterfaceSettings InterfaceSettings;
    public static GlobalSettings Instance;

    private void Awake()
    {
        Instance = GetComponent<GlobalSettings>();
    }
}
