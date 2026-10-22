using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditComponent : MonoBehaviour,IRewardListenner
{
    public int credit=200;
    private List<IPurchaseListenner> PurchaseListennerInterfaces=new List<IPurchaseListenner>();//记录的购买者

    public delegate void OnCreditUpdated(int credit);
    public event OnCreditUpdated onCreditUpdated;

    private void Start()
    {
        //UpdateCreditImmaditly();
        CollectPurchaseListenner();
    }

    /// <summary>
    /// 购买的函数（侧重点在，是否可买，装备所购商品）
    /// </summary>
    /// <param name="price"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Purchase(int price,ShopItem item)
    {
        //Debug.Log("测试44444444444444444444CreditComponent.Purchase" + PurchaseListennerInterfaces.Count);
        foreach (IPurchaseListenner purchase in PurchaseListennerInterfaces)
        {
            if (credit<price)
            {
                
                return false;
            }
            else
            {
                //Debug.Log("测试333333333333333CreditComponent.Purchase----credit>price");
            }
            if (purchase.HandheldPurchase(item.Item))
            {
                //Debug.Log("测试333333333333333CreditComponent.Purchase----purchase.HandheldPurchase(item)");
                credit -= price;
                onCreditUpdated?.Invoke(credit);
                return true;
            }
        }
        //Debug.Log("测试333333333333333CreditComponent.Purchase");
        return false;
    }

    //记录所有购买者对象
    private void CollectPurchaseListenner()
    {
        IPurchaseListenner[] interpurchases = this.GetComponents<IPurchaseListenner>();
        foreach (IPurchaseListenner item in interpurchases)
        {
            PurchaseListennerInterfaces.Add(item);
        }
        //Debug.Log("记录所有购买者对象数量为"+PurchaseListennerInterfaces.Count);
    }

    public  void UpdateCreditImmaditly()
    {
        onCreditUpdated?.Invoke(credit);
    }

    public void Reward(Reward reward)
    {
        credit +=reward.creditReward;
        onCreditUpdated?.Invoke(credit);
    }
}
