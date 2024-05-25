using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartrolState : IState
{
    Vector3 next_pos;
    float time, timer;
    public void OnEnter(Bot bot)
    {
        next_pos = bot.transform.position;
        time = 0;
        timer = Random.Range(3, 5);
    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        if(time <= timer)
        {
            if (Vector3.Distance(next_pos, bot.transform.position) < .5f)
            {
                next_pos = RandomTarget.Instance.R_point_Get(bot.transform.position, bot.radius);
                bot.Move(next_pos);

            }
        }
        else
        {
            bot.SetTarget();
            bot.ChangeState(new IdleState());

   
        }

        if(bot.listgameObjectHitcollider.Count > 0)
        {
            bot.SetTarget();
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
