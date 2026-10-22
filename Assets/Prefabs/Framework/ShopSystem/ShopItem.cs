using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 商店物品数据类
/// </summary>
[CreateAssetMenu(menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string Titile;
    public int price=20;
    public Object Item;
    public Sprite ItemIcon;
    [TextArea]
    public string Description;

}
