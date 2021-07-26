using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestTimer : MonoBehaviour
{
    public int time = 86400; //значение счетчика в секундах
    string timer; //значение для преобразования секуды в часовой формат 00:00:00
    [SerializeField] private TextMeshProUGUI _timer;
    public GameObject[] Chests;

    void Awake()
    {
        if (PlayerPrefs.HasKey("TimeOnExitChestQueue"))
        {
            System.TimeSpan date = System.DateTime.Now - System.DateTime.Parse(PlayerPrefs.GetString("TimeOnExitChestQueue"));
            time = 86400 - (int)date.TotalSeconds;
            StartCoroutine(TimerCounter());
        }
    }


    public void Timer()
    {
        time--; //отнимаем 1 с значения time
        var ts = System.TimeSpan.FromSeconds(time); //преобразуем time в секунды
        timer = string.Format("{0}:{1}", ts.Hours, ts.Minutes); //меняем формат отображения секунд
        _timer.text = timer;
        if (time <= 0)
        {
            int i = Random.Range(0, 2);

            if(i == 0)
            {
                Chests[0].SetActive(true);
                Chests[1].SetActive(false);
            }else if (i == 0)
            {
                Chests[1].SetActive(true);
                Chests[0].SetActive(false);
            }

            time = 86400;
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
        PlayerPrefs.SetString("TimeOnExitChestQueue", System.DateTime.Now.ToShortTimeString());
        PlayerPrefs.Save();
    }
}
