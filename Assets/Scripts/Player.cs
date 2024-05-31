using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick _variableJoyStick;
    bool isMove;
    private Vector3 moveDirection;
    void Update()
    {
        if (!isDie&&isPlay)
        {
            
            ChangeState();



            CheckSight();
            if (time >= timer)
            {
                isAttack = true;

            }
            time += Time.deltaTime;
        }
        

        //Debug.Log(enemyCurrentPos);
    }
    private void MovePlayer()
    {
        
        
        transform.position += moveDirection * speed * Time.deltaTime;
        
    }
    private void ChangeState()
    {
        if (_variableJoyStick.Horizontal != 0 || _variableJoyStick.Vertical != 0)
        {
            moveDirection = new Vector3(_variableJoyStick.Horizontal, 0, _variableJoyStick.Vertical);
            moveDirection.Normalize();
            transform.forward = moveDirection;
            if (CheckGround())
            {
                MovePlayer();
            }
            
            ChangeAnim(RUN);
            time = timer;
            //Debug.Log(1);
            _weaponFaketf.gameObject.SetActive(true);

        }
        else if (_variableJoyStick.Horizontal == 0 && _variableJoyStick.Vertical == 0)
        {
            if (!isAttack ||CurrentPos == Vector3.zero)
            {
                ChangeAnim(IDLE);
                _weaponFaketf.gameObject.SetActive(true);
                time += Time.deltaTime;
            }

            if (CurrentPos != Vector3.zero && isAttack&& time >= timer)
            {


                if (hitcollider.Length != 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(CurrentPos - transform.position);
                    targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);//cho xoay moi truc y ve phia enemy
                                                                                          // Xoay player về phía quái
                    transform.rotation = targetRotation;

                }

                Attack();
            }
        }

    }
    protected override void OnInit()
    {
        base.OnInit();
        //ChangeWeapon(weaponData1.GetWeapon(WeaponType.hammer));
        ChangeAnim(DANCE);
        score = 0;
        _text.text = score.ToString();
    }
    private bool CheckGround()
    {
        if(!Physics.Raycast(transform.position + Vector3.up * 2f, transform.forward * 1.5f + Vector3.down * 4f,planeLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CanvasBuyWeapon.selectWeaponAction += ChangeWeapon;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        CanvasBuyWeapon.selectWeaponAction -= ChangeWeapon;
    }


}

