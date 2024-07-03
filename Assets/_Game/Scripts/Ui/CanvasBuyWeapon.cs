using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasBuyWeapon : UiCanvas
{
    [SerializeField] Button exitShopButton;
    [SerializeField] Button nextRightButton;
    [SerializeField] Button nextLeftButton;
    [SerializeField] Button selectButton;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI _textnameWeapon;
    [SerializeField] TextMeshProUGUI _textPriceWeapon;
    [SerializeField] Image _IconWeapon;
    [SerializeField] WeaponData1 weaponData;
    public WeaponItem weaponitem;
    [SerializeField] int weaponNR = 0;
    [SerializeField] List<int> listWeapondata;
    public SpriteRenderer spr;
    public static UnityAction<WeaponType> selectWeaponAction;
    public static UnityAction actionOpenAnimToWeaponfromPlay;

    private void Start()
    {
        listWeapondata = new List<int>(weaponData.weapons.Count);
        exitShopButton.onClick.AddListener(ExitShop);
        nextRightButton.onClick.AddListener(NextRight);
        nextLeftButton.onClick.AddListener(NextLeft);
        selectButton.onClick.AddListener(SelectWeapon);
        buyButton.onClick.AddListener(BuySkin);

        MyClass<DataWeapon>.GetDataKey(Contains.DATA_WEAPON);
    }

    void ExitShop()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        actionOpenAnimToWeaponfromPlay.Invoke();
        UiManager.Instance.CloseUI<CanvasBuyWeapon>(0f);


    }

    void NextRight()
    {

        weaponNR++;
        if(weaponNR == weaponData.weapons.Count)
        {
            weaponNR = 0;
        }
        weaponitem = weaponData.weapons[weaponNR];
        _IconWeapon.sprite = weaponitem.iconWeapon;
        _textnameWeapon.text = weaponitem.name;
        CheckBuySkin(weaponitem.typeState);
        SetPriceForWeapon(weaponitem.price);
    }
    void NextLeft()
    {
        weaponNR--;
        if (weaponNR < 0)
        {
            weaponNR = weaponData.weapons.Count -1;
        }
        weaponitem = weaponData.weapons[weaponNR];
        _IconWeapon.sprite = weaponitem.iconWeapon;
        _textnameWeapon.text = weaponitem.name;
        CheckBuySkin(weaponitem.typeState);
        SetPriceForWeapon(weaponitem.price);
    }
    void CheckBuySkin(TypeState type)
    {
        if(type == TypeState.notyetowned)
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        }
        if(type == TypeState.haveowned)
        {
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        if(type == TypeState.selected)
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }
    }
    
    void BuySkin()
    {
        int a = int.Parse(_textPriceWeapon.text);
        weaponitem.typeState = TypeState.haveowned;
        CheckBuySkin(TypeState.haveowned);
        GameController.Instance.SetScore(a);
        GameController.Instance.UpdateScore();
        SaveDataWeapon();
        //DataManager.Instance.SaveDataWeapon(listWeapondata);
    }
    void SaveDataWeapon()
    {
        if(listWeapondata.Count > 0)
        {
            listWeapondata.Clear();
        }
        
        for(int i = 0;i < weaponData.weapons.Count;i++)
        {
            int a = (int)weaponData.weapons[i].typeState;
            listWeapondata.Add(a);
        }
    }
    void SetPriceForWeapon(int score)
    {
        _textPriceWeapon.text = score.ToString();
    }
    void SelectWeapon()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.acClick, 1);
        selectWeaponAction.Invoke(weaponitem.wpType);
        weaponitem.typeState = TypeState.selected;
        ReadJustDataWeapon(weaponitem);
        actionOpenAnimToWeaponfromPlay.Invoke();
        UiManager.Instance.CloseUI<CanvasBuyWeapon>(0f);
    }
    void CheckBuySkinKhiOpenChangSkin()
    {
        weaponNR = 0;
        weaponitem = weaponData.weapons[weaponNR];
        _IconWeapon.sprite = weaponitem.iconWeapon;
        _textnameWeapon.text = weaponitem.name;
        SetPriceForWeapon(weaponitem.price);
        CheckBuySkin(weaponitem.typeState);
    }
    void ReadJustDataWeapon(WeaponItem item)
    {
        foreach(WeaponItem it in weaponData.weapons)
        {
            if(it.typeState == TypeState.selected && it.wpType != item.wpType)
            {
                it.typeState = TypeState.haveowned;
            }
        }
    }
    void OpenShopWeapon()
    {
        weaponNR = 0;
        weaponitem = weaponData.weapons[weaponNR];
        _IconWeapon.sprite = weaponitem.iconWeapon;
        _textnameWeapon.text = weaponitem.name;
        CheckBuySkin(weaponitem.typeState);
        SetPriceForWeapon(weaponitem.price);
    }

    private void OnEnable()
    {
        CanvasGamePlay.actionChangeWEapon += CheckBuySkinKhiOpenChangSkin;
    }

    private void OnDisable()
    {
        CanvasGamePlay.actionChangeWEapon -= CheckBuySkinKhiOpenChangSkin;
    }
}
