using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    [HideInInspector] public int Stamina;
    public int Gems;
    public int Coins;

    public Text GemsText;
    public Text CoinsText;

    [SerializeField] private GameObject _inventory;

    public static Menu Instance;
    public int time = 780; //значение счетчика в секундах
    string timer; //значение для преобразования секуды в часовой формат 00:00:00
    [SerializeField] private Text StaminaText;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        Instance = GetComponent<Menu>();
 
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Stamina"))
        {
            IncreaseStamina(5);
        }

        if (PlayerPrefs.HasKey("TimeOnExit"))
        {
            System.TimeSpan date = System.DateTime.Now - System.DateTime.Parse(PlayerPrefs.GetString("TimeOnExit"));
            time = 86400 - (int)date.TotalSeconds;
            StartCoroutine(TimerCounter());
            IncreaseStamina((int)time / 780 - 1);
        }
        else
        {
            StartCoroutine(TimerCounter());
        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            IncreaseCoins(PlayerPrefs.GetInt("Coins"));
        }
        else
        {
            IncreaseCoins(500);
        }

        if (PlayerPrefs.HasKey("Gems"))
        {
            IncreaseGems(PlayerPrefs.GetInt("Gems"));
        }
        else
        {
            IncreaseGems(100);
        }

        scoreText.text = (PlayerPrefs.GetInt("Cups1") + PlayerPrefs.GetInt("Cups0") + PlayerPrefs.GetInt("Cups2") + PlayerPrefs.GetInt("Cups3")).ToString();
    }

    public void OpenInventory(bool isOpen)
    {
        if (isOpen)
        {
            _inventory.SetActive(true);
        }
        else
        {
            _inventory.SetActive(false);
        }
    }

    public void IncreaseCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("Coins", Coins);
        CoinsText.text = Coins.ToString();
    }

    public void DecreaseCoins(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt("Coins", Coins);
        CoinsText.text = Coins.ToString();
    }

    public void IncreaseGems(int amount)
    {
        Gems += amount;
        PlayerPrefs.SetInt("Gems", Gems);
        GemsText.text = Gems.ToString();
    }

    public void DecreaseGems(int amount)
    {
        Gems -= amount;
        PlayerPrefs.SetInt("Gems", Gems);
        GemsText.text = Gems.ToString();
    }

    public void IncreaseStamina(int a)
    {
        if(Stamina < 5)
        {
            int index = 0;
            while (true)
            {
                if (index >= 5 || Stamina >= 5 || a <= 0)
                {
                    break;
                }
                Stamina++;
                StaminaText.text = Stamina.ToString() + "/5";
                a--;
                index++;
                
            }
            PlayerPrefs.SetInt("Stamina", Stamina);
        }
    }


    public void DecreaseStamina(int Amount)
    {
        Stamina -= Amount;
        PlayerPrefs.SetInt("Stamina", Stamina);
        StaminaText.text = Stamina.ToString() + "/5";
    }

    //public void OnPurchaseComplete(Product product)
    //{
    //    if (product.definition.id == "1")
    //    {
    //        PlayerPrefs.SetInt("IsBuy", 1);
    //        IncreaseGems(20);
    //        StartCoroutine(TimerCounter());
    //    }
    //}

    public void Timer()
    {
        time--; 
        if (time <= 0 || time % 780 == 0)
        {
            time = 780;
            IncreaseStamina(1);
        }
    }

    private IEnumerator TimerCounter()
    {
        while (true)
        {
            Timer();

            yield return new WaitForSeconds(1);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("TimeOnExit", System.DateTime.Now.ToShortTimeString());
        PlayerPrefs.Save();
    }

}
