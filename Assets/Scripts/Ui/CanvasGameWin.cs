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
        UiManager.Instance.CloseUI<CanvasGameLose>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
