using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Text")]
    [SerializeField] TextMeshProUGUI _textScore;

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

    public List<int> listHat,listPant,listShield;

    //Aciton
    public static UnityAction actionChangeExitSkinCameraFlow;
    public static UnityAction actionSelectSkin;
    public static UnityAction actionNotSelectSkin;
    public static UnityAction actionOpenAnimToSkinfromPlay;
    public static UnityAction<HatType> actionSelectHatStart;
    public static UnityAction<ShieldType> actionSelectShieldStart;
    public static UnityAction<PantType> actionSelectPantStart;
    public static UnityAction<SuitType> actionSelectSuitStart;


    public HatItem _hatitem;
    public ShieldItem _shieldItem;
    public PantItem _pantitem;
    public SuitdItem _suitditem;

 
    private void Awake()
    {
        colorCurrent = IconButton[0].color;
        
    }
    private void Start()
    {
        listHat = new List<int>();
        listPant = new List<int>();
        listShield = new List<int>();

        hatButton.onClick.AddListener(InstantiateHat);
        pantButton.onClick.AddListener(InstantiatePant);
        shieldButton.onClick.AddListener(InstantiateShield);
        SuitButton.onClick.AddListener(InstantiateSuit);
        ExitBuySkin.onClick.AddListener(Exit);
        selectSkinButton.onClick.AddListener(SelectSkin);
        buySkinButton.onClick.AddListener(BuySkin);
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
                if (i == 0)
                {
                    SetBuySkin(item.GetStateTypePant());
                }

            } 
        }
        for (int i = 0; i < pantData.pants.Count; i++)
        {
            if (i == 0)
            {
                rectTransformPant.GetChild(i).GetComponent<ItemUIPant>().ActiveSelect();//dung de active cai effect select khi click button ItemShields
            }
            else
            {
                rectTransformPant.GetChild(i).GetComponent<ItemUIPant>().DeActiveSelect();
            }

        }
        actionSelectPantStart.Invoke(rectTransformPant.GetChild(0).GetComponent<ItemUIPant>().GetPantType());//truyen type of shield = action de goi den chang shield

    }
    void InstantiateHat()
    {
        Debug.Log(1);
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
                if(i == 0)
                {
                    SetBuySkin(item.GetTypeStateHat());
                }
            }
            
        }
        for (int i = 0; i < hatData.hats.Count; i++)
        {
            if (i == 0)
            {
                RectTransformHat.GetChild(i).GetComponent<ItemUIHat>().ActiveSelect();//dung de active cai effect select khi click button ItemShields
            }
            else
            {
                RectTransformHat.GetChild(i).GetComponent<ItemUIHat>().DeActiveSelect();
            }

        }
        actionSelectHatStart.Invoke(RectTransformHat.GetChild(0).GetComponent<ItemUIHat>().GetHatType());//truyen type of shield = action de goi den chang shield

    }

    void InstantiateShield()//tao ra cac icon khien
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
                
                if (i == 0)
                {

                    SetBuySkin(item.GetStateTypeShield());
                }
            }
        }
        for (int i = 0; i < shieldData.shields.Count; i++)
        {
                if (i == 0)
                {
                rectTransformShield.GetChild(i).GetComponent<ItemUIShield>().ActiveSelect();//dung de active cai effect select khi click button ItemShields
            }
            else
            {
                rectTransformShield.GetChild(i).GetComponent<ItemUIShield>().DeActiveSelect();
            }
            
        }

        actionSelectShieldStart.Invoke(rectTransformShield.GetChild(0).GetComponent<ItemUIShield>().GetShieldType());//truyen type of shield = action de goi den chang shield
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
        actionOpenAnimToSkinfromPlay.Invoke();
        actionNotSelectSkin.Invoke();

    }
    public void SelectSkin()//chon skin
    {
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        UiManager.Instance.CloseUI<CanvasBuySkin>(0f);
        actionChangeExitSkinCameraFlow.Invoke();
        actionOpenAnimToSkinfromPlay.Invoke();
        actionSelectSkin.Invoke();
    }
    void GetData()
    {
        for(int i = 0; i < hatData.hats.Count; i++)
        {
            int a =(int)hatData.hats[i].typeState;
            listHat.Add(a);
        }
        for (int i = 0; i < pantData.pants.Count; i++)
        {
            int a = (int)pantData.pants[i].typeState;
            listPant.Add(a);
        }
        for (int i = 0; i < shieldData.shields.Count; i++)
        {
            int a = (int)shieldData.shields[i].typeState;
            listShield.Add(a);
        }
    }

    void ReleaseList(List<int> list)
    {
        if(list.Count  == 0)
        {
            list.Clear();
        }
    }
   
    public void BuySkin()
    {
        if(_hatitem != null)
        {
            GameController.Instance.SetScore(_hatitem.price);
            _hatitem.typeState = TypeState.haveowned;
            SetBuySkin(_hatitem.typeState);

        }
        else if(_pantitem != null)
        {
            GameController.Instance.SetScore(_pantitem.price);
            _pantitem.typeState = TypeState.haveowned;
            SetBuySkin(_pantitem.typeState);
        }
        else if (_shieldItem != null)
        {
            GameController.Instance.SetScore(_shieldItem.price);
            _shieldItem.typeState = TypeState.haveowned;
            SetBuySkin(_shieldItem.typeState);
        }
        else if(_suitditem != null)
        {
            GameController.Instance.SetScore(_suitditem.price);
            _suitditem.typeState = TypeState.haveowned;
            SetBuySkin(_suitditem.typeState);
        }
        GetData();//lay data tu shop skin
        DataManager.Instance.SaveDataSkin(listHat,listPant,listShield);
        GameController.Instance.UpdateScore();
    }


    //active effect select cua hatitem, an hien nut mua va chon
    void ActiveAndDeActiveEffectSelectHat(HatType newtype,TypeState newState)
    {
        for(int i = 0; i < RectTransformHat.childCount; i++)
        {
            if(RectTransformHat.GetChild(i).GetComponent<ItemUIHat>().GetHatType() == newtype)
            {
                RectTransformHat.GetChild(i).GetComponent<ItemUIHat>().ActiveSelect();
                SetBuySkin(newState);
            }
            else
            {
                RectTransformHat.GetChild(i).GetComponent<ItemUIHat>().DeActiveSelect();
            }
        }
    }
    //active effect select cua ItemUIPant, an hien nut mua va chon
    void ActiveAndDeActiveEffectSelectPant(PantType newtype, TypeState newState)
    {
        for (int i = 0; i < rectTransformPant.childCount; i++)
        {
            if (rectTransformPant.GetChild(i).GetComponent<ItemUIPant>().GetPantType() == newtype)
            {
                rectTransformPant.GetChild(i).GetComponent<ItemUIPant>().ActiveSelect();
                SetBuySkin(newState);
            }
            else
            {
                rectTransformPant.GetChild(i).GetComponent<ItemUIPant>().DeActiveSelect();
            }
        }
    }
    //active effect select cua ItemUIShield, an hien nut mua va chon
    void ActiveAndDeActiveEffectSelectShield(ShieldType newtype, TypeState newState)
    {
        for (int i = 0; i < rectTransformShield.childCount; i++)
        {
            if (rectTransformShield.GetChild(i).GetComponent<ItemUIShield>().GetShieldType() == newtype)
            {
                rectTransformShield.GetChild(i).GetComponent<ItemUIShield>().ActiveSelect();
                SetBuySkin(newState);
            }
            else
            {
                rectTransformShield.GetChild(i).GetComponent<ItemUIShield>().DeActiveSelect();
            }
        }
    }

    //overload Setscore cac loai item
    void SetScoreItem(int value,HatItem newHat)
    {
        _textScore.text = value.ToString();
        _hatitem = newHat;
    }
    void SetScoreItem(int value, PantItem newPant)
    {
        _textScore.text = value.ToString();
        _pantitem = newPant;
    }
    void SetScoreItem(int value, ShieldItem newShield)
    {
        _textScore.text = value.ToString();
        _shieldItem = newShield;
    }
    void SetScoreItem(int value, SuitdItem suit)
    {
        _textScore.text = value.ToString();
        _suitditem = suit;
    }

    void SetBuySkin(TypeState newType)//dung de active button select khi item do da duoc mua và nguoc lai
    {
        if (newType == TypeState.haveowned)
        {
            selectSkinButton.gameObject.SetActive(true);
            buySkinButton.gameObject.SetActive(false);
        }
        else
        {
            buySkinButton.gameObject.SetActive(true);
            selectSkinButton.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        //dung de bat tat nut mua va nut select
        //con dung de bat tat hieu ung select
        ItemUIHat.selectHatEffectaction += ActiveAndDeActiveEffectSelectHat;
        ItemUIPant.selectPantEffectAction += ActiveAndDeActiveEffectSelectPant;
        ItemUIShield.SelectShieldEffectAction += ActiveAndDeActiveEffectSelectShield;


        //set price cho tung item
        ItemUIHat.GetPriceHataction += SetScoreItem;
        ItemUIPant.GetPricePantaction += SetScoreItem;
        ItemUIShield.GetPriceShieldaction += SetScoreItem;

        CanvasGamePlay.actionChangeSkinCameraFlow += InstantiateHat;//khi an vao nut changeskin o gamePlay thi se goi den ham tao ra mu va focus vao cai mũ dau tien
    }

    private void OnDisable()
    {
        CanvasGamePlay.actionChangeSkinCameraFlow -= InstantiateHat;
        ItemUIHat.selectHatEffectaction -= ActiveAndDeActiveEffectSelectHat;
        ItemUIPant.selectPantEffectAction -= ActiveAndDeActiveEffectSelectPant;


        
        ItemUIHat.GetPriceHataction -= SetScoreItem;
        ItemUIPant.GetPricePantaction -= SetScoreItem;
        ItemUIShield.GetPriceShieldaction -= SetScoreItem;

        ItemUIShield.SelectShieldEffectAction -= ActiveAndDeActiveEffectSelectShield;
    }
}
