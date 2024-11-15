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

    public TextMeshProUGUI currentYearText;
    public TextMeshProUGUI currentMonthText;
    public TextMeshProUGUI currentDayText;
    public TextMeshProUGUI currentSeasonText;

    // Display Icons
    public TextMeshProUGUI nightText;
    public TextMeshProUGUI dayText;

    public GameObject calendarPrefab;
    public List<GameObject> dayPanels;
    public Color highlightColor = Color.green;
    public Color previousDayColor = Color.red;
    private int previousDay = -1;

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
            clock.days = 1;  // If days is not initialized, set it to 1 (or any default value)
        }

        InvokeRepeating("UpdateClock", 1f, clock.secondSpeed);
        HighlightCurrentDay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenCalendar();
        }

        HighlightCurrentDay();
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

    private void HighlightCurrentDay()
    {
        if (clock.days != previousDay)
        {
            // Reset color for previous day
            if (previousDay >= 0 && previousDay < dayPanels.Count)
            {
                var previousPanel = dayPanels[previousDay];
                var previousText = previousPanel.GetComponentInChildren<TextMeshProUGUI>();
                if (previousText != null)
                {
                    previousText.color = Color.white; // Reset to original color (white)
                }
            }

            for (int i = 0; i < clock.days - 1; i++) 
            {
                if (i >= 0 && i < dayPanels.Count)
                {
                    var previousPanel = dayPanels[i];
                    var previousText = previousPanel.GetComponentInChildren<TextMeshProUGUI>();
                    if (previousText != null)
                    {
                        previousText.color = previousDayColor; // Set previous days to red
                    }
                }
            }

            int adjustedDayIndex = clock.days - 1;
            
            if (adjustedDayIndex >= 0 && adjustedDayIndex < dayPanels.Count)
            {
                var currentPanel = dayPanels[adjustedDayIndex];
                var currentText = currentPanel.GetComponentInChildren<TextMeshProUGUI>();
                if (currentText != null)
                {
                    currentText.color = highlightColor; // Change color to highlight
                }
            }

            previousDay = clock.days;
        }
    }
}
