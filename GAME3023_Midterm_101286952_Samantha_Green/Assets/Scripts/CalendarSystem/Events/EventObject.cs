using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EventName", menuName = "CalendarEvents/EventData")]

public class EventObject : ScriptableObject
{
    [Header("Date and Time Settings")]
    public string eventName;
    public string triggerSeason;
    public string triggerMonth;
    public int triggerDay;
    public int endDay;

    [Header("Prefab Settings")] 
    public GameObject eventPrefab;

    [Header("Calender Color Settings")] 
    public Color highlightColor = Color.white;

    public string GetCurrentDate(int currentDay, string currentMonth)
    {
        return $"Day {currentDay} of {currentMonth}";
    }

    public string GetCurrentSeason(string currentSeason)
    {
        return currentSeason;
    }

    public bool IsEventTriggered(int currentDay, string currentSeason)
    {
        if (currentDay >= triggerDay && currentDay <= endDay &&
            string.Equals(currentSeason, triggerSeason, StringComparison.OrdinalIgnoreCase))
        {
            return true;  
        }
        return false;  
    }

    public bool IsEventEnded(int currentDay, string currentSeason)
    {
        if (currentDay > endDay && string.Equals(currentSeason, triggerSeason, StringComparison.OrdinalIgnoreCase))
        {
            return true;  
        }
        return false; 
    }

    public void TriggerEvent(CalendarManager calendarManager)
    {
        int currentDay = calendarManager.clock.days;
        string currentSeason = calendarManager.clock.month.seasonType;

        if (IsEventTriggered(currentDay, currentSeason))
        {
            Debug.Log($"It is the day of your event: {eventName}!");

            
            if (eventPrefab != null)
            {
                eventPrefab.SetActive(true);
            }
            else
            {
                Debug.Log("No prefab set for the event.");
            }
        }
        else
        {
           
            if (eventPrefab != null)
            {
                eventPrefab.SetActive(false);
            }
        }

        // Check if the event has ended
        if (IsEventEnded(currentDay, currentSeason))
        {
            if (eventPrefab != null)
            {
                eventPrefab.SetActive(false); 
            }
            Debug.Log($"The event {eventName} has ended.");
        }
    }

    public void HighlightEventDay(int currentDay, string currentSeason, TMP_Text dayText)
    {
        if (IsEventTriggered(currentDay, currentSeason))
        {
            dayText.color = highlightColor;
        }
        else
        {
            dayText.color = Color.white;
        }
    }
}
