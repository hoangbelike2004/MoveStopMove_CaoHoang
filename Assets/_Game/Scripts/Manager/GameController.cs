using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameController : Singleton<GameController>
{


    public static UnityAction<int> updateTextScoreAction;
    public static UnityAction ReLoadHome;
    public static UnityAction OnInitAllAction;
    public static UnityAction QuitGameEvent;
    public int score;
    [SerializeField] Player Player;
    [SerializeField] private float timeActiveLoseUI;
    bool isWin, isLose;

    private void Start()
    {
        //if (PlayerPrefs.HasKey(Contains.DATA_SKIN))
        //{
        //    Debug.Log("Ton tai dataskin");
        //    PlayerPrefs.DeleteKey(Contains.DATA_SKIN);

        //}
        //if (PlayerPrefs.HasKey(Contains.DATA_PLAYER))
        //{
        //    Debug.Log("Ton tai dataPlayer");
        //    PlayerPrefs.DeleteKey(Contains.DATA_PLAYER);
        //}
        Application.quitting += quit;
        isWin = true;
        isLose = true;
    }
    void quit()
    {

         DataManager.Instance.SaveDataPlayer(Player.GetHatTypePlayer()
                , Player.GetPantTypePlayer(), Player.GetShieldTypePlayer(),
                  Player.GetWeaponTypePlayer(), GetScore());
        
    }
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int price)
    {
        score = score - price;
    }
    public void SetScoreSaved(int score)
    {
        this.score = score;
    }
    public void Home()
    {
        ReLoadHome.Invoke();
    }
    public void OnInitAll()
    {
        isWin = true;
        isLose = true;
        if (Player.gameObject.activeSelf == false)
        {
            Player.transform.gameObject.SetActive(true);
        }
        OnInitAllAction.Invoke();
        
    }

    public void UpdateScore()
    {
        updateTextScoreAction.Invoke(GetScore());
    }


    public void GameLose()
    {
        if (isLose)
        {
            isWin = false;
            Invoke(nameof(ActiveUILose), timeActiveLoseUI);
        }
        
    }
    void ActiveUILose()
    {
        UiManager.Instance.OpenUI<CanvasGameLose>();
    }
    void ActiveUIWin()
    {
        UiManager.Instance.OpenUI<CanvasGameWin>();
    }

    public void GameWin()
    {
        if (isWin)
        {
            isLose = false;
            Invoke(nameof(ActiveUIWin), timeActiveLoseUI);
        }
        
    }
    private void OnEnable()
    {
        Player.LoseEvent += GameLose;
        BotManager.WinGameEvent += GameWin;
    }
    private void OnDisable()
    {
        Player.LoseEvent -= GameLose;
        BotManager.WinGameEvent -= GameWin;
    }

}
