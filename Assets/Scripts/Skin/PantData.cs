using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PantType
{
    pantBatman,pantchambi,pantcomy
}
[CreateAssetMenu(fileName = "Pant")]
public class Pants : ScriptableObject
{
    public List<PantItem> pants;

    public GameObject GetPants(PantType type)
    {
        return pants[(int)type].pantprefab;
    }
}
