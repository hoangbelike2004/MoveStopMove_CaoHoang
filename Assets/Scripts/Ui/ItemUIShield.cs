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
    public static UnityAction<ShieldType> ChangeShieldAction;
    private void Start()
    {
        buttonthis.onClick.AddListener(SelectShield);

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
        ChangeShieldAction.Invoke(ShieldItem.shieldtype);
    }
}
