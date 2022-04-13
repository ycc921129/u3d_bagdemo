using UnityEngine;

public class ConsumableItem : Item
{
    public int HP { get; set; }
    public int MP { get; set; }
    public ConsumableItem(int id, string name, ItemType itemtype, QualityType qualitytype, string description, int capacity, int buyprice, int sellprice, string spritePath, int hp,int mp)
        : base(id, name, itemtype, qualitytype, description, capacity, buyprice, sellprice,spritePath)
    {
        HP = hp;
        MP = mp;
    }
}