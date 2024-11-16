using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonthName", menuName = "MonthlyCalendar/MonthData")]

public class MonthObject : ScriptableObject
{

    public string monthName;
    public string seasonType;
    public int numberOfDays;
}
