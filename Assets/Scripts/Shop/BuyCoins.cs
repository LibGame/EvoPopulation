using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCoins : MonoBehaviour
{
    public int Cost;
    public int Coins;

    public void Buy()
    {
        SoundController.Instance.PlayButtonClick();
        if (Menu.Instance.Gems >= Cost)
        {
            Menu.Instance.IncreaseCoins(Coins);
            Menu.Instance.DecreaseGems(Cost);
        }
    }
}
