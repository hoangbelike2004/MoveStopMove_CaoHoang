using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{
    float time,timer;
    public void OnEnter(Bot bot)
    {
        time = 0;
        timer = Random.Range(2, 4);
        bot.ChangeAnim(Contains.IDLE);
    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        if(time >= timer)
        {
            bot.ChangeState(new PartrolState());
        }
        if(bot.GetCurrentPos() != Vector3.zero)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
