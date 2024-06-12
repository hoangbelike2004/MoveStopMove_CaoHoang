using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShieldType
{
    khienden,khiendo,noneShield
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
