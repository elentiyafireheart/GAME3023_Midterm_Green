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
   // public TimeClock time;
   // public MonthObject monthObject;

    [Header("Date and Time Settings")]
    public string eventName;
    public string triggerSeason;
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
        // Event triggers when the current day is within the start and end days, and season matches
        if (currentDay >= triggerDay && currentDay <= endDay &&
            string.Equals(currentSeason, triggerSeason, StringComparison.OrdinalIgnoreCase))
        {
            return true;  // Event should trigger on this day
        }
        return false;  // Event does not trigger
    }

    public bool IsEventEnded(int currentDay, string currentSeason)
    {
        if (currentDay > endDay && string.Equals(currentSeason, triggerSeason, StringComparison.OrdinalIgnoreCase))
        {
            return true;  // Event has ended
        }
        return false;  // Event has not ended
    }

    public void TriggerEvent(CalendarManager calendarManager)
    {
        int currentDay = calendarManager.clock.days;
        string currentSeason = calendarManager.clock.month.seasonType;

        if (IsEventTriggered(currentDay, currentSeason))  // Check if event should trigger
        {
            Debug.Log($"It is the day of your event: {eventName}!");

            // Activate the event prefab
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
            // Deactivate the event prefab if it's not triggered today
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
                eventPrefab.SetActive(false);  // Deactivate prefab when event ends
            }
            Debug.Log($"The event {eventName} has ended.");
        }
    }

    public void HighlightEventDay(int currentDay, string currentSeason, TMP_Text dayText)
    {
        if (IsEventTriggered(currentDay, currentSeason))
        {
            // If the current day is part of this event, highlight it with the event's color
            dayText.color = highlightColor;
        }
        else
        {
            // If it's not an event day, set it to white (non-event days)
            dayText.color = Color.white;
        }
    }
}
