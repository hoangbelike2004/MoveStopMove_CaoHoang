using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : WeaponCharacter
{
    //[SerializeField] private Character character;
    [SerializeField] Rigidbody rb;
    public WeaponType type;
    Character target;
    Character current;
    public static UnityAction<Character,Character> ActionAddScore;
    IEnumerator Start()
    {
        Character newCharacter = Cache.GetCharacterInCache(characterParent);
        float valuesize = newCharacter.GetValueSize()*transform.localScale.x; //tang size cua vu khi
        timeDeactiveWeapon = newCharacter.GetRangeAttack()/speed;//thay doi time khi quang duong thay doi
        transform.localScale = new Vector3(transform.localScale.x+valuesize, transform.localScale.y+valuesize, transform.localScale.z+valuesize);
        rb.AddForce(speed* transform.forward, ForceMode.Impulse);
        transform.Rotate(-90, 0, 0);
        Invoke(nameof(ActiveWeapon), timeDeactiveWeapon);
        //transform.rotation = Quaternion.Euler(-90, 0, 0);
        while (true) {
            
            transform.Rotate(0, 0, 360 * 3 * Time.deltaTime);
            yield return null;
        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
       if(other.GetComponent<Character>() != characterParent&&other.GetComponent<Character>()!= null)
        {
            target = Cache.GetCharacteOfColliderInCache(other);
            current = Cache.GetCharacterInCache(characterParent);
            gameObject.SetActive(false);
            target.Die();
            // ActionAddScore?.Invoke(current,target);
            current.AddScore();

            
        }
    }
    void ActiveWeapon()
    {
        gameObject.SetActive(false);
    }
}
