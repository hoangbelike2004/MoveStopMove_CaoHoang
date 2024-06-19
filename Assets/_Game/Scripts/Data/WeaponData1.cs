using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { hammer, candy_0 ,axe,boomerang, arrow };


[CreateAssetMenu(menuName = "WeaponData")]

public class WeaponData1 : ScriptableObject
{
    public List<WeaponItem> weapons;


    public WeaponType GetTypeWeapon(int a)
    {
        return weapons[a].wpType;
    }
    public Weapon GetWeapon(WeaponType type)
    {
        return weapons[(int)type].prefabWeapon;
    }
    public GameObject GetWeaponGOB(WeaponType type)
    {
        return weapons[(int)type].weaponFake;
    }

    public Sprite GetSprite(WeaponType type)
    {
        return weapons[(int)type].iconWeapon;
    }
    public TypeState GetTypeState(WeaponType type)
    {
        return weapons[((int)type)].typeState;
    }
    public int GetPriceWeapon(WeaponType type)
    {
        return weapons[((int)type)].price;
    }

}
