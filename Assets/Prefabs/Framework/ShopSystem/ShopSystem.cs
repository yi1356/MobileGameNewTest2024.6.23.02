using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 商店系统
/// </summary>
[CreateAssetMenu(menuName="Shop/ShopSystem")]
public class ShopSystem : ScriptableObject
{
    public  List<ShopItem> shopItems;
    public List<ShopItem> GetShopItem()
    {
        return shopItems;
    }

    /// <summary>
    /// 购买商店中的道具/能力（这个是真正的起作用的购买函数）
    /// </summary>
    /// <param name="selectedItem"></param>
    /// <param name="creditComponent"></param>
    /// <returns></returns>
    public bool TryPurChase(ShopItem selectedItem,CreditComponent creditComponent)
    {
        if (creditComponent.Purchase(selectedItem.price,selectedItem))
        {
            return true;
        }
        return false;
    }
}
