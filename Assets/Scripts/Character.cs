using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float speed,valueSize;
    protected bool isDie;
    protected const string IDLE = "Idle", ATTACK = "Attack",DIE = "Die",RUN = "Run", DANCE_WIN = "DanceWin";
    private string currentAnim;
    [SerializeField] private Animator anim;

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
        valueSize = 0.15f;
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
    }
    protected virtual void Die()
    {
        ChangeAnim(DIE);
    }
    protected virtual void Win()
    {
        ChangeAnim(DANCE_WIN);
    }
}
