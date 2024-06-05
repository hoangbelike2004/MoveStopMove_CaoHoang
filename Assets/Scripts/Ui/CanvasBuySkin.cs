using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

[Serializable]
public enum TypeScroll { scrollHat,scrollPant, scrollShield, scrollSuit };

public class CanvasBuySkin : UiCanvas
{
    [Header("Button")]
    [SerializeField] Button hatButton;
    [SerializeField] Button pantButton;
    [SerializeField] Button shieldButton;
    [SerializeField] Button SuitButton;
    [SerializeField] Button ExitBuySkin;
    [SerializeField] Button selectSkinButton;
    [SerializeField] Button buySkinButton;

    [Header("IconButton")]
    [SerializeField] Image[] IconButton = new Image[4];


    [Header("Panel UI")]
    [SerializeField] RectTransform RectTransformHat;
    [SerializeField] RectTransform rectTransformPant;
    [SerializeField] RectTransform rectTransformShield;
    [SerializeField] RectTransform rectTransformSuit;

    [Header("Data")]
    [SerializeField] HatData hatData;
    [SerializeField] PantData pantData;
    [SerializeField] ShieldData shieldData;
    [SerializeField] SuitData suitData;

    [Header("ItemUI")]
    public ItemUIPant itemUiPantprefab;
    [SerializeField] ItemUIHat itemUiHatprefab;
    [SerializeField] ItemUiSuit itemUiSuitprefab;
    [SerializeField] ItemUIShield itemUIShieldprefab;

    [Header("ScrollObject")]
    [SerializeField] GameObject[] scrollObjects = new GameObject[4];

    Color colorCurrent;

    //Aciton
    public static UnityAction actionChangeExitSkinCameraFlow;
    public static UnityAction actionSelectSkin;
    public static UnityAction actionNotSelectSkin;
    private void Awake()
    {
        colorCurrent = IconButton[0].color;
        InstantiateHat();
    }
    private void Start()
    {
        hatButton.onClick.AddListener(InstantiateHat);
        pantButton.onClick.AddListener(InstantiatePant);
        shieldButton.onClick.AddListener(InstantiateShield);
        SuitButton.onClick.AddListener(InstantiateSuit);
        ExitBuySkin.onClick.AddListener(Exit);
        selectSkinButton.onClick.AddListener(SelectSkin);
    }
    void InstantiatePant()
    {
        ActiveScrollObjectUsed(TypeScroll.scrollPant);
        ChangeColorObjectUsed(TypeScroll.scrollPant);
        for (int i = 0; i < pantData.pants.Count; i++)
        {
            if (rectTransformPant.childCount < pantData.pants.Count)
            {
                ItemUIPant item = Instantiate(itemUiPantprefab, rectTransformPant);
                item.SetSprite(pantData.GetSpritePant((PantType)i));
                item.transform.SetParent(rectTransformPant);
                item.SetDataPant(pantData.pants[i]);
            }
            
        }
    }
    void InstantiateHat()
    {
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        for (int i = 0; i < hatData.hats.Count; i++)
        {
            if(RectTransformHat.childCount < hatData.hats.Count)
            {
                ItemUIHat item = Instantiate(itemUiHatprefab, RectTransformHat);
                item.SetIconHat(hatData.GetIcon((HatType)i));
                item.transform.SetParent(RectTransformHat);
                item.SetDataHat(hatData.hats[i]);
            }
            
        }
    }

    void InstantiateShield()
    {
        ActiveScrollObjectUsed(TypeScroll.scrollShield);
        ChangeColorObjectUsed(TypeScroll.scrollShield);
        for (int i = 0; i < shieldData.shields.Count; i++)
        {
            if(rectTransformShield.childCount < shieldData.shields.Count)
            {
                ItemUIShield item = Instantiate(itemUIShieldprefab, rectTransformShield);
                item.SetIconShield(shieldData.GetIconShield((ShieldType)i));
                item.transform.SetParent(rectTransformShield);
                item.SetDataShield(shieldData.shields[i]);
            }
           
        }
    }

    void InstantiateSuit()
    {
        ActiveScrollObjectUsed(TypeScroll.scrollSuit);
        ChangeColorObjectUsed(TypeScroll.scrollSuit);
    }


    public void ActiveScrollObjectUsed(TypeScroll type)
    {
        for(int i = 0; i < IconButton.Length;i++)
        {
            if (i == (int)type)
            {
                scrollObjects[i].SetActive(true);
            }
            else
            {
                scrollObjects[i].SetActive(false);
            }
        }
    }
    public void ChangeColorObjectUsed(TypeScroll type)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == (int)type)
            {
                IconButton[i].color = Color.white;
            }
            else
            {
                IconButton[i].color = colorCurrent;
            }
        }
    }
    public void Exit()//thoat khỏi shop
    {
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        UiManager.Instance.CloseUI<CanvasBuySkin>(0f);
        actionChangeExitSkinCameraFlow.Invoke();
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        actionNotSelectSkin.Invoke();
    }
    public void SelectSkin()//chon skin
    {
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        UiManager.Instance.CloseUI<CanvasBuySkin>(0f);
        actionChangeExitSkinCameraFlow.Invoke();
        UiManager.Instance.OpenUI<CanvasGamePlay>();
        actionSelectSkin.Invoke();
    }
}
