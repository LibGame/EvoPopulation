using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateController : MonoBehaviour
{
    public float Stamina;

    void Start()
    {
        if (PlayerPrefs.HasKey("h"))
        {
            int hour = PlayerPrefs.GetInt("h");
            int mounth = PlayerPrefs.GetInt("m");
            int day = PlayerPrefs.GetInt("d");

            System.DateTime dateLast = new System.DateTime(0, mounth, day, hour, 0, 0);
            System.DateTime datenow = System.DateTime.Now;

            System.TimeSpan timeSpan = datenow - dateLast;
            int minutesBeforeLeaf = (int)timeSpan.TotalMinutes; // столько минут прошло с последнего захода

            Stamina += minutesBeforeLeaf / 11.5f;
            if(Stamina >= 10)
            {
                Stamina = 0;
            }
        }

        System.DateTime date = System.DateTime.Now;
        PlayerPrefs.SetInt("h", date.Hour);
        PlayerPrefs.SetInt("m", date.Month);
        PlayerPrefs.SetInt("d", date.Day);

    }

}
