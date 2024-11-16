using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public EventObject eventObjectData;
    public TimeClock clock;

    [Header("Text Labels for UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI currentYearText;
    public TextMeshProUGUI currentMonthText;
    public TextMeshProUGUI currentDayText;
    public TextMeshProUGUI currentSeasonText;

    [Header("Day/Night Text Labels")]
    public TextMeshProUGUI nightText;
    public TextMeshProUGUI dayText;

    [Header("Highlight Colors for Calender Days")]
    public Color highlightColor = Color.green;
    public Color previousDayColor = Color.red;

    [Header("Prefabs for Calender")]
    public GameObject calendarPrefab;
    private int previousDay = -1;
    private MonthObject previousMonth;
    public List<GameObject> dayPanels;

    void Start()
    {

        if (dayPanels.Count == 0 && calendarPrefab != null)
        {
            // Populate the dayPanels list from the prefab
            for (int i = 0; i < calendarPrefab.transform.childCount; i++)
            {
                dayPanels.Add(calendarPrefab.transform.GetChild(i).gameObject);
            }

        }


        if (clock.days <= 0)
        {
            clock.days = 1;
        }

        InvokeRepeating("UpdateClock", 1f, clock.secondSpeed);
        HighlightCurrentDay();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenCalendar();
        }

        HighlightCurrentDay();

        if (eventObjectData != null && eventObjectData.IsEventTriggered(clock.days, clock.month.seasonType))
        {
            TriggerEvent();
        }

        if (eventObjectData != null && eventObjectData.IsEventEnded(clock.days, clock.month.seasonType))
        {
            EndEvent();
        }
    }

    public GameObject calendar;
    public bool isActive;

    public void OpenCalendar()
    {
        // Opening the Calendar Menu pauses the game
        // Closing the Calendar Menu resumes the game

        if (calendar == isActive)
        {
            calendar.SetActive(false);
            isActive = false;
            Time.timeScale = 1f;
        }
        else
        {
            isActive = true;
            calendar.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void UpdateClock()
    {
        var timeClock = clock;
        var monthObject = timeClock.month;

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
        currentSeasonText.text = clock.month.seasonType;
        currentDayText.text = clock.days.ToString();

        string hours = clock.hh.ToString("D2");
        string minutes = clock.mm.ToString("D2");

        timeText.text = $"{hours}:{minutes}";
    }

    public TMP_Text GetDayText(int day)
    {
        // Assuming you have a way to get day text based on the current day
        return dayPanels[day].GetComponentInChildren<TMP_Text>();  // Adjust this as per your setup
    }

    private void HighlightCurrentDay()
    {
        if (clock.month != previousMonth)
        {
            ResetAllDaysToWhite(); 
            previousMonth = clock.month;
        }

        if (clock.days != previousDay)
        {
            if (previousDay >= 0 && previousDay < dayPanels.Count)
            {
                var previousPanel = dayPanels[previousDay];
                var previousText = previousPanel.GetComponentInChildren<TextMeshProUGUI>();
                if (previousText != null)
                {
                    previousText.color = Color.red; 
                }
            }

            for (int i = 0; i < dayPanels.Count; i++)
            {
                var currentPanel = dayPanels[i];
                var currentText = currentPanel.GetComponentInChildren<TextMeshProUGUI>();

                if (i == clock.days - 1)
                {
                    if (currentText != null)
                    {
                        currentText.color = Color.green;
                    }
                }
                
                else if (i < clock.days - 1)
                {
                    if (currentText != null)
                    {
                        currentText.color = previousDayColor; 
                    }
                }
                
                else if (i >= clock.days)
                {
                    if (currentText != null)
                    {
                        
                        if (eventObjectData != null && eventObjectData.IsEventTriggered(i + 1, clock.month.seasonType))
                        {
                            currentText.color = eventObjectData.highlightColor;
                        }
                        
                        else
                        {
                            currentText.color = Color.white;  
                        }
                    }
                }
            }

            previousDay = clock.days;
        }
    }

    private void ResetAllDaysToWhite()
    {
        foreach (var panel in dayPanels)
        {
            var dayText = panel.GetComponentInChildren<TextMeshProUGUI>();
            if (dayText != null)
            {
                dayText.color = Color.white;  // Reset all days to white
            }
        }
    }

    private void TriggerEvent()
    {
        Debug.Log($"Event triggered: {eventObjectData.eventName} on {eventObjectData.GetCurrentDate(clock.days, clock.month.monthName)} in {clock.month.monthName}");
        
        if (eventObjectData.IsEventTriggered(clock.days, clock.month.monthName))
        {
          
            if (eventObjectData.eventPrefab != null)
            {
                eventObjectData.eventPrefab.SetActive(true);  // Show the event's prefab
                Debug.Log("Event prefab activated.");
            }
            else
            {
                Debug.Log("No prefab added for this event.");
            }
        }
        else
        {
            // Event is not triggered, hide its prefab
            if (eventObjectData.eventPrefab != null)
            {
                eventObjectData.eventPrefab.SetActive(false);
                Debug.Log("Event prefab deactivated.");
            }
        }
    }

    private void EndEvent()
    {
        Debug.Log($"Event ended: {eventObjectData.eventName}");

        // Deactivate the event prefab if it's set
        if (eventObjectData.eventPrefab != null)
        {
            eventObjectData.eventPrefab.SetActive(false);
        }
    }
}
