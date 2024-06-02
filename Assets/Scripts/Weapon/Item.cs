using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public enum TypeState { notyetowned, selected, haveowned }
public class WeaponItem
{
    public Weapon prefabWeapon;
    public WeaponType wpType;
    public int price;
    public Sprite iconWeapon;
    public GameObject weaponFake;
    public TypeState typeState;
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
    public GameObject pantprefab;
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
public class Item : MonoBehaviour
{

}


