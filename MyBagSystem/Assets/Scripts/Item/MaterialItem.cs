using UnityEngine;

public class MaterialItem : Item
{
    public MaterialItem(int id, string name, ItemType itemtype, QualityType qualitytype, string description, int capacity, int buyprice, int sellprice, string spritePath) 
        : base(id, name, itemtype, qualitytype, description, capacity, buyprice, sellprice,spritePath)
    {
    }

    public override string GetItemInfo()
    {
        return base.GetItemInfo();
    }
}