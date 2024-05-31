using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstanAttack : MonoBehaviour
{
    
    [SerializeField] private Character _character;
    [SerializeField] private Transform _weaponFaketf;
    [SerializeField] private GameObject parent;
    public void RunAttack()
    {
        _character.InstanWeapon();//create a weapon
       _weaponFaketf.gameObject.SetActive(false);//deactive Weapon fake
    }
    public void WhenAttackFinished()
    {
        
        _character.DeActiveAttack();

    }
    public void Dead()
    {
        parent.SetActive(false);
    }
}
