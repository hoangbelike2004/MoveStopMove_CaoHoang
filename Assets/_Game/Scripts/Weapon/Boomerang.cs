using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boomerang : WeaponBase
{
    //[SerializeField] private Character character;
    [SerializeField] Rigidbody rb;
    public WeaponType type;
    private float time;
    private void Awake()
    {
        sizeinitial = transform.localScale;
    }

    public override void OnInit()
    {
        base.OnInit();
        time = 0;
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Move(Vector3 dir)
    {
        time += Time.deltaTime;
        if (time < timeDeactiveWeapon)
        {
            base.Move(dir);
        }
        else
        {
            TF.position = Vector3.MoveTowards(TF.position, characterParent.transform.position, speed * Time.deltaTime);
            TF.Rotate(0, 0, 360 * 3 * Time.deltaTime);
            if (Vector3.Distance(transform.position, characterParent.transform.position) < 0.1f)
            {
                OnDespawn();
            }
        }


    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        TF.localScale = sizeinitial;
    }
}
