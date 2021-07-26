using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] private string[] _levelName;
    [SerializeField] private bool _isOpen;
    [SerializeField] private TextMeshProUGUI _levelComplite;
    [SerializeField] private int _levelId;
    [SerializeField] private Image _compliteBar;
    [SerializeField] private Color _selectLevelColor;
    [SerializeField] private Image _levelBackGround;
    [SerializeField] private Animation _gemBonusAnimation;
    [SerializeField] private Animation _coinsBonusAnimation;
    [SerializeField] private int MaxCups;

    void Start()
    {
        if (PlayerPrefs.HasKey("Cups" + _levelId.ToString()))
        {
            float a = PlayerPrefs.GetInt("Cups" + _levelId.ToString());
            _levelComplite.text = a.ToString() + "/" + MaxCups.ToString();
            _compliteBar.fillAmount = a / MaxCups;

            if(a > 40)
            {
                if (!PlayerPrefs.HasKey("GemBonus" + _levelId.ToString()))
                {
                    Menu.Instance.IncreaseGems(40);
                    _gemBonusAnimation.Play();
                    PlayerPrefs.SetInt("GemBonus" + _levelId.ToString(), 1);
                }
                else
                {
                    _gemBonusAnimation.gameObject.SetActive(true);
                }
            }

            if (a > 75)
            {

                if (!PlayerPrefs.HasKey("CoinBonus" + _levelId.ToString()))
                {
                    Menu.Instance.IncreaseGems(100);
                    _coinsBonusAnimation.Play();
                    PlayerPrefs.SetInt("CoinBonus" + _levelId.ToString(), 1);
                }
                else
                {
                    _coinsBonusAnimation.gameObject.SetActive(true);
                }
            }

        }
        else
        {
            _levelComplite.text = "0" + "/" + MaxCups.ToString();
        }
    }

    public void SelectLevel()
    {
        _levelBackGround.color = _selectLevelColor;
        Play.Instance.LevelName = _levelName[Random.Range(0, _levelName.Length)];
        Play.Instance.IsOpen = _isOpen;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Triger")
        {
            SelectLevel();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Triger")
        {
            Play.Instance.IsOpen = false;
            _levelBackGround.color = Color.white;
        }
    }

}
