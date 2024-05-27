using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { hammer, lollipop };


[CreateAssetMenu(menuName = "WeaponData")]

public class WeaponData1 : ScriptableObject
{
    public List<WeaponItem> weapons;

    public Weapon GetWeapon(WeaponType type)
    {
        return weapons[(int)type].prefabWeapon;
    }
}
