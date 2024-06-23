using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ItemUIPant : MonoBehaviour
{
    private PantItem pantItem;
    public PantItem PantItem { get => pantItem; set => pantItem = value; }
    [SerializeField] Image icon;
    [SerializeField] Image background;
    [SerializeField] Button selectButton;
    public static UnityAction<PantType> ChangePantAction;
    public static UnityAction<PantType,TypeState> selectPantEffectAction;
    public static UnityAction<int,PantItem> GetPricePantaction;
    private void Start()
    {
       selectButton.onClick.AddListener(SelectPant);
        if (MyClass<DataSkin>.GetDataKey(Contains.DATA_SKIN) != null)
        {
            PantItem.typeState = (TypeState)MyClass<DataSkin>.GetDataKey(Contains.DATA_SKIN).pantstates[(int)PantItem.pantType];
        }
    }

    public void SetDataPant(PantItem pantitem)
    {
        this.pantItem = pantitem;
    }
    public void SetSprite(Sprite sprite)
    {
        icon.sprite = sprite;
    }
    private void SelectPant()
    {
        selectPantEffectAction.Invoke(PantItem.pantType,PantItem.typeState);
        ChangePantAction.Invoke(pantItem.pantType);
        GetPricePantaction.Invoke(PantItem.price,PantItem);
    }
    public PantType GetPantType()
    {
        return PantItem.pantType;
    }
    public TypeState GetStateTypePant()
    {
        return PantItem.typeState;
    }
    public void ActiveSelect()
    {
        background.gameObject.SetActive(true);
    }

    public void DeActiveSelect()
    {
        background.gameObject.SetActive(false);
    }

}
