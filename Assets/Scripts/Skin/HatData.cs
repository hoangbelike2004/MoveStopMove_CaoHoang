using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HatType { hatArrowhat,hatCowboy,hatCrown,hatEar,hat,murom, earphone}
[CreateAssetMenu(menuName = "Hat")]
public class HatData : ScriptableObject
{
    public List<HatItem> hats;

    public GameObject GetHat(HatType type)
    {
        return hats[(int)type].hatprefab;
    }
    public Sprite GetIcon(HatType type)
    {
        return hats[(int)type].iconhat;
    }
}
