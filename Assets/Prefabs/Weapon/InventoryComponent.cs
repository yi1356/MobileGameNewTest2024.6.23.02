using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
/// <summary>
/// 武器存储类
/// </summary>
public class InventoryComponent : MonoBehaviour,IPurchaseListenner
{
    public Weapon[] weaponList;
    public Transform[] weaponSlots;
    public Transform defaulttWeaponSlot;
    List<Weapon> weapons = new List<Weapon>();
    
    int currentWeaponIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        InitlizeWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 根据武器列表初始化武器
    /// </summary>
    void InitlizeWeapons()
    {
        foreach (Weapon item in weaponList)
        {
            GivenNewWeapon(item);
        }
        NextWeapon();
    }
    /// <summary>
    /// 增加武器
    /// </summary>
    /// <param name="weapon"></param>
    public void GivenNewWeapon(Weapon weapon)
    {
        Transform Slot = defaulttWeaponSlot;
        List<Transform> weaponSlot = weaponSlots.Where<Transform>(n => n.gameObject.tag == weapon.slotTages).ToList();
        if (weaponSlot.Count > 0) { Slot = weaponSlot[0]; }
        Weapon ww = Instantiate<Weapon>(weapon, Slot);
        ww.Init(gameObject);
        weapons.Add(ww);
        //Debug.Log("测试装备武器");

    }
    /// <summary>
    /// 切换武器
    /// </summary>
    public void NextWeapon()
    {
        int nextWeaponIndex = currentWeaponIndex + 1;
        if (nextWeaponIndex >= weapons.Count)
        {
            nextWeaponIndex = 0;
        }
        EuipWeapon(nextWeaponIndex);
    }
    /// <summary>
    /// 获取当前使用武器
    /// </summary>
    /// <returns></returns>
    public Weapon GetActiveWeapon()
    {
        return weapons[currentWeaponIndex];
    }
    public bool HandheldPurchase(UnityEngine.Object purchaseItem)
    {
        //Debug.Log("测试666666666666666666666666666InventoryComponent.HandheldPurchase已经执行");
        GameObject obj = purchaseItem as GameObject;
        if (obj == null) return false;
        Weapon purchaseWeapon = obj.GetComponent<Weapon>();
        if (purchaseWeapon != null)
        {
            //Debug.Log("测试666666666666666666666666666InventoryComponent.HandheldPurchase已经执行purchaseWeapon != null");
            GivenNewWeapon(purchaseWeapon);
            return true;
        }
        return false;
    }
    //*****************************************************
    private void EuipWeapon(int nextWeaponIndex)
    {
        if (nextWeaponIndex < 0 || nextWeaponIndex >= weapons.Count)
        {
            return;
        }

        if (currentWeaponIndex>=0&&currentWeaponIndex< weapons.Count)
        {
            weapons[currentWeaponIndex].UnEquip();
        }
        weapons[nextWeaponIndex].Equip();

        currentWeaponIndex = nextWeaponIndex;
    }


}
