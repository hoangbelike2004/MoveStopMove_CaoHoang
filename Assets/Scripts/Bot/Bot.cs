using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] GameObject _selectAttackOfPlayer;
    public float radius = 20;
    [SerializeField] NavMeshAgent _agent;
    private IState currentState;
    


    private void Update()
    {
        if(isDie)
        {
            return;
        }
        if(currentState != null)
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
    protected override void OnInit()
    {
        _agent.speed = speed;
        base.OnInit();
        score = Random.Range(0, 15);
        isDie = false;
        ChangeState(new IdleState());
        _text.text = score.ToString();
        CheckScoreForUpSize(score);
        ChangeWeapon(weaponData1.GetWeapon(WeaponType.lollipop));
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
            _weaponFake.SetActive(true);
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

}
