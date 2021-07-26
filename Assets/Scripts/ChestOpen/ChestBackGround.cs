using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestBackGround : MonoBehaviour, IPointerDownHandler
{
    public GameObject Award;
    public ChestQueue ChestQueue;

    public void FindChestQueue()
    {
        ChestQueue = FindObjectOfType<ChestQueue>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ChestQueue != null)
        {
            if (!ChestQueue.IsQueue)
            {
                Destroy(Award);
                gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(Award);
            gameObject.SetActive(false);
        }
    }
}
