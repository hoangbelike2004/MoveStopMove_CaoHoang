using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PantType
{
    pantBatman = 0, pantChambi = 1, pantComy = 2, pantDabao = 3,
    pantOnion = 4, pantPokemon = 5, pantRainbow = 6, nonePant = 7
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
