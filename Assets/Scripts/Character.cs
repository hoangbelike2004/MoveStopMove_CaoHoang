using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speed,valueSize,valuesizetmp,rangeAttack = 7f;
    protected bool isDie;
    public const string IDLE = "Idle", ATTACK = "Attack",DIE = "Dead",RUN = "Run", DANCE_WIN = "DanceWin";
    private string currentAnim;
    [SerializeField] private Animator anim;
    //[SerializeField] private SphereCollider sphereCollider;
    protected Vector3 CurrentPos;
    [SerializeField] private int planeLayer,layerWeapon;
    public Collider[] hitcollider;
    public List<GameObject> listgameObjectHitcollider;
    [SerializeField] protected GameObject _weaponFake;
    [SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected CapsuleCollider capsuleCollider;
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

    protected virtual void ChangeWeapon()
    {

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
        ChangeAnim(ATTACK);

    }
    public void DeActiveAttack()
    {
        isAttack = false;
        
    }
    public void InstanWeapon()
    {
        
        
        GameObject bullet = Instantiate(weaponPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Weapon>().SetCharracterParent(this);
        

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
    protected virtual void CheckSight()
    {
        
            hitcollider = Physics.OverlapSphere(transform.position, rangeAttack);
        
        
        
        if (hitcollider.Length > 0)
        {
           for(int i = 0;i< hitcollider.Length;i++)
            {
                if (!listgameObjectHitcollider.Contains(hitcollider[i].gameObject) && hitcollider[i].gameObject.layer != planeLayer
                && hitcollider[i].gameObject != this.gameObject && hitcollider[i].gameObject.layer != layerWeapon)
                {
                    listgameObjectHitcollider.Add(hitcollider[i].gameObject);
                }

            }

        }
        if (listgameObjectHitcollider.Count != 0)
        {
            if (listgameObjectHitcollider[0].activeSelf == false)
            {
                Debug.LogError("chay vao xoa");
                listgameObjectHitcollider.RemoveAt(0);

                CurrentPos = Vector3.zero;
                isAttack = false;
            }
            if (listgameObjectHitcollider.Count != 0)
            {
                CurrentPos = listgameObjectHitcollider[0].transform.position;
                if(this.gameObject.tag == "Player")
                {
                    listgameObjectHitcollider[0].GetComponent<Bot>()._selectAttackOfPlayer.SetActive(true);
                }
                
            }
            if (Vector3.Distance(transform.position, listgameObjectHitcollider[0].transform.position) > rangeAttack)
            {
                if (this.gameObject.tag == "Player")
                {
                    listgameObjectHitcollider[0].GetComponent<Bot>()._selectAttackOfPlayer.SetActive(false);
                }
                listgameObjectHitcollider.RemoveAt(0);
               
            }
            
        }
        if (listgameObjectHitcollider.Count == 0)
        {

            CurrentPos = Vector3.zero;
            isAttack = false;
        }


        }


}
