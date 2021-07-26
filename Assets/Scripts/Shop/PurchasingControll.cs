using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchasingControll : MonoBehaviour
{
    public Chest Chest;
    public GameObject BackGround;

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "1")
        {
            Menu.Instance.IncreaseGems(40);
        }
        else if (product.definition.id == "2")
        {
            Menu.Instance.IncreaseGems(220);
        }
        else if (product.definition.id == "3")
        {
            Menu.Instance.IncreaseGems(480);

        }
        else if (product.definition.id == "4")
        {
            Menu.Instance.IncreaseGems(1200);
        }
        else if (product.definition.id == "5")
        {
            Menu.Instance.IncreaseGems(2100);
        }
        else if (product.definition.id == "SO")
        {
            Menu.Instance.IncreaseGems(450);
            BackGround.SetActive(true);
            Chest.OpenChest();
            BackGround.GetComponent<ChestBackGround>().Award = Chest.CreatedPrize; 
            PlayerPrefs.SetInt("Offer", 1);
            PlayerPrefs.Save();
        }
        else if (product.definition.id == "SO2")
        {
            Menu.Instance.IncreaseGems(10);
            Menu.Instance.IncreaseCoins(1000);
            if (!PlayerPrefs.HasKey("4"))
            {
                PlayerPrefs.SetInt("4", 4);
                PlayerPrefs.SetInt("Evo" + "4", 1);
            }
            PlayerPrefs.SetInt("Offer", 1);
            PlayerPrefs.Save();
        }


    }

}
