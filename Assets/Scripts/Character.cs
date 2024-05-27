using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speed,valueSize,valuesizetmp,rangeAttack = 7f;
    protected bool isDie,isSelectEnemy;
    public const string IDLE = "Idle", ATTACK = "Attack",DIE = "Dead",RUN = "Run", DANCE_WIN = "DanceWin";
    private string currentAnim;
    [SerializeField] private Animator anim;
    //[SerializeField] private SphereCollider sphereCollider;
    protected Vector3 CurrentPos;
    [SerializeField] private int planeLayer,layerWeapon;
    public Collider[] hitcollider;
    public List<GameObject> listgameObjectHitcollider;
    [SerializeField] protected GameObject _weaponFake;
    [SerializeField] protected Weapon weaponPrefab;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] protected WeaponData1 weaponData1;
    public bool isAttack;
    public float time,timer;


    void Start()
    {
        OnInit();
    }
    //protected virtual void Move()
    //{

    //}
    protected virtual void OnInit()
    {
        isDie = false;
        speed = 10f;
        valueSize = 0.1f;
        time = 0;
        isAttack = false;
        capsuleCollider.enabled = true;
        
    }

    public virtual void OnDespawn(GameObject newobject)
    {
       
    }



    public virtual void ChangeAnim(string nameAnim)
    {
            anim.SetTrigger(nameAnim);
    }

    protected virtual void ChangeWeapon(Weapon newwp)
    {
        weaponPrefab = newwp;
    }

    protected virtual void ChangeHat()
    {

    }

    protected virtual void ChangePant()
    {

    }

    protected virtual void AttackRang()
    {
        
    }

    protected virtual void UpSize()
    {
        transform.localScale = new Vector3(transform.localScale.x+valueSize,transform.localScale.y+valueSize,transform.localScale.z+valueSize);
        rangeAttack += valueSize*5f;
        valuesizetmp += valueSize;
        
    }
    public float GetRangeAttack()
    {
        return rangeAttack;
    }
    public float GetValueSize()
    {
        return valuesizetmp;
    }

    public virtual void Attack()
    {

        CurrentPos = Vector3.zero;
        ChangeAnim(ATTACK);
        

    }
    public void DeActiveAttack()
    {
        isAttack = false;
        
    }
    public void InstanWeapon()
    {
        
        
        Weapon bullet = Instantiate(weaponPrefab, firePos.position, firePos.rotation);
        Weapon newWeapon = Cache.GetWeaponInCache(bullet);
        newWeapon.SetCharracterParent(this);
        

    }
   
    public virtual void Die()
    {
        isDie = true;
        capsuleCollider.enabled = false;
        speed = 0;
        ChangeAnim(DIE);
        
    }
    protected virtual void Win()
    {
        ChangeAnim(DANCE_WIN);
    }
    public int Compare(Collider x, Collider y)
    {
        float distanceX = Vector3.Distance(x.transform.position, transform.position);
        float distanceY = Vector3.Distance(y.transform.position, transform.position);

        if (distanceX < distanceY)
        {
            return -1;
        }
        else if (distanceX > distanceY)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    protected virtual void CheckSight()
    {
        
        hitcollider = Physics.OverlapSphere(transform.position, rangeAttack);
        int value = 0;

        Array.Sort(hitcollider, Compare);
        if(hitcollider.Length != 0)
        {     
            for (int i = 0; i < hitcollider.Length; i++)
            {
                if (hitcollider[i].gameObject.layer != planeLayer
                    && hitcollider[i].gameObject != this.gameObject && hitcollider[i].gameObject.layer != layerWeapon)
                {
                    Character newChar = Cache.GetCharacteOfColliderInCache(hitcollider[i]);
                    CurrentPos = newChar.transform.position;
                    value = i;
                    break;
                }
            }
            if(value != 0 && gameObject.tag != "Bot")
            {
                if (hitcollider.Contains(hitcollider[value]))
                {
                    hitcollider[value].GetComponent<Bot>().IsSelect(true);
                    
                }
                if(Vector3.Distance(transform.position, hitcollider[value].transform.position) > rangeAttack)
                {
                    hitcollider[value].GetComponent<Bot>().IsSelect(false);
                }
                
            }
            
        }
    }
   

}
