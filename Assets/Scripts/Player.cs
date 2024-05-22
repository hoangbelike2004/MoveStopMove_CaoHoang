using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private VariableJoystick _variableJoyStick;

    private Vector3 moveDirection;
    void Update()
    {
        ChangeState();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            UpSize();
        }


        
            CheckSight();
        if (time >= timer)
        {
            isAttack = true;
            
        }
        time += Time.deltaTime;

        //Debug.Log(enemyCurrentPos);
    }
    private void MovePlayer()
    {
        moveDirection = new Vector3(_variableJoyStick.Horizontal, 0, _variableJoyStick.Vertical);
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
        transform.forward = moveDirection;
    }
    private void ChangeState()
    {
        if (_variableJoyStick.Horizontal != 0 || _variableJoyStick.Vertical != 0)
        {
            MovePlayer();
            ChangeAnim(RUN);
            time = timer;
            Debug.Log(1);
            _weaponFake.SetActive(true);

        }
        else if (_variableJoyStick.Horizontal == 0 && _variableJoyStick.Vertical == 0)
        {
            if (!isAttack ||CurrentPos == Vector3.zero)
            {
                ChangeAnim(IDLE);
                _weaponFake.SetActive(true);
            }

            if (CurrentPos != Vector3.zero && isAttack)
            {
                


                if (listgameObjectHitcollider.Count != 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(listgameObjectHitcollider[0].transform.position - transform.position);
                    targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);//cho xoay moi truc y ve phia enemy
                                                                                          // Xoay player về phía quái
                    transform.rotation = targetRotation;

                }

                Attack();
            }
        }

    }

}

