using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUIShield : MonoBehaviour
{
    private ShieldItem shieldItem;
    public ShieldItem ShieldItem { get => shieldItem; set { shieldItem = value; } }

    [SerializeField] Button buttonthis;
    [SerializeField] Image Icon;
    [SerializeField] Image backGround;
    public static UnityAction<ShieldType> ChangeShieldAction;
    public static UnityAction<ShieldType,TypeState> SelectShieldEffectAction;
    public static UnityAction<int,ShieldItem> GetPriceShieldaction;
    private void Start()
    {
        buttonthis.onClick.AddListener(SelectShield);
        if (DataManager.Instance.GetDataSkin() != null)
        {
            ShieldItem.typeState = (TypeState)DataManager.Instance.GetDataSkin().shieldstates[(int)ShieldItem.shieldtype];
        }
    }

    public void SetDataShield(ShieldItem shieldItem)
    {
        this.shieldItem = shieldItem;
    }

    public void SetIconShield(Sprite sprite)
    {
        this.Icon.sprite = sprite;
    }

    public void SelectShield()
    {
        SelectShieldEffectAction.Invoke(ShieldItem.shieldtype, ShieldItem.typeState);
        ChangeShieldAction.Invoke(ShieldItem.shieldtype);
        GetPriceShieldaction.Invoke(ShieldItem.price, ShieldItem);
    }
    public TypeState GetStateTypeShield()
    {
        return ShieldItem.typeState;
    }
    public ShieldType GetShieldType()
    {
        return ShieldItem.shieldtype;
    }
    public void ActiveSelect()
    {
        backGround.gameObject.SetActive(true);
    }

    public void DeActiveSelect()
    {
        backGround.gameObject.SetActive(false);
    }
}
