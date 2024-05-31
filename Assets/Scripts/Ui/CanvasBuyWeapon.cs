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
    [SerializeField] TextMeshProUGUI _textnameWeapon;
    [SerializeField] TextMeshProUGUI _textPriceWeapon;
    [SerializeField] Image _IconWeapon;
    [SerializeField] WeaponData1 weaponData;
    [SerializeField] int weaponNR = 0;
    public SpriteRenderer spr;
    public static UnityAction<WeaponType> selectWeaponAction;
    
    private void Start()
    {
        exitShopButton.onClick.AddListener(ExitShop);
        nextRightButton.onClick.AddListener(NextRight);
        nextLeftButton.onClick.AddListener(NextLeft);
        selectButton.onClick.AddListener(SelectWeapon);
    }

    void ExitShop()
    {
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
    }
    void NextLeft()
    {
        weaponNR--;
        if (weaponNR < 0)
        {
            weaponNR = weaponData.weapons.Count -1;
        }
    }
    void SelectWeapon()
    {
        selectWeaponAction.Invoke((WeaponType)weaponNR);
        UiManager.Instance.CloseUI<CanvasBuyWeapon>(0f);
    }
}
