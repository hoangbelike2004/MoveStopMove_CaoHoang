using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum HatType { hatArrowhat,hatCowboy,hatCrown,hatEar,hat,murom, earphone,nonehat}
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
    public TypeState GetTypeStateHat(HatType type)
    {
        return hats[(int)type].typeState;
    }
}
