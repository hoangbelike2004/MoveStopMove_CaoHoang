using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeState { notyetowned = 0, selected = 1, haveowned = 2}
[Serializable]
public class WeaponItem
{
    public Weapon prefabWeapon;
    public Boomerang prefabBoom;
    public WeaponType wpType;
    public int price;
    public Sprite iconWeapon;
    public GameObject weaponFake;
    public TypeState typeState;
    public string name;
}
[Serializable]
public class HatItem
{
    public GameObject hatprefab;
    public HatType hattype;
    public int price;
    public Sprite iconhat;
    public TypeState typeState;
}
[Serializable]
public class PantItem
{
    public Material pantMaterial;
    public PantType pantType;
    public int price;
    public Sprite iconpant;
    public TypeState typeState;
}
[Serializable]
public class ShieldItem
{
    public GameObject shieldprefab;
    public ShieldType shieldtype;
    public int price;
    public Sprite iconshield;
    public TypeState typeState;
}

[Serializable]
public class SuitdItem
{
    public GameObject suitprefab;
    public SuitType suittype;
    public int price;
    public Sprite iconsuit;
    public TypeState typeState;
}


