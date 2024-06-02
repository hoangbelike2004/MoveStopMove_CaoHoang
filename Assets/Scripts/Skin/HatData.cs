using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HatType { hatArrowhat,hatCowboy,hatCrown,hatEar}
[CreateAssetMenu(menuName = "Hat")]
public class HatData : ScriptableObject
{
    public List<HatItem> hats;

    public GameObject GetHat(HatType type)
    {
        return hats[(int)type].hatprefab;
    }
}
