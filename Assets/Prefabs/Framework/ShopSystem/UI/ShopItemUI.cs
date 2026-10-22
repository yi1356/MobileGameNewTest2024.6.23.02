using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 商品图标UI
/// </summary>
public class ShopItemUI : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Desciption;
    public Button button;
    public Image GrayOutCover;
    public Color INEfficientCreditColor;
    public Color SurEfficientCredientColor;

    private ShopItem item;
    public delegate void OnItemSelected(ShopItemUI itemui);
    public event OnItemSelected onItemSelected;
    private void Start()
    {
        button.onClick.AddListener(ItemSelect);
    }
    public ShopItem GetShopItem()
    {
        return item;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="item"></param>
    /// <param name="AvaliableCCredits"></param>
    public void Init(ShopItem item,int AvaliableCredits)
    {
        this.item = item;
        this.Price.text = "$"+item.price.ToString();
        this.Title.text = item.Titile;
        this.Desciption.text = item.Description;
        this.Icon.sprite = item.ItemIcon;
        Refresh(AvaliableCredits);

    }

    /// <summary>
    /// 观察者：更新选中的Shopitem
    /// </summary>
    private void ItemSelect()
    {
        onItemSelected?.Invoke(this);
    }

    /// <summary>
    /// 更新这个商品图标UI
    /// </summary>
    public void Refresh(int AvaliableCredits)
    {
        if (AvaliableCredits>=item.price)
        {
            GrayOutCover.gameObject.SetActive(false);
            Title.color = SurEfficientCredientColor;
            Price.color= SurEfficientCredientColor;
            Desciption.color = SurEfficientCredientColor;
            button.interactable = true;

        }
        else
        {
            GrayOutCover.gameObject.SetActive(true);
            Title.color = INEfficientCreditColor;
            Price.color = INEfficientCreditColor;
            Desciption.color =INEfficientCreditColor;
            button.interactable = false;
        }
    }

}
