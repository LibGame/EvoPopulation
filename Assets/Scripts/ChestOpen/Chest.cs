using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chest : MonoBehaviour
{
    public GameObject CreatedPrize;
    public int AwardsAmount;


    public virtual void CheckToRevard(int id)
    {

    }

    public virtual void OpenChest()
    {
    }
}

