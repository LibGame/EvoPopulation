using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    [SerializeField] private Text _timer;
    public int LevelID;
    public int time = 180; //значение счетчика в секундах
    string timer; //значение для преобразования секуды в часовой формат 00:00:00
    [SerializeField] private Text _staminaText;
    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private Image _staminaImage;
    public static Interface Instance;
    public SkillSetter[] Skills;

    public Image FirstPlace;
    public Image SecondPlace;
    public Image ThirdPlace;

    public int Coins;
    public int Gems;
    public int Cups;

    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI GemsText;
    public TextMeshProUGUI CupsText;
    public int RedAmount;
    public int BlueAmount;
    public int YellowAmount;

    private void Awake()
    {
        Instance = GetComponent<Interface>();
    }

    public void StartTimer()
    {
        StartCoroutine(Cowntdown());
    }

    public void SetSkills(GameObject[] skills)
    {
        for(int i = 0; i < skills.Length; i++)
        {
            Skills[i].SetSkill(skills[i]);
        }
    }

    public void Timer()
    {
        time--; //отнимаем 1 с значения time
        var ts = System.TimeSpan.FromSeconds(time); //преобразуем time в секунды
        timer = string.Format("{0}:{1}", "0" + ts.Minutes, ts.Seconds); //меняем формат отображения секунд
        _timer.text = timer;
        if (time <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    public void Update()
    {
        if (!GameManager.Instance.IsEndGame)
            StaminaUpdate();
    }


    public void StaminaUpdate()
    {
        GlobalSettings.Instance.InterfaceSettings.Stamina = Mathf.MoveTowards(GlobalSettings.Instance.InterfaceSettings.Stamina, 10, GlobalSettings.Instance.InterfaceSettings.IncreseStaminaSpeed * Time.deltaTime);
        _staminaImage.fillAmount = GlobalSettings.Instance.InterfaceSettings.Stamina / 10;
        _staminaText.text = (System.Math.Floor(GlobalSettings.Instance.InterfaceSettings.Stamina)).ToString();
    }

    public void SortingByPlace()
    {
        Coins += Random.Range(100, 300);
        Cups += Random.Range(10, 30);
        Gems += Random.Range(15, 40);

        List<int> scores = new List<int>();
        scores.Add(GameManager.Instance.RedAmount);
        scores.Add(GameManager.Instance.YellowAmount);
        scores.Add(GameManager.Instance.BlueAmount);
        RedAmount = GameManager.Instance.RedAmount;
        BlueAmount = GameManager.Instance.BlueAmount;
        YellowAmount = GameManager.Instance.YellowAmount;
        int[] places = BubbleSort(scores.ToArray());
        
        foreach(var p in places)
        {
            Debug.Log(p);
        }

        GameObject f = GetEvoObject(places[2],0);
        GameObject s = GetEvoObject(places[1],1);
        GameObject t = GetEvoObject(places[0],2);

        Sprite firstPlace = f.GetComponent<SpriteRenderer>().sprite;
        Sprite secondPlace = s.GetComponent<SpriteRenderer>().sprite;
        Sprite thirdPlace = t.GetComponent<SpriteRenderer>().sprite;

        FirstPlace.sprite = firstPlace;
        SecondPlace.sprite = secondPlace;
        ThirdPlace.sprite = thirdPlace;

        PlayerPrefs.SetInt("Cups" + LevelID.ToString(), PlayerPrefs.GetInt("Cups" + LevelID.ToString()) + Cups) ;
        PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + Gems);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + Coins);

        CupsText.text = Cups.ToString();
        GemsText.text = Gems.ToString();
        CoinsText.text = Coins.ToString();
        PlayerPrefs.Save();
    }


    private int[] BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            for (int j = 0; j < array.Length - 1; j++)
                if (array[j] > array[j + 1])
                {
                    int t = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = t;
                }

        return array;
    }

    public GameObject GetEvoObject(int index , int place)
    {
        int first = RedAmount;
        int second = BlueAmount;
        int third = YellowAmount;
        
        if(first == index)
        {
            return GameManager.Instance.RedEvos[0].gameObject;
        }else if (second == index)
        {
            _result.text = place + 1 + GetEndingPlaceWord(place + 1) + " Place";
            return GameManager.Instance.BlueEvos[0].gameObject;
        }
        else if (third == index)
        {
            return GameManager.Instance.YellowEvos[0].gameObject;
        }

        return null;
    }


    private string GetEndingPlaceWord(int place)
    {
        switch (place)
        {
            case 1:
                return "st";
            case 2:
                return "nd";
            case 3:
                return "rd";
        }
        return "st";
    }

    public void Replay()
    {
        SoundController.Instance.PlayButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SoundController.Instance.PlayButtonClick();
        SceneManager.LoadScene("Menu");
    }


    public IEnumerator Cowntdown()
    {
        while (true)
        {
            if (GameManager.Instance.RedAmount <= 0 && GameManager.Instance.YellowAmount <= 0)
            {
                GameManager.Instance.EndGame();
            }
            else if (GameManager.Instance.BlueAmount <= 0)
            {
                GameManager.Instance.EndGame();
            }

            Timer();
            yield return new WaitForSeconds(1); //ждем 1 секунду
        }
    }
}
