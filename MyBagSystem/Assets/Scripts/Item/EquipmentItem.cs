using UnityEngine;

public class EquipmentItem : Item
{
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Statmina { get; set; }
    public EquitType Equittype { get; set; }

    public EquipmentItem(int id, string name, ItemType itemtype, QualityType qualitytype, string description, int capacity, int buyprice, int sellprice, string spritePath,
        int strength,int intellect,int agility,int statmina,EquitType equittype) : base(id, name, itemtype, qualitytype, description, capacity, buyprice, sellprice,spritePath)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Statmina = statmina;
        Equittype = equittype;
    }

    public enum EquitType
    {
        None,
        Head,
        Neck,
        Armor,
        Ring,
        Leg,
        Bracer,
        Boots,
        Shoudler,
        Belt
    }

    public override string GetItemInfo()
    {
        string itemInfo = base.GetItemInfo();
        itemInfo += string.Format("力量：{0}\n敏捷：{1}\n智力：{2}\n体力：{3}", Strength, Intellect, Agility, Statmina);
        return itemInfo;
    }
}