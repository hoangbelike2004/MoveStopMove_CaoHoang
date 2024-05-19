using System.Collections;
using System.Collections.Generic;
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
        if(_variableJoyStick.Horizontal !=0 || _variableJoyStick.Vertical != 0)
        {
            MovePlayer();
            ChangeAnim(RUN);
            Debug.Log(1);
        }else if(_variableJoyStick.Horizontal == 0 && _variableJoyStick.Vertical == 0)
        {
            Debug.Log(2);
            ChangeAnim(IDLE);
        }
    }
}
