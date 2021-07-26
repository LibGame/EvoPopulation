using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerName : MonoBehaviour {
	public TMP_InputField inputF;
    public TextMeshProUGUI nameText;

    public GameObject Panel;
    private bool c = false;

    public string named;

    // Use this for initialization
    void Start () {

        named = PlayerPrefs.GetString("name");
        inputF.text = PlayerPrefs.GetString("name");
        nameText.text = PlayerPrefs.GetString("name");

if(PlayerPrefs.GetInt("CheckName") != 5)
        {
            PlayerPrefs.SetInt("CheckName", 5);
            PlayerPrefs.SetString("name", "King");

        }
        else
        {
            gameObject.SetActive(false);
            name = PlayerPrefs.GetString("name");
            inputF.text = PlayerPrefs.GetString("name");
        }
    }

    // Update is called once per frame
    void Update () {
        

        if (c == false)
        {
            inputF.text = PlayerPrefs.GetString("name");
            nameText.text = PlayerPrefs.GetString("name");
        }
        else
        {
            nameText.text = inputF.text;
        }
	}
	public void ApplyName()
	{
        PlayerPrefs.SetString("name", inputF.text);
        c = false;
    }

    public void Changed()
    {
        PlayerPrefs.SetString("name", "");
        c = true;
    }
}
