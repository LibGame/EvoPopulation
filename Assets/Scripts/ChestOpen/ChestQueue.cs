using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestQueue : MonoBehaviour
{
    public bool IsQueue;
    public Chest[] Queue;
    public GameObject BackGround;

    public void OpenQueue()
    {
        IsQueue = true;
        BackGround.SetActive(true);
        StartCoroutine(OpenChests());
    }

    public IEnumerator OpenChests()
    {
        int i = 0;
        GameObject prize = null;
        while (true)
        {
            if (prize != null)
                Destroy(prize);
            Queue[i].CheckToRevard(i);
            prize = Queue[i].CreatedPrize;
            BackGround.GetComponent<ChestBackGround>().Award = prize;
            i++;
            if (i >= Queue.Length)
            {
                IsQueue = false;
                break;
            }
            yield return new WaitForSeconds(2);
        }
    }

}
