using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasSetting : UiCanvas
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button continueButton;
    public static UnityAction OpenGoBackHome;
    private void Start()
    {
        homeButton.onClick.AddListener(GoBackHome);
        continueButton.onClick.AddListener(Continue);
    }

    void Continue()
    {
        UiManager.Instance.CloseUI<CanvasSetting>(0f);
    }

    void GoBackHome()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        GameController.Instance.OnInitAll();
        GameController.Instance.Home();
        UiManager.Instance.CloseUI<CanvasSetting>(0f);
        UiManager.Instance.CloseUI<CanvasMatch>(0f);
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        OpenGoBackHome.Invoke();
    }
}
