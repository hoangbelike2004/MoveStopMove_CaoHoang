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
    [SerializeField] Image backGround;
    [SerializeField] Button buttonThis;
    public static UnityAction<HatType> ChangeHatAction;
    public static UnityAction<HatType,TypeState> selectHatEffectaction;
    public static UnityAction<int,HatItem> GetPriceHataction;

    private void Start()
    {
        buttonThis.onClick.AddListener(SelectHat);
        if(MyClass<DataSkin>.GetDataKey(Contains.DATA_SKIN) != null)
        {
            Hatitem.typeState = (TypeState)MyClass<DataSkin>.GetDataKey(Contains.DATA_SKIN).hatstates[(int)Hatitem.hattype];
        }
        
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
        selectHatEffectaction.Invoke(Hatitem.hattype, Hatitem.typeState);
        ChangeHatAction.Invoke(Hatitem.hattype);// chon do khi dang mua(Thay doi do tren nguoi choi ke ca khong mua)
        GetPriceHataction.Invoke(Hatitem.price,Hatitem);
    }
    public HatType GetHatType()
    {
        return Hatitem.hattype;
    }
    public TypeState GetTypeStateHat()
    {
        return Hatitem.typeState;
    }
    public int GetPriceHat()
    {
        return Hatitem.price;
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
