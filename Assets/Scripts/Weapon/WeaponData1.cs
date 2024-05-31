using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { hammer, lollipop ,axe,boomerang, arrow };


[CreateAssetMenu(menuName = "WeaponData")]

public class WeaponData1 : ScriptableObject
{
    public List<WeaponItem> weapons;

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

}
