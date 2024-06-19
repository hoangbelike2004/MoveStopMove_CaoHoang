using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasGameLose : UiCanvas
{
    [SerializeField] Button reloadbtn;
    [SerializeField] Button homebtn;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _top;
    public static UnityAction OpenPlayGameWhenLose;


    private void Start()
    {
        homebtn.onClick.AddListener(ClickHomeBtn);
        reloadbtn.onClick.AddListener(ClickReloadBtn);

    }

    void ClickHomeBtn()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();//dua cam ve vi tri ban dau
        UiManager.Instance.CloseUI<CanvasMatch>(0f);
        UiManager.Instance.CloseUI<CanvasGameLose>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        OpenPlayGameWhenLose.Invoke();//mo uiGamePlay khi lose
    }
    
    void ClickReloadBtn()
    {

    }
}
