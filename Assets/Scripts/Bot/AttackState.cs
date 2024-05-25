using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float time, timer;
    public void OnEnter(Bot bot)
    {
        time = 2;
        timer = Random.Range(1, 4);
    }

    public void OnExcute(Bot bot)
    {
        time += Time.deltaTime;
        if(time >= timer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(bot.listgameObjectHitcollider[0].transform.position - bot.transform.position);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);//cho xoay moi truc y ve phia enemy
                                                                                  // Xoay player về phía quái
            bot.transform.rotation = targetRotation;
            bot.Attack();
            time = 0;
        }
        if(bot.listgameObjectHitcollider.Count == 0)
        {
            bot.ChangeState(new PartrolState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
