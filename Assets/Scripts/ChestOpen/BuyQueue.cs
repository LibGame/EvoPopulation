using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyQueue : MonoBehaviour
{
    public ChestBackGround ChestBackGround;
    public int Cost;

    public void Buy()
    {
        SoundController.Instance.PlayButtonClick();
        if (Menu.Instance.Gems >= Cost)
        {

            ChestQueue chest = FindObjectOfType<ChestQueue>();
            chest.OpenQueue();
            Menu.Instance.DecreaseGems(Cost);
            ChestBackGround.FindChestQueue();
            ChestBackGround.gameObject.SetActive(true);
        }
    }
}
