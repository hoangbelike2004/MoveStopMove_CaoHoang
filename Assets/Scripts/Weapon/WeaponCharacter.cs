using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharacter : MonoBehaviour
{
    [SerializeField] protected float speed,timeDeactiveWeapon;
    protected Character characterParent;
    public void SetCharracterParent(Character newCharacter)
    {
        characterParent = newCharacter;
    }
}
