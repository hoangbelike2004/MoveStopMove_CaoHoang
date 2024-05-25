using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public GameObject _selectAttackOfPlayer;
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
        isDie = false;
        ChangeState(new IdleState());
    }
    public void ChangeAnimBot()
    {
        Vector3 veloc = _agent.velocity;
        bool isMoving = veloc.magnitude != 0;
        if(isMoving)
        {
            ChangeAnim(RUN);
        }else if (!isMoving)
        {
            ChangeAnim(IDLE);
        }
        
        if (!isAttack)
        {
            _weaponFake.SetActive(true);
        }
    }
    protected override void CheckSight()
    {
        base.CheckSight();
    }

}
