using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClockUpdater : MonoBehaviour
{
    public TimeClock clock;

    // Text Labels
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI currentSeasonText;
    public TextMeshProUGUI currentYearText;
    public TextMeshProUGUI currentMonthText;
    public TextMeshProUGUI currentDayText;


    // Display Icons
    public TextMeshProUGUI nightText;
    public TextMeshProUGUI dayText;

    void Start()
    {
        InvokeRepeating("UpdateClock", 1f, clock.secondSpeed);
    }

    void Update()
    {

    }

    private void UpdateClock()
    {
        if (clock.night)
        {
            dayText.enabled = false;
            nightText.enabled = true;
        }
        else
        {
            dayText.enabled = true;
            nightText.enabled = false;
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
