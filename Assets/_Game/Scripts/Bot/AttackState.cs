using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time, timer;
    public void OnEnter(Bot bot)
    {
        time = 0;
        timer = Random.Range(1, 3);// TODO: tính xem bao lâu thì tấn công
        Quaternion targetRotation = Quaternion.LookRotation(bot.GetCurrentPos() - bot.transform.position);
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);//cho xoay moi truc y ve phia enemy
                                                                              // Xoay player về phía quái
        bot.transform.rotation = targetRotation;
        bot.Attack();


    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        bot.SetCurrentPos();
        if (!bot.isAttack && time >= timer)
        {
            bot.ChangeState(new PartrolState());
        }

    }
        

    public void OnExit(Bot bot)
    {
        
    }
}
