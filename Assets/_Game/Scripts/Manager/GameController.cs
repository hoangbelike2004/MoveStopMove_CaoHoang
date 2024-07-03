using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameController : Singleton<GameController>
{


    public static UnityAction<int> updateTextScoreAction;
    public static UnityAction ReLoadHome;
    public static UnityAction CallTimeUIWhenLose;
    public static UnityAction OnInitAllAction;
    public static UnityAction QuitGameEvent;
    public static UnityAction<int> UpdateScoreAndSetScoreWhenFinish;
    public int score;
    [SerializeField] Player Player;
    [SerializeField] private float timeActiveLoseUI;
    bool isWin, isLose;

    private void Start()
    {
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
    public int Scoreearned()
    {
        score += Player.GetScore();// update diem khi xong tran
        return Player.GetScore();//tra ve so diem kiem duoc
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
    public void RevivalPlayer()
    {
        isWin = true;
        isLose = true;

        Player.transform.gameObject.SetActive(true);

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
        Scoreearned();
        UiManager.Instance.OpenUI<CanvasGameLose>();
        CallTimeUIWhenLose.Invoke();
        int a = Scoreearned();
        UpdateScoreAndSetScoreWhenFinish.Invoke(a);
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
