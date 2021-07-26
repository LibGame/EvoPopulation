using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EvoCard : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float _damage = 20;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _reproduceSpeed = 15;
    [SerializeField] private float _health = 20;
    public int Level = 1;
    private float _incriseFactor = 2;
    [SerializeField] private EvoRareType _rareType;
    public int[] CostPerLvl;
    public int[] DamagePerLvl;
    public int[] ReproducePreLvl;
    public int[] HealthPerLvl;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI SpeedText;
    //public TextMeshProUGUI ReproduceSpeedText;
    public TextMeshProUGUI HealthText;
    public GameObject[] LevelImage;
    public int Cost;
    public GameObject BuyIcon;
    public GameObject BuyButton;
    public GameObject SelectButton;
    public GameObject UpgradeButton;
    public GameObject SelectedButton;
    public TextMeshProUGUI UpgradeButtonText;
    public bool IsOpen;

    private void Start()
    {
 
        if (PlayerPrefs.HasKey(id.ToString()))
        {
            IsOpen = true;
            Level = PlayerPrefs.GetInt("Evo" + id.ToString());
            IncreseParameters();
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            SelectedButton.SetActive(false);
            UpgradeButton.SetActive(true);
            UpgradeButtonText.text = CostPerLvl[Level - 1].ToString();
            if (Level < 5)
            {
                UpgradeButtonText.text = CostPerLvl[Level - 1].ToString();
            }
            else
            {
                UpgradeButtonText.text = "Max";
            }
        }

        if (PlayerPrefs.HasKey("SelectedEvo"))
        {
            int id = PlayerPrefs.GetInt("SelectedEvo");
            if (this.id == id)
            {
                SelectedButton.SetActive(true);
                UpgradeButton.SetActive(true);
                BuyIcon.SetActive(true);
                if (Level < 5)
                {
                    UpgradeButtonText.text = CostPerLvl[Level - 1].ToString();
                }
                else
                {
                    UpgradeButtonText.text = "Max";
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("3", 3);
            PlayerPrefs.SetInt("Evo" + "3", 1);
            if(id == 3)
                SelectEvo();

        }
    }


    public void IncreseParameters()
    {
        DamageText.text = DamagePerLvl[Level - 1].ToString();
        SpeedText.text = ReproducePreLvl[Level - 1].ToString();
        //ReproduceSpeedText.text = _reproduceSpeed.ToString();
        HealthText.text = HealthPerLvl[Level - 1].ToString();
        SetLvlIcons(Level);
    }

    public void UpgradeEvo()
    {
        if (Level < 5 && PlayerPrefs.HasKey(id.ToString()))
        {
            int cost = CostPerLvl[Level - 1];
            if (cost <= Menu.Instance.Coins)
            {
                Upgrade(); 
                UpgradeButtonText.text = CostPerLvl[Level - 1].ToString();
                Menu.Instance.DecreaseCoins(cost);
                if (Level == 5)
                {
                    UpgradeButtonText.text = "Max";
                }
            }
        }
        else
        {
            UpgradeButtonText.text = "Max";
        }
    }

    public void BuyEvo()
    {
        if (Menu.Instance.Gems >= Cost && !PlayerPrefs.HasKey(id.ToString()))
        {
            IsOpen = true;
            PlayerPrefs.SetInt(id.ToString(), id);
            PlayerPrefs.SetInt("Evo" + id.ToString(), Level);
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
            UpgradeButton.SetActive(true);
            Menu.Instance.DecreaseGems(Cost);
            SelectEvo();
        }

    }

    public void SelectEvo()
    {
        PlayerPrefs.SetInt("SelectedEvo", id);
        ParamSettings.Instance.CloseAllSelect();
        SelectedButton.SetActive(true);
        SelectButton.SetActive(false);
        BuyIcon.SetActive(true);
    }

    public void SetLvlIcons(int lvl)
    {
        for (int i = 0; i < LevelImage.Length; i++)
        {
            if (i < lvl)
            {
                LevelImage[i].SetActive(true);
            }
            else
            {
                LevelImage[i].SetActive(false);
            }
        }
    }

    public void Upgrade()
    {
        if(Level <= 5)
        {
            Level++;
            IncreseParameters();
            PlayerPrefs.SetInt("Evo" + id.ToString(), Level);
        }
        else
        {
            UpgradeButtonText.text = "Max";
        }
    }

    public void Select()
    {

        if (PlayerPrefs.HasKey(id.ToString()))
        {
            BuyButton.SetActive(false);
            SelectButton.SetActive(true);
        }

        if (PlayerPrefs.HasKey("SelectedEvo"))
        {
            if(PlayerPrefs.GetInt("SelectedEvo") == id)
            {
                SelectedButton.SetActive(true);

            }
            else
            {
                SelectedButton.SetActive(false);
            }
        }

    }
}
