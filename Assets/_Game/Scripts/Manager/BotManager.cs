using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] List<Bot> bots;
    [SerializeField] int valueBotMax,valueBotAlive,bottmp = 0,nextLevel;
    [SerializeField] float secondsBotHs;
    [SerializeField] Bot botPrefab;
    [SerializeField] float locationappearsX, locationappearsZ;// vi tri xuat hien cua bot
    bool isWin;
    public static UnityAction WinGameEvent;
    public static UnityAction<int> CheckBotEvent;
    IEnumerator Start()
    {
        if (PlayerPrefs.HasKey(Contains.DATA_LEVELBOT))
        {
            bottmp = DataManager.Instance.GetDataBot(Contains.DATA_LEVELBOT);
            valueBotAlive = DataManager.Instance.GetDataBot(Contains.DATA_LEVELBOT);
        }
        else
        {
            bottmp = valueBotMax;
            valueBotAlive = valueBotMax;
        }
        
        StartCoroutine(InstantiateBot());
        while (true)
        {
            for(int i = 0; i < bots.Count; i++)
            {
                if(bots[i].gameObject.activeSelf == false && valueBotMax > 0)
                {
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
            yield return null;
        }
    }
    
    public int GetValueBot()
    {
        return valueBotAlive;
    }
    void OnInit()
    {
        valueBotAlive = bottmp;
        valueBotMax = bottmp;
        CheckBotEvent.Invoke(valueBotMax);
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
                
                    bot.gameObject.SetActive(true);
                    bot.transform.position = newpos;
                    bot.OnInit();
                    bot.NotPlayGame();
                

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
    private void UpdateBotAlive()
    {
        valueBotAlive--;
        if (UiManager.Instance.IsOpen<CanvasMatch>())
        {
            CheckBotEvent.Invoke(valueBotAlive);
        }
        if(valueBotAlive == 0)
        {
            bottmp += nextLevel;
            DataManager.Instance.SaveDataBot(bottmp);
            AudioManager.Instance.PlaySound(AudioManager.Instance.acWin, 1);
            WinGameEvent.Invoke();
            isWin = false;
        }
    }
    private void OnEnable()
    {
        Bot.testaction += updatebot;
        Bot.valueBotAlive += UpdateBotAlive;
        GameController.OnInitAllAction += OnInit;
    }
    private void OnDisable()
    {
        Bot.testaction -= updatebot;
        Bot.valueBotAlive -= UpdateBotAlive;
        GameController.OnInitAllAction -= OnInit;
    }

}
