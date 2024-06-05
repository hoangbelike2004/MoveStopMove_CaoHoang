using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUIHat : MonoBehaviour
{
    private HatItem hatitem;
    public HatItem Hatitem { get => hatitem; set => hatitem = value; }
    [SerializeField] Image icon;
    [SerializeField] Button buttonThis;
    public static UnityAction<HatType> ChangeHatAction;

    private void Start()
    {
        buttonThis.onClick.AddListener(SelectHat);
    }

    public void SetDataHat(HatItem itemUiHat)
    {
        this.hatitem = itemUiHat;
    }

    public void SetIconHat(Sprite icon)
    {
       this.icon.sprite = icon;
    }
    
    public void SelectHat()
    {
        ChangeHatAction.Invoke(Hatitem.hattype);
    }
}
