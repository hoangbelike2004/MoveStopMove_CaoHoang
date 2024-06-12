using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameLose : UiCanvas
{
    [SerializeField] Button reloadbtn;
    [SerializeField] Button homebtn;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _top;


    private void Start()
    {
        homebtn.onClick.AddListener(ClickHomeBtn);
        reloadbtn.onClick.AddListener(ClickReloadBtn);

    }

    void ClickHomeBtn()
    {
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();
        UiManager.Instance.CloseUI<CanvasSetting>(0f);
        UiManager.Instance.CloseUI<CanvasGameLose>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
    }
    
    void ClickReloadBtn()
    {

    }
}
