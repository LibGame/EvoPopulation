using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurtainControll : MonoBehaviour
{
    public bool on;
    public bool onLevel;

    // Start is called before the first frame update
    public void On()
    {
        SoundController.Instance.PlayButtonClick();
        if (Menu.Instance.Stamina > 0)
        {
            on = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(on == true)
        {
            gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);
        }
        if (gameObject.GetComponent<Image>().color.a >= 1 && on == true) {
            Menu.Instance.DecreaseStamina(1);
            PlayerPrefs.SetString("TimeOnExit", System.DateTime.Now.ToShortTimeString());
            PlayerPrefs.Save();
            SceneManager.LoadScene(Play.Instance.LevelName);
            on = false;
        }

        if (onLevel == true)
        {
            gameObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);
        }
        if (gameObject.GetComponent<Image>().color.a <= 0 && onLevel == true)
        {
            onLevel = false;
        }

    }

}
