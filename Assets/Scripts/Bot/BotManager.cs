using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotManager : MonoBehaviour
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
        while (true)
        {
            for(int i = 0; i < bots.Count; i++)
            {
                if(bots[i].gameObject.activeSelf == false && bottmp < valueBotMax)
                {
                    yield return new WaitForSeconds(secondsBotHs);
                    float posx = Random.Range(-locationappearsX, locationappearsX);
                    float posz = Random.Range(-locationappearsZ, locationappearsZ);
                    Vector3 newpos = new Vector3(posx, 0, posz);
                    bots[i].gameObject.SetActive(true);
                    bots[i].transform.position = newpos;
                    bots[i].OnInit();
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
                WinGameEvent.Invoke();
            }
            yield return null;
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
        bottmp++;
    }
    private void OnEnable()
    {
        Bot.testaction += updatebot;
    }
    private void OnDisable()
    {
        Bot.testaction -= updatebot;
    }

}
