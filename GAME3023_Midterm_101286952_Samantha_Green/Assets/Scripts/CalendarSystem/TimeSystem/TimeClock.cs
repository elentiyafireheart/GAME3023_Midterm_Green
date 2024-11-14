using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeClock : MonoBehaviour
{
  
    public MonthObject[] scriptableMonths = new MonthObject[12];
    public MonthObject month;

    // Clock Properties
    public int yy;
    public int days;
    public int actualMonth;
    public int hh;
    public int mm;
    public bool night = false;

    
    public float secondSpeed;

    public Light2D globalLight;
    public Light2D playerLight;
    int hoursPassed;

    //public SeasonObject[] scriptableSeasons = new SeasonObject[4];
    //public SeasonObject seasons;

    void Start()
    {
        month = scriptableMonths[1];
        InvokeRepeating("TimePasses", secondSpeed, secondSpeed);
        InvokeRepeating("DayNightSwitch", 2f, 2f);
    }

    void FixedUpdate()
    {
        SwitchLights();
    }

    private void SwitchLights()
    {
        float targetIntensity;

        if (night)
        {
            targetIntensity = 0.1f;
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, targetIntensity, Time.deltaTime * 0.2f);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 1f, Time.deltaTime * 1f);
        }
        else
        {
            targetIntensity = 0.95f;
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, targetIntensity, Time.deltaTime * 0.2f);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, 0f, Time.deltaTime * 1f);
        }
    }

    public void TimePasses()
    {
        mm++;

        if (mm > 59)
        {
            mm = 0;
            hh++;
            hoursPassed++;
            CheckClock();

        }
    }

    private void CheckClock()
    {
        if (hh > 23)
        {
            hh = 0;
            days++;

            if (days > month.numberOfDays)
            {
                days = 1;
                actualMonth++;
                month = scriptableMonths[actualMonth];

                if (actualMonth >= 12)
                {
                    actualMonth = 0;
                    yy++;
                }
            }
        }
    }

    public void DayNightSwitch()
    {
        if (hh < 21 && hh > 5)
        {
            night = false;
        }
        else
        {
            night = true;
        }
    }
}
