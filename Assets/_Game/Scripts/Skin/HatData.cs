using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum HatType { hatArrowhat = 0,hatCowboy = 1,hatCrown = 2,hatEar = 3,hat = 4,murom = 5, earphone = 6,nonehat = 7}
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
