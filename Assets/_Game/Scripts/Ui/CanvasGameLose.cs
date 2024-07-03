using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasGameLose : UiCanvas
{
    [SerializeField] Button buttonRevival;
    [SerializeField] Button homebtn;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _top;
    [SerializeField] TextMeshProUGUI txtTimeAutoDeactive;
    public static UnityAction OpenPlayGameWhenLose;
    public static UnityAction RevivalEvent;


    private void Start()
    {
        homebtn.onClick.AddListener(ClickHomeBtn);
        buttonRevival.onClick.AddListener(ClickRevivalBtn);
        
    }
    public void UpdateScoreWhenLose(int a)
    {
        _score.text = a.ToString();
    }
    void ClickHomeBtn()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();//dua cam ve vi tri ban dau
        UiManager.Instance.CloseUI<CanvasMatch>(0f);
        UiManager.Instance.CloseUI<CanvasGameLose>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        GameController.Instance.UpdateScore();
        OpenPlayGameWhenLose.Invoke();//mo uiGamePlay khi lose
    }
    
    void ClickRevivalBtn()
    {
        UiManager.Instance.CloseUI<CanvasGameLose>(0f);
        GameController.Instance.RevivalPlayer();
        RevivalEvent.Invoke();
    }

    IEnumerator AutomaticDeactiveCanvasLose()
    {
        int a = 5;
        txtTimeAutoDeactive.text = a.ToString();
        WaitForSeconds delay = new WaitForSeconds(1);
        yield return delay;
        a--;
        txtTimeAutoDeactive.text = a.ToString();
        yield return delay;
        a--;
        txtTimeAutoDeactive.text = a.ToString();
        yield return delay;
        a--;
        txtTimeAutoDeactive.text = a.ToString();
        yield return delay;
        a--;
        txtTimeAutoDeactive.text = a.ToString();
        yield return delay;
        a--;
        txtTimeAutoDeactive.text = a.ToString();
        yield return delay;
        ClickHomeBtn();
    }

    void functionTmp()
    {
        StartCoroutine(AutomaticDeactiveCanvasLose());
    }

    private void OnEnable()
    {
        GameController.CallTimeUIWhenLose += functionTmp;
        GameController.UpdateScoreAndSetScoreWhenFinish += UpdateScoreWhenLose;
    }

    private void OnDisable()
    {
        GameController.CallTimeUIWhenLose -= functionTmp;
        GameController.UpdateScoreAndSetScoreWhenFinish -= UpdateScoreWhenLose;
    }
}
