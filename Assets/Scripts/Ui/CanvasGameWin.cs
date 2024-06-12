using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameWin : UiCanvas
{
    [SerializeField] Button homebtn;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _top;



    private void Start()
    {
        homebtn.onClick.AddListener(ClickHomeBtn);

    }

    void ClickHomeBtn()
    {
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();
        UiManager.Instance.CloseUI<CanvasGameWin>(0f);
        UiManager.Instance.CloseUI<CanvasSetting>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        
    }
}
