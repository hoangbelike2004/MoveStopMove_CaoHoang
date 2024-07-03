using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BotManager : Singleton<BotManager>
{
    [SerializeField] List<Bot> bots;
    [SerializeField] List<Vector3> PosBots;
    [SerializeField] int valueBotMax,valueBotAlive,bottmp = 0,nextLevel;
    [SerializeField] float secondsBotHs;
    [SerializeField] Bot botPrefab;
    [SerializeField] float locationappearsX, locationappearsZ;// vi tri xuat hien cua bot
    [SerializeField] Color[] colors;
 
    public static UnityAction WinGameEvent;
    public static UnityAction<int> CheckBotEvent;
    bool isWin;

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
                if(bots[i].gameObject.activeSelf == false && valueBotMax > 0)//hoi sinh bot
                {
                    
                        bots[i].gameObject.SetActive(true);
                        int randomPoshs = Random.Range(0, bots.Count);
                        bots[i].transform.position = PosBots[randomPoshs];
                        bots[i].OnInit();
                    
                    
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
        //foreach (Bot bot in bots)
        //{
        //    if (bot.gameObject.activeSelf == true)
        //    {
        //        bot.gameObject.SetActive(false);

        //    }
        //}
        for (int i = 0; i < bots.Count; i++)
        {
            if (bots[i].gameObject.activeSelf == true)//deactive bot
            {
                bots[i].gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < bots.Count; i++)
        {
            if (bots[i].gameObject.activeSelf == false)//hoi sinh bot
            {
                bots[i].gameObject.SetActive(true);
                bots[i].transform.position = PosBots[i];
                bots[i].OnInit();
                bots[i].NotPlayGame();


            }
        }
        //foreach (Bot bot in bots)
        //{
        //    if (bot.gameObject.activeSelf == false)
        //    {
                
        //        float posx = Random.Range(-locationappearsX, locationappearsX);
        //        float posz = Random.Range(-locationappearsZ, locationappearsZ);
        //        Vector3 newpos = new Vector3(posx, 0, posz);
                
        //            bot.gameObject.SetActive(true);
        //            bot.transform.position = newpos;
        //            bot.OnInit();
        //            bot.NotPlayGame();
                

        //    }
        //}
    }

    IEnumerator InstantiateBot()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            Bot bot = Instantiate(botPrefab, PosBots[i], Quaternion.identity);
            
            bots[i] = bot;
            bots[i].SetColorIndicator(colors[i]);


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
