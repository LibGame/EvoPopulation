using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyChest : MonoBehaviour
{
    public int Cost;
    public Chest Chest;
    public GameObject BackGround;

    public void Buy()
    {
        SoundController.Instance.PlayButtonClick();
        if(Menu.Instance.Gems >= Cost)
        {
            BackGround.SetActive(true);
            Chest.OpenChest();
            Menu.Instance.DecreaseGems(Cost);
            BackGround.GetComponent<ChestBackGround>().Award = Chest.CreatedPrize;
        }
    }
}
