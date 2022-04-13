using UnityEngine;

public class WeaponItem : Item
{
    public int Damage { get; set; }
    public WeaponType Weapontype { get; set; }

    public WeaponItem(int id, string name, ItemType itemtype, QualityType qualitytype, string description, int capacity, int buyprice, int sellprice, string spritePath,
        int damage,WeaponType weapontype) : base(id, name, itemtype, qualitytype, description, capacity, buyprice, sellprice,spritePath)
    {
        Damage = damage;
        Weapontype = weapontype;
    }

    public enum WeaponType
    {
        None,
        OffHand,
        MainHand
    }

    public override string GetItemInfo()
    {
        string itemInfo = base.GetItemInfo();
        itemInfo += string.Format("攻击里：{0}", Damage);
        return itemInfo;
    }
}