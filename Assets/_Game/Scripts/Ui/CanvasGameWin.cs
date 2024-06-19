using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasGameWin : UiCanvas
{
    [SerializeField] Button homebtn;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _top;
    public static UnityAction OpenPlayGameWhenWin;


    private void Start()
    {
        homebtn.onClick.AddListener(ClickHomeBtn);

    }

    void ClickHomeBtn()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();
        UiManager.Instance.CloseUI<CanvasGameWin>(0f);
        UiManager.Instance.CloseUI<CanvasMatch>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        OpenPlayGameWhenWin.Invoke();
        
    }
}
