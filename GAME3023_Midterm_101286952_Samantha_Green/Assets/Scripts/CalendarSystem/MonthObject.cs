using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonthName", menuName = "ScriptableObjects/MonthData")]

public class MonthObject : ScriptableObject
{

    public string monthName;
    public int numberOfDays;
}
