using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMatch : UiCanvas
{
    [SerializeField] Button settingButton;
    [SerializeField] TextMeshProUGUI Alivestxt;
    private void Start()
    {
        settingButton.onClick.AddListener(OpenSetting);
        Alivestxt.text = Contains.nameAlive + BotManager.Instance.GetValueBot().ToString();
    }


    void OpenSetting()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        UiManager.Instance.OpenUI<CanvasSetting>();
    }

    void UpdateAlive(int alive)
    {
        Alivestxt.text = Contains.nameAlive + alive.ToString();
    }

    private void OnEnable()
    {
        BotManager.CheckBotEvent += UpdateAlive;
    }

    private void OnDisable()
    {
        BotManager.CheckBotEvent += UpdateAlive;
    }
}
