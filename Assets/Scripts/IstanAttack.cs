using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IstanAttack : MonoBehaviour
{
    
    [SerializeField] private Character _character;
    [SerializeField] private GameObject _weaponFake;
    public void RunAttack()
    {
        _character.AttackActive();
       _weaponFake.SetActive(false);
    }
    public void WhenAttackFinished()
    {
        
        _character.DeActiveAttack();
        
    }
}
