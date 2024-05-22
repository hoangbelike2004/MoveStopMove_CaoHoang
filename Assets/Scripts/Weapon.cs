using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : WeaponCharacter
{
    //[SerializeField] private Character character;
    [SerializeField] Rigidbody rb;
    public Vector3 target;
    IEnumerator Start()
    {
        rb.AddForce(speed* transform.forward, ForceMode.Impulse);
        transform.Rotate(-90, 0, 0);
        //transform.rotation = Quaternion.Euler(-90, 0, 0);
        while (true) {
            
            transform.Rotate(0, 0, 360 * 3 * Time.deltaTime);
            yield return null;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
