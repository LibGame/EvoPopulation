using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicChest : Chest
{
    public GameObject EpicEvo;
    public int ID;

    public GameObject Gold;

    public override void OpenChest()
    {
        CheckToRevard(Random.Range(0, 2));
    }

    public override void CheckToRevard(int id)
    {
        switch (id)
        {
            case 0:
                CreatedPrize = Instantiate(EpicEvo, new Vector3(0, 0, 0), Quaternion.identity);
                if (!PlayerPrefs.HasKey(ID.ToString()))
                {
                    PlayerPrefs.SetInt(ID.ToString(), ID);
                    PlayerPrefs.SetInt("Evo" + ID.ToString(), 1);
                }
                else
                {
                    CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                    Menu.Instance.IncreaseCoins(500);
                }
                break;
            case 1:
                CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                Menu.Instance.IncreaseCoins(500);
                break;
            case 2:
                CreatedPrize = Instantiate(EpicEvo, new Vector3(0, 0, 0), Quaternion.identity);
                if (!PlayerPrefs.HasKey(ID.ToString()))
                {
                    PlayerPrefs.SetInt(ID.ToString(), ID);
                    PlayerPrefs.SetInt("Evo" + ID.ToString(), 1);
                }
                break;
            case 3:
                CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                Menu.Instance.IncreaseCoins(500);
                break;

        }
    }

}
