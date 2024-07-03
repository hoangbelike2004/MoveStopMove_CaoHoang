using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartrolState : IState
{
    Vector3 next_pos;
    int  numberoftimesmoved,numberofmoves;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(Contains.RUN);
        next_pos = bot.transform.position;
        
        numberoftimesmoved = 0;
        numberofmoves = Random.Range(1, 2);
    }

    public void OnExcute(Bot bot)
    {
        bot.SetCurrentPos();
        if (numberoftimesmoved <= numberofmoves)
        {
            if (Vector3.Distance(next_pos, bot.transform.position) < .5f)
            {
                numberoftimesmoved++;
                next_pos = RandomTarget.Instance.R_point_Get(bot.transform.position, bot.radius);
                bot.Move(next_pos);

            }
        }
        else
        {
            bot.SetTarget();
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
