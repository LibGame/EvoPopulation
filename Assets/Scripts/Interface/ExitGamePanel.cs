using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGamePanel : MonoBehaviour
{
    public GameObject Panel;
    
    public void QuitFromGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClosePane()
    {
        Panel.SetActive(false);
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);
    }

}
