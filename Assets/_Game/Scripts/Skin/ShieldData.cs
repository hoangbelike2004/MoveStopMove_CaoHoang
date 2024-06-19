using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShieldType
{
    khienden = 0, khiendo = 1, noneShield = 2
}
[CreateAssetMenu(fileName = "Shield")]
public class ShieldData : ScriptableObject
{
    public List<ShieldItem> shields;

    public GameObject GetShield(ShieldType type)
    {
        return shields[(int)type].shieldprefab;
    }
    public Sprite GetIconShield(ShieldType type)
    {
        return shields[(int)type].iconshield;
    }
}
