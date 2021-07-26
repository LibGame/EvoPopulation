using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoForm : MonoBehaviour
{
    public Evo EvoSource;
    
    public void DestroyFrom()
    {
        Destroy(gameObject, 3f);
    }
}
