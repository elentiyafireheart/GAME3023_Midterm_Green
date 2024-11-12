using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockUpdater : MonoBehaviour
{
    public TimeClock clock;

    // Text Labels
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI currentYearText;
    public TextMeshProUGUI currentMonthText;
    public TextMeshProUGUI currentDayText;


    // Display Icons
    public GameObject moon;
    public GameObject sun;

    void Start()
    {
        InvokeRepeating("UpdateClock", 1f, clock.secondSpeed);
    }

    private void UpdateClock()
    {
        if (clock.night == true)
        {
            sun.SetActive(false);
            moon.SetActive(true);
        }
        else
        {
            sun.SetActive(true);
            moon.SetActive(false);
        }

        currentYearText.text = clock.yy.ToString();
        currentMonthText.text = clock.month.monthName;
        currentDayText.text = clock.days.ToString();

        string hours = clock.hh.ToString();
        string minutes = clock.mm.ToString();

        if (hours.Length < 1)
        {
            hours = "0" + clock.hh.ToString();
        }

        if (minutes.Length <= 1)
        {
            minutes = "0" + clock.mm.ToString();
        }

        timeText.text = hours + ":" + minutes;
    }
}
