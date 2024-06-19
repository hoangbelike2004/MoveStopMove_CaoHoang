using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuitType
{
    suit1 = 0, suit2 = 1, noneSuit = 2
}

[CreateAssetMenu(menuName = "SuitData")]
public class SuitData : ScriptableObject
{
    public List<SuitdItem> suits;

    public GameObject GetSuit(SuitType type)
    {
        return suits[(int)type].suitprefab;
    }
}
