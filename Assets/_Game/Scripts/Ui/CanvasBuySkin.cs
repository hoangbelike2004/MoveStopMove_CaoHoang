using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    public static UnityAction<HatItem,PantItem,ShieldItem,SuitdItem> actionSelectSkin;
    public static UnityAction actionNotSelectSkin;
    public static UnityAction actionOpenAnimToSkinfromPlay;
    public static UnityAction<HatType> actionSelectHatStart;
    public static UnityAction<ShieldType> actionSelectShieldStart;
    public static UnityAction<PantType> actionSelectPantStart;
    public static UnityAction<SuitType> actionSelectSuitStart;



    [Header("SKINITEM")]
    public HatItem _hatitem;
    public ShieldItem _shieldItem;
    public PantItem _pantitem;
    public SuitdItem _suitditem;


    [Header("GridLayOutGroup")]
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform _buttonlayoutChangSkin;
    public float spacing, witdhRange,heightRange;
 
    private void Awake()
    {
        colorCurrent = IconButton[0].color;
        
    }
    private void Start()
    {
        listHat = new List<int>();
        listPant = new List<int>();
        listShield = new List<int>();
        Vector2 newvt = new Vector2(_buttonlayoutChangSkin.rect.width / witdhRange - spacing, Screen.width / heightRange);
        gridLayoutGroup.cellSize = newvt;
        hatButton.onClick.AddListener(InstantiateHat);
        pantButton.onClick.AddListener(InstantiatePant);
        shieldButton.onClick.AddListener(InstantiateShield);
        SuitButton.onClick.AddListener(InstantiateSuit);
        ExitBuySkin.onClick.AddListener(Exit);
        selectSkinButton.onClick.AddListener(EventSelectSkin);
        buySkinButton.onClick.AddListener(BuySkin);
    }
    void InstantiatePant()
    {
        _hatitem = null;
        _shieldItem = null;
        _suitditem = null;
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
                    _pantitem = pantData.pants[i];
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
        _pantitem = null;
        _shieldItem = null;
        _suitditem = null;
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
                    _hatitem = hatData.hats[i];
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
        _hatitem = null;
        _pantitem = null;
        _suitditem = null;
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
                    _shieldItem = shieldData.shields[i];
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
        _hatitem = null;
        _shieldItem = null;
        _pantitem = null;
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
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        UiManager.Instance.CloseUI<CanvasBuySkin>(0f);
        actionChangeExitSkinCameraFlow.Invoke();
        actionOpenAnimToSkinfromPlay.Invoke();
        actionNotSelectSkin.Invoke();

    }
    public void EventSelectSkin()//chon skin
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        ActiveScrollObjectUsed(TypeScroll.scrollHat);
        ChangeColorObjectUsed(TypeScroll.scrollHat);
        UiManager.Instance.CloseUI<CanvasBuySkin>(0f);
        actionChangeExitSkinCameraFlow.Invoke();
        actionOpenAnimToSkinfromPlay.Invoke();
        if (_hatitem != null)
        {
            _hatitem.typeState = TypeState.selected;
            actionSelectSkin.Invoke(_hatitem,null,null,null);
        }
        else if(_pantitem != null)
        {
            _pantitem.typeState = TypeState.selected;
            actionSelectSkin.Invoke(null, _pantitem, null, null);
        }
        else if (_shieldItem != null)
        {
            _shieldItem.typeState = TypeState.selected;
            actionSelectSkin.Invoke(null, null, _shieldItem, null);
        }
        else if(_suitditem != null)
        {
            actionSelectSkin.Invoke(null, null, null, _suitditem);
        }
        ReadJustDataSkin();
        GetData();
        DataManager.Instance.SaveDataSkin(listHat, listPant, listShield);

    }

    void ReadJustDataSkin()
    {
        if (_hatitem != null)
        {
            for (int i = 0; i < hatData.hats.Count; i++)
            {
                if(hatData.hats[i].hattype != _hatitem.hattype &&
                  hatData.hats[i].typeState == TypeState.selected)
                {
                    hatData.hats[i].typeState = TypeState.haveowned;
                }
            }
        }
        else if (_pantitem != null)
        {
            for (int i = 0; i < pantData.pants.Count; i++)
            {
                if (pantData.pants[i].pantType != _pantitem.pantType &&
                  pantData.pants[i].typeState == TypeState.selected)
                {
                    pantData.pants[i].typeState = TypeState.haveowned;
                }
            }
        }
        else if (_shieldItem != null)
        {
            for (int i = 0; i < shieldData.shields.Count; i++)
            {
                if (shieldData.shields[i].shieldtype != _shieldItem.shieldtype &&
                  shieldData.shields[i].typeState == TypeState.selected)
                {
                    shieldData.shields[i].typeState = TypeState.haveowned;
                }
            }
        }
        else if (_suitditem != null)
        {
        }
    }
    void GetData()
    {
        ReleaseList(listShield);
        ReleaseList(listHat);
        ReleaseList(listPant);
        for (int i = 0; i < hatData.hats.Count; i++)
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
            GameController.Instance.SetScore(_hatitem.price);//set lai score của minh dang co khi mua hatitem 
            _hatitem.typeState = TypeState.haveowned;

            //khi an vao buy skin de deactive button buyskin di va active buttonselect
            SetBuySkin(_hatitem.typeState);

        }
        if(_pantitem != null)
        {
            GameController.Instance.SetScore(_pantitem.price);
            _pantitem.typeState = TypeState.haveowned;
            SetBuySkin(_pantitem.typeState);
        }
        if (_shieldItem != null)
        {
            GameController.Instance.SetScore(_shieldItem.price);
            _shieldItem.typeState = TypeState.haveowned;
            SetBuySkin(_shieldItem.typeState);
        }
        if(_suitditem != null)
        {
            GameController.Instance.SetScore(_suitditem.price);
            _suitditem.typeState = TypeState.haveowned;
            SetBuySkin(_suitditem.typeState);
        }
        GetData();//lay data tu shop skin
        DataManager.Instance.SaveDataSkin(listHat,listPant,listShield);//save lai data vưa get
        GameController.Instance.UpdateScore();//Update lai score o gameplay
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
    void SetScoreItemHat(int value,HatItem newHat)
    {
        _textScore.text = value.ToString();
        _hatitem = newHat;
    }
    void SetScoreItemPant(int value, PantItem newPant)
    {
        _textScore.text = value.ToString();
        _pantitem = newPant;
    }
    void SetScoreItemShield(int value, ShieldItem newShield)
    {
        _textScore.text = value.ToString();
        _shieldItem = newShield;
    }
    void SetScoreItemSuit(int value, SuitdItem suit)
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
        if(newType == TypeState.notyetowned) 
        {
            buySkinButton.gameObject.SetActive(true);
            selectSkinButton.gameObject.SetActive(false);
        }
        if (newType == TypeState.selected)
        {
            buySkinButton.gameObject.SetActive(false);
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
        ItemUIHat.GetPriceHataction += SetScoreItemHat;
        ItemUIPant.GetPricePantaction += SetScoreItemPant;
        ItemUIShield.GetPriceShieldaction += SetScoreItemShield;

        CanvasGamePlay.actionChangeSkinCameraFlow += InstantiateHat;//khi an vao nut changeskin o gamePlay thi se goi den ham tao ra mu va focus vao cai mũ dau tien
    }

    private void OnDisable()
    {
        CanvasGamePlay.actionChangeSkinCameraFlow -= InstantiateHat;
        ItemUIHat.selectHatEffectaction -= ActiveAndDeActiveEffectSelectHat;
        ItemUIPant.selectPantEffectAction -= ActiveAndDeActiveEffectSelectPant;


        
        ItemUIHat.GetPriceHataction -= SetScoreItemHat;
        ItemUIPant.GetPricePantaction -= SetScoreItemPant;
        ItemUIShield.GetPriceShieldaction -= SetScoreItemShield;

        ItemUIShield.SelectShieldEffectAction -= ActiveAndDeActiveEffectSelectShield;
    }
}