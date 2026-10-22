using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 购买者需要继承的接口（AbilityComponent、InventoryComponent）
/// </summary>
public interface IPurchaseListenner 
{
    /// <summary>
    /// 装备所购商品的方法
    /// </summary>
    /// <param name="purchaseItem"></param>
    /// <returns></returns>
    public bool HandheldPurchase(Object purchaseItem);
}
