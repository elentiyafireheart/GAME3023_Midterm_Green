using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeasonName", menuName = "ScriptableObjects/SeasonData")]

public class SeasonObject : ScriptableObject
{

    public string seasonName;
    public int numberOfDays;
}
