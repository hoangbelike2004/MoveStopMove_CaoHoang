using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] List<Bot> bots;
    [SerializeField] int valueBotMax,bottmp = 0;
    [SerializeField] float secondsBotHs;
    [SerializeField] Bot botPrefab;
    [SerializeField] float locationappearsX, locationappearsZ;// vi tri xuat hien cua bot
    bool isWin;
    public static UnityAction WinGameEvent;
     IEnumerator Start()
    {
        StartCoroutine(InstantiateBot());
        bottmp = valueBotMax;
        while (true)
        {
            for(int i = 0; i < bots.Count; i++)
            {
                if(bots[i].gameObject.activeSelf == false && valueBotMax >=0)
                {
                    yield return new WaitForSeconds(secondsBotHs);
                    float posx = Random.Range(-locationappearsX, locationappearsX);
                    float posz = Random.Range(-locationappearsZ, locationappearsZ);
                    Vector3 newpos = new Vector3(posx, 0, posz);
                    if((newpos.x > 7 || newpos.x < -7)&& (newpos.z > 2 || newpos.z < -12))
                    {
                        bots[i].gameObject.SetActive(true);
                        bots[i].transform.position = newpos;
                        bots[i].OnInit();
                    }
                    
                }  
            }
            isWin = true;
            foreach(Bot bot in bots)
            {
                if(bot.gameObject.activeSelf == true)
                {
                    isWin = false;
                    break;
                }
            }
            if (isWin)
            {
                GameController.Instance.GameWin();
                isWin = false;
            }
            yield return null;
        }
    }
    void OnInit()
    {
        valueBotMax = bottmp;
        isWin = false;
        foreach (Bot bot in bots)
        {
            if (bot.gameObject.activeSelf == true)
            {
                bot.gameObject.SetActive(false);

            }
        }
        foreach (Bot bot in bots)
        {
            if (bot.gameObject.activeSelf == false)
            {
                
                float posx = Random.Range(-locationappearsX, locationappearsX);
                float posz = Random.Range(-locationappearsZ, locationappearsZ);
                Vector3 newpos = new Vector3(posx, 0, posz);
                if ((newpos.x > 7 || newpos.x < -7) && (newpos.z > 2 || newpos.z < -12))
                {
                    bot.gameObject.SetActive(true);
                    bot.transform.position = newpos;
                }
                //bot.OnInit();
                //bot.SetIsDie(true);

            }
        }
    }

    IEnumerator InstantiateBot()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            float posx = Random.Range(-locationappearsX, locationappearsX);
            float posz = Random.Range(-locationappearsZ, locationappearsZ);
            Vector3 newpos = new Vector3(posx, 0, posz);
            Bot bot = Instantiate(botPrefab, newpos, Quaternion.identity);
            bots[i] = bot;

        }
        yield return null;
    }
    void updatebot()
    {
        valueBotMax--;
    }
    private void OnEnable()
    {
        Bot.testaction += updatebot;
        GameController.OnInitAllAction += OnInit;
    }
    private void OnDisable()
    {
        Bot.testaction -= updatebot;
        GameController.OnInitAllAction -= OnInit;
    }

}
