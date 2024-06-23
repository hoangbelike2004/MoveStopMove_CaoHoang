using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : WeaponBase
{
    //[SerializeField] private Character character;
    [SerializeField] Rigidbody rb;

    public WeaponType type;
    public static UnityAction<Character, Character> ActionAddScore;
    private void Awake()
    {
        sizeinitial = transform.localScale;
    }

    public override void OnInit()
    {
        base.OnInit();
        Invoke(nameof(OnDespawn), timeDeactiveWeapon);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        transform.localScale = sizeinitial;
    }
}
