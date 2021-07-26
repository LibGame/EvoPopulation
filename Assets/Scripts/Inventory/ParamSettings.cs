using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EvoRareType { Start , Normal ,Gold , Lucky , Epic}

public class ParamSettings : MonoBehaviour
{
    public static ParamSettings Instance;
    public EvoCard[] AllEvoCards;
 
    private void Awake()
    {
        Instance = GetComponent<ParamSettings>();
    }

    public void CloseAllSelect()
    {
        foreach(var evo in AllEvoCards)
        {
            evo.SelectedButton.SetActive(false);
            evo.BuyIcon.SetActive(false);
            if (evo.IsOpen)
                evo.SelectButton.SetActive(true);
        }
    }

}
