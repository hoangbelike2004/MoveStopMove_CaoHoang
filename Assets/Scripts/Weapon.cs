using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponCharacter
{
    //[SerializeField] private Character character;
    [SerializeField] Rigidbody rb;
    public Vector3 target;
    BoxCollider box;
    Collider[] col;
    IEnumerator Start()
    {
        float valuesize = characterParent.GetComponent<Character>().GetValueSize()*transform.localScale.x; //tang size cua vu khi
        timeDeactiveWeapon = characterParent.GetComponent<Character>().GetRangeAttack()/speed;//thay doi time khi quang duong thay doi
        transform.localScale = new Vector3(transform.localScale.x+valuesize, transform.localScale.y+valuesize, transform.localScale.z+valuesize);
        Debug.Log(timeDeactiveWeapon);
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
        //Debug.Log(other.name);
       if(other.GetComponent<Character>() != characterParent)
        {
            other.gameObject.SetActive(false);
        }
    }
    void ActiveWeapon()
    {
        gameObject.SetActive(false);
    }
}
