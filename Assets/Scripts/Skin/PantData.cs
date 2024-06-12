using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PantType
{
    pantBatman, pantChambi, pantComy, pantDabao, pantOnion,pantPokemon, pantRainbow,nonePant
}
[CreateAssetMenu(fileName = "Pant")]
public class PantData : ScriptableObject
{
    public List<PantItem> pants;

    public Material GetPants(PantType type)
    {
        return pants[(int)type].pantMaterial;
    }
    public Sprite GetSpritePant(PantType type)
    {
        return pants[(int)type].iconpant;
    }
}
