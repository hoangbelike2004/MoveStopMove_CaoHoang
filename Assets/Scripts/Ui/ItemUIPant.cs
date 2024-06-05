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
    [SerializeField] Button selectButton;
    public static UnityAction<PantType> ChangePantAction;
    private void Start()
    {
       selectButton.onClick.AddListener(SelectPant);
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
        ChangePantAction.Invoke(pantItem.pantType);
    }
}
