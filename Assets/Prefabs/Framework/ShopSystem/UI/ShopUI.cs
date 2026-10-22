using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 商店面板UI
/// </summary>
public class ShopUI : MonoBehaviour
{
    public ShopSystem shopSystem;
    public CreditComponent creditComponent;
    public ShopItemUI shopItemUIPrefab;
    public RectTransform shoplist;//生成的商品UI图标的父对象
    public UIManager uiManager;

    public TextMeshProUGUI textCredit;
    public Button backBtn;
    public Button buyBtn;


    private List<ShopItem> shopitems=new List<ShopItem>();
    private ShopItemUI seletShopItem;
    private List<ShopItemUI> ShopItemUIs=new List<ShopItemUI>();
    private void Start()
    {
        InitShopItem();
        buyBtn.onClick.AddListener(TryPurchaseItem);
        creditComponent.onCreditUpdated += UpdateCredit;
        backBtn.onClick.AddListener(()=> { uiManager.SwitchToGamePlay(); });

    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void InitShopItem()
    {
        shopitems = shopSystem.GetShopItem();
        for (int i = 0; i < shopitems.Count; i++)
        {
            AddShopItem(shopitems[i]);
        }
        Debug.Log("测试222222222222222ShopUI.shopitems.Count"+ shopitems.Count);
        RefreshItems();
    }
    /// <summary>
    /// 增加商店物品UI
    /// </summary>
    /// <param name="item"></param>
    public void AddShopItem(ShopItem item)
    {
        ShopItemUI shopItemUI = GameObject.Instantiate<ShopItemUI>(shopItemUIPrefab, shoplist);
        ShopItemUIs.Add(shopItemUI);
        shopItemUI.Init(item, creditComponent.credit);
        shopItemUI.onItemSelected += SelectedItem;
        RefreshItems();
    }

    /// <summary>
    /// 观察者：更新选中商品UI
    /// </summary>
    /// <param name="shopItemUI"></param>
    private void SelectedItem(ShopItemUI shopItemUI)
    {
        this.seletShopItem = shopItemUI;
    }

    /// <summary>
    /// 购买按钮的逻辑
    /// </summary>
    private void TryPurchaseItem()
    {
        RefreshItems();
        if (seletShopItem==null)
        {
            return;
        }
        if (shopSystem.TryPurChase(seletShopItem.GetShopItem(), creditComponent))
        {
            ShopItemUIs.Remove(seletShopItem);
            Destroy(seletShopItem.gameObject);
        }


    }

    /// <summary>
    /// 观察者：更新credit显示
    /// </summary>
    /// <param name="credit"></param>
    /// <param name="delt"></param>
    /// <param name="maxCredit"></param>
    private void UpdateCredit(int credit)
    {
        textCredit.text = "$" + credit;
        RefreshItems();
    }
    /// <summary>
    /// 更新商场面板UI
    /// </summary>
    private void RefreshItems()
    {
        for (int i=0;i<ShopItemUIs.Count;i++)
        {
            ShopItemUIs[i].Refresh(creditComponent.credit);
        }
    } 



}
