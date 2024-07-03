using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Bot : Character
{
    [SerializeField] GameObject _selectAttackOfPlayer;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Material[] materials;
    [SerializeField] SkinnedMeshRenderer _renderer;
    [SerializeField] Target _targetArrowIndication;

    public float radius = 20;
    public static UnityAction testaction;
    public static UnityAction valueBotAlive;

    private IState currentState;
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
    public void SetColorIndicator(Color c)
    {
        _targetArrowIndication.SetTagetColor(c);
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
        int valuemt = Random.Range(0, materials.Length);
        _renderer.material = materials[valuemt];
        _agent.enabled = true;
        testaction.Invoke();
        base.OnInit();
        _agent.speed = speed;
        score = Random.Range(0, 5);
        CurrentPos = Vector3.zero;
        _text.text = score.ToString();
        isSelectEnemy = false;
        int tmp = Random.Range(0,weaponData1.weapons.Count);
        ChangeWeapon((WeaponType)tmp);
        ChangeState(new IdleState());
        int indexhat = Random.Range(0, hatData.hats.Count);
        ChangeHat((HatType)indexhat);
        int indexPant = Random.Range(0,pantData.pants.Count);
        ChangePant((PantType)indexPant);
    }

    public override void ChangeAnim(string nameAnim)
    {
     
            anim.SetTrigger(nameAnim);

    }
    //public void DeActive
    public override void NotPlayGame()
    {
        base.NotPlayGame();
        isDie = false;
        isPlay = false;
        ChangeAnim(Contains.IDLE);
    }
    protected override void PlayGame()
    {
        base.PlayGame();
        ChangeState(new PartrolState());
    }
    public override void Die()
    {
        base.Die();
        _selectAttackOfPlayer.SetActive(false);
        _agent.enabled = false;
        valueBotAlive.Invoke();
       
    }

    public void ChangeAnimBot()
    {   
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
    public void SetCurrentPos()
    {
        CurrentPos = Vector3.zero;
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
