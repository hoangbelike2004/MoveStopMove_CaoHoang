using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{


    public static UnityAction<int> updateTextScoreAction;
    public int score;
    

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int price)
    {
        score = score - price;
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
        BotManager.WinGameEvent += GameWin;
    }

    private void OnDisable()
    {
        BotManager.WinGameEvent -= GameWin;
    }

}
