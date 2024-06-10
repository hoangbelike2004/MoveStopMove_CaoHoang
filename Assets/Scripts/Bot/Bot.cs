using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Bot : Character
{
    [SerializeField] GameObject _selectAttackOfPlayer;
    public float radius = 20;
    [SerializeField] NavMeshAgent _agent;
    private IState currentState;
    public static UnityAction testaction;


    private void Update()
    {

        if(isDie)
        {
            currentState = null;
            return;
            
        }
         else if(currentState != null && isPlay)
        {
            currentState.OnExcute(this);
        }
        ChangeAnimBot();
        CheckSight();
    }
    public void Move(Vector3 newPos)
    {
        _agent.SetDestination(newPos);
    }
    public void SetTarget()
    {
        _agent.ResetPath();
    }
    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public override void OnInit()
    {
        testaction.Invoke();
        base.OnInit();
        _agent.speed = speed;
        _agent.enabled = true;
        score = Random.Range(0, 5);
        isDie = false;
        
        CurrentPos = Vector3.zero;
        _text.text = score.ToString();
        CheckScoreForUpSize(score);



        int tmp = Random.Range(0,weaponData1.weapons.Count);
        ChangeWeapon((WeaponType)tmp);
        _selectAttackOfPlayer.gameObject.SetActive(false);
        ChangeState(new PartrolState());
        int indexhat = Random.Range(0, hatData.hats.Count);
        ChangeHat((HatType)indexhat);
        int indexPant = Random.Range(0,pantData.pants.Count);
        ChangePant((PantType)indexPant);
    }
    public override void Die()
    {
        base.Die();
        
        _agent.enabled = false;
        isPlay = true;
       
    }
    public void ChangeAnimBot()
    {
        Vector3 veloc = _agent.velocity;
        bool isMoving = veloc.magnitude != 0;
        if(isMoving)
        {
            ChangeAnim(RUN);
        }else if (!isMoving&&!isAttack)
        {
            ChangeAnim(IDLE);
        }
        
        if (!isAttack)
        {
            _weaponFaketf.gameObject.SetActive(true);
        }
        if (isSelectEnemy)
        {
            _selectAttackOfPlayer.SetActive(true);
            
        }
        else
        {

            _selectAttackOfPlayer.SetActive(false); 
        }
    }
    public void IsSelect(bool isselect)
    {
        this.isSelectEnemy = isselect;
    }
    public Vector3 GetCurrentPos()
    {
        return CurrentPos;
    }
    protected override void CheckSight()
    {
        base.CheckSight();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
