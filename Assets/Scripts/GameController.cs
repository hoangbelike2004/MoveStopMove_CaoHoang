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

    private void Start()
    {
        Application.quitting += quit;
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
        if(Player.gameObject.activeSelf == false)
        {
            Debug.Log("Run");
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
        UiManager.Instance.OpenUI<CanvasGameLose>();
    }
    
    public void GameWin()
    {
        UiManager.Instance.OpenUI<CanvasGameWin>();
    }


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

}
