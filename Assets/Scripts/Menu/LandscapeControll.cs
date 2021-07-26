using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandscapeControll : MonoBehaviour
{
    public Image landscape;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == false)
        {
            if (gameObject.GetComponent<RectTransform>().offsetMin.x == 100)
            {
                if (landscape.color.a <= 1)
                {
                    landscape.color += new Color(0, 0, 0, 0.1f);
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

                }
            }
            else
            {
                if (gameObject.GetComponent<RectTransform>().offsetMin.x == -300 || gameObject.GetComponent<RectTransform>().offsetMin.x == -299.9999f || gameObject.GetComponent<RectTransform>().offsetMin.x == 500 || gameObject.GetComponent<RectTransform>().offsetMin.x == 500.0001f)
                {
                    if (landscape.color.a >= 0)
                    {
                        landscape.color -= new Color(0, 0, 0, 0.1f);
                    }
                    gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
            }
        }
    }
}
