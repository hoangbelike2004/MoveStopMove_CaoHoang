using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponBase : GameUnit
{
    [SerializeField] protected float speed,timeDeactiveWeapon;

    protected Character characterParent;
    protected Character target;
    protected Character current;

    private Vector3 direction;
    protected Vector3 sizeinitial;

    protected void Start()
    {
        transform.Rotate(-90, 0, 0);
        OnInit();
    }

    protected void Update()
    {
        if (gameObject.activeSelf)
        {
            Move(direction);
        }
    }

    public virtual void OnInit()
    {
        //Character newCharacter = Cache.GetCharacterInCache(characterParent);
        float valuesize = characterParent.GetValueSize() * transform.localScale.x; //tang size cua vu khi
        timeDeactiveWeapon = characterParent.GetRangeAttack() / speed;//thay doi time khi quang duong thay doi
        transform.localScale = new Vector3(transform.localScale.x + valuesize, transform.localScale.y + valuesize, transform.localScale.z + valuesize);
        transform.Rotate(-90, 0, 0);
    }
    public void GetScale()
    {
        Debug.Log(transform.localScale.x);
    }
    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public void SetTarget(Vector3 target)
    {
        direction = (target - TF.position).normalized;
    }
    public virtual void Move(Vector3 dir)
    {
        direction.y = 0;
        TF.position += direction * speed * Time.deltaTime;
        TF.Rotate(0, 0, 360 * 3 * Time.deltaTime);
    }
    public void SetCharracterParent(Character newCharacter)
    {
        characterParent = newCharacter;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != characterParent && other.GetComponent<Character>() != null)
        {
            target = Cache.GetCharacteOfColliderInCache(other);
            //current = Cache.GetCharacterInCache(characterParent);
            OnDespawn();
            target.Die();
            // ActionAddScore?.Invoke(current,target);
            characterParent.AddScore();
        }
    }
}
