using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //[Header("Event List")] public List<EventObject> eventObjects;
    //private CalendarManager calendarManager;

    //private void Awake()
    //{
    //    calendarManager = FindObjectOfType<CalendarManager>();
    //}

    //public void UpdateEvents()
    //{
    //    foreach (var eventObject in eventObjects)
    //    {
    //        if (eventObject != null)
    //        {
    //            eventObject.TriggerEvent(calendarManager);
    //        }
    //    }
    //}

    //public List<string> GetTriggeredEventNames(int currentDay, string currentSeason)
    //{
    //    List<string> triggeredEventNames = new List<string>();

    //    foreach (var eventObject in eventObjects)
    //    {
    //        if (eventObject != null && eventObject.IsEventTriggered(currentDay, currentSeason))
    //        {
    //            triggeredEventNames.Add(eventObject.eventName);
    //        }
    //    }

    //    return triggeredEventNames;
    //}

    //public void HighlightEventDays(int currentDay, string currentSeason)
    //{
    //    foreach (var eventObject in eventObjects)
    //    {
    //        if (eventObject != null)
    //        {
    //            // Check if this event is triggered on the current day
    //            eventObject.HighlightEventDay(currentDay, currentSeason, calendarManager.GetDayText(currentDay));
    //        }
    //    }
    //}
}
