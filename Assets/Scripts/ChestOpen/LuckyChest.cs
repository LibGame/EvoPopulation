using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyChest : Chest
{
    public GameObject[] LuckyEvo;
    public int[] ID;

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
                int random = Random.Range(0, LuckyEvo.Length);
                CreatedPrize = Instantiate(LuckyEvo[random], new Vector3(0, 0, 0), Quaternion.identity);
                if (!PlayerPrefs.HasKey(ID[random].ToString()))
                {
                    PlayerPrefs.SetInt(ID[random].ToString(), ID[random]);
                    PlayerPrefs.SetInt("Evo" + ID[random].ToString(), 1);
                }
                else
                {
                    CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                    Menu.Instance.IncreaseCoins(1000);
                }
                break;
            case 1:
                CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                Menu.Instance.IncreaseCoins(1000);
                break;
            case 2:
                int random1 = Random.Range(0, LuckyEvo.Length);
                CreatedPrize = Instantiate(LuckyEvo[random1], new Vector3(0, 0, 0), Quaternion.identity);
                if (!PlayerPrefs.HasKey(ID[random1].ToString()))
                {
                    PlayerPrefs.SetInt(ID[random1].ToString(), ID[random1]);
                    PlayerPrefs.SetInt("Evo" + ID[random1].ToString(), 1);
                }
                break;
            case 3:
                CreatedPrize = Instantiate(Gold, new Vector3(0, 0, 0), Quaternion.identity);
                Menu.Instance.IncreaseCoins(1000);
                break;

        }
    }

}
