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
    }

    void ExitShop()
    {
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
        _IconWeapon.sprite = weaponData.GetSprite((WeaponType)weaponNR);
        CheckBuySkin(weaponData.GetTypeState((WeaponType)weaponNR));
        SetPriceForWeapon(weaponData.GetPriceWeapon((WeaponType)weaponNR));
    }
    void NextLeft()
    {
        weaponNR--;
        if (weaponNR < 0)
        {
            weaponNR = weaponData.weapons.Count -1;
        }
        _IconWeapon.sprite = weaponData.GetSprite((WeaponType)weaponNR);
        CheckBuySkin(weaponData.GetTypeState((WeaponType)weaponNR));
        SetPriceForWeapon(weaponData.GetPriceWeapon((WeaponType)weaponNR));
    }
    void CheckBuySkin(TypeState type)
    {
        if(type == TypeState.notyetowned)
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        }else if(type == TypeState.haveowned)
        {
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }else if(type == TypeState.selected)
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
        }
    }
    
    void BuySkin()
    {
        int a = int.Parse(_textPriceWeapon.text);
        CheckBuySkin(TypeState.haveowned);
        GameController.Instance.SetScore(a);
        GameController.Instance.UpdateScore();
    }
    void SaveDataWeapon()
    {
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
        selectWeaponAction.Invoke((WeaponType)weaponNR);
        actionOpenAnimToWeaponfromPlay.Invoke();
        UiManager.Instance.CloseUI<CanvasBuyWeapon>(0f);
        
    }
}
