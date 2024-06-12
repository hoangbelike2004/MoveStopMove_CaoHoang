using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuitType
{
    suit1,suit2,noneSuit
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
