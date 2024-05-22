using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speed,valueSize,rangeAttack = 5f;
    protected bool isDie;
    protected const string IDLE = "Idle", ATTACK = "Attack",DIE = "Die",RUN = "Run", DANCE_WIN = "DanceWin";
    private string currentAnim;
    [SerializeField] private Animator anim;
    //[SerializeField] private SphereCollider sphereCollider;
    protected Vector3 CurrentPos;
    [SerializeField] private LayerMask botLayer;
    public Collider[] hitcollider;
    public List<GameObject> listgameObjectHitcollider;
    [SerializeField] protected GameObject _weaponFake;
    [SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected Transform firePos;
    protected bool isAttack;
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
    }

    protected virtual void OnDespawn()
    {

    }



    protected virtual void ChangeAnim(string nameAnim)
    {
        //if(currentAnim != nameAnim)
        //{
            //anim.ResetTrigger(nameAnim);
            //currentAnim = nameAnim;
            //Debug.Log(nameAnim);
            anim.SetTrigger(nameAnim);
       // }
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
        //_rangeAttackIMG.transform.localScale = new Vector3(_rangeAttackIMG.transform.localScale.x + valueSize, _rangeAttackIMG.transform.localScale.y + valueSize,
        //    0f);
        //_sphererange.transform.localScale = new Vector3(_sphererange.transform.localScale.x + .5f, _sphererange.transform.localScale.y, _sphererange.transform.localScale.x + .5f);
    }
    protected virtual void Attack()
    {
        
          
        ChangeAnim(ATTACK);
        time = 0;
        //Debug.Log(2);
        
        
      

    }
    public void DeActiveAttack()
    {
        isAttack = false;
        //time = timer;
        
    }
    public void AttackActive()
    {
        
        
        GameObject bullet = ObjectPooling.Instance.SpawnGameObjectFromPool(PoolType.Weapon,firePos.position,firePos.rotation);
        

    }
   
    protected virtual void Die()
    {
        ChangeAnim(DIE);
    }
    protected virtual void Win()
    {
        ChangeAnim(DANCE_WIN);
    }
    protected virtual void CheckSight()
    {

        //Collider other;
        //other = hitcollider[0];
        //int index = Array.IndexOf(hitcollider, other);
        hitcollider = Physics.OverlapSphere(transform.position, rangeAttack, botLayer);
        if (hitcollider.Length > 0)
        {
            
            //enemyCurrentPos = hitcollider[0].transform.position;
            
            if (!listgameObjectHitcollider.Contains(hitcollider[0].gameObject))
            {
                listgameObjectHitcollider.Add(hitcollider[0].gameObject);
            }
            
        }
        if (listgameObjectHitcollider.Count != 0)
        {
            
            if (hitcollider.Length != 0)
            {
                CurrentPos = listgameObjectHitcollider[0].transform.position;
                listgameObjectHitcollider[0].GetComponent<Bot>()._selectAttackOfPlayer.SetActive(true);
            }
            //Debug.Log(listgameObjectHitcollider[0].gameObject.name);
            if (Vector3.Distance(transform.position, listgameObjectHitcollider[0].transform.position) > rangeAttack)
            {
                //Debug.Log(Vector3.Distance(transform.position, listgameObjectHitcollider[0].transform.position));
                listgameObjectHitcollider[0].GetComponent<Bot>()._selectAttackOfPlayer.SetActive(false);
                listgameObjectHitcollider.RemoveAt(0);
               
            }
            else if(listgameObjectHitcollider[0].activeSelf == false)
            {
                listgameObjectHitcollider.RemoveAt(0);
                
                CurrentPos = Vector3.zero;
                isAttack = false;
            }
        }
        if (listgameObjectHitcollider.Count == 0)
        {
            CurrentPos = Vector3.zero;
            isAttack = false;
        }


        }
   
}
