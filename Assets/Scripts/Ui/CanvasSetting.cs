using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UiCanvas
{
    [SerializeField] Button settingButton;
    [SerializeField] TextMeshProUGUI Alives;
    private void Start()
    {
        settingButton.onClick.AddListener(OpenSetting);
    }


    void OpenSetting()
    {
        Debug.Log("OpenSetting");
    }
}
