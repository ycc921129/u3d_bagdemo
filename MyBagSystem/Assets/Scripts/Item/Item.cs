using UnityEngine;

public abstract class Item  
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Itemtype { get; set; }
    public QualityType Qualitytype { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string SpritePath { get; set; }

    public Item(int id,string name,ItemType itemtype, QualityType qualitytype, string description,int capacity,int buyprice,int sellprice, string spritePath)
    {
        ID = id;
        Name = name;
        Itemtype = itemtype;
        Qualitytype = qualitytype;
        Description = description;
        Capacity = capacity;
        BuyPrice = buyprice;
        SellPrice = sellprice;
        SpritePath = spritePath;
    }

    public enum ItemType
    {
        ConsumableItem,
        EquipmentItem,
        WeaponItem,
        MaterialItem
    }
    public enum QualityType
    {
        White,
        Green,
        Blue,
        Purple,
        Orange,
        Red
    }

    public virtual string GetItemInfo()
    {
        string color = string.Empty;
        switch (Qualitytype)
        {
            case QualityType.White:
                color = "White";
                break;
            case QualityType.Green:
                color = "Green";
                break;
            case QualityType.Blue:
                color = "Blue";
                break;
            case QualityType.Purple:
                color = "Purple";
                break;
            case QualityType.Orange:
                color = "Orange";
                break;
            case QualityType.Red:
                color = "Red";
                break;
            default:break;
        }
        string type = string.Empty;
        switch (Itemtype)
        {
            case ItemType.ConsumableItem:
                type = "消耗品";
                break;
            case ItemType.EquipmentItem:
                type = "防具";
                break;
            case ItemType.WeaponItem:
                type = "武器";
                break;
            case ItemType.MaterialItem:
                type = "材料";
                break;
            default:break;
        }
        string itemInfo = string.Format("<color={0}><size=35>{1}</size></color>\n类型：{2}\n描述：{3}\n购买价格：{4}\n出售价格：{5}\n", color, Name, type, Description, BuyPrice, SellPrice);
        return itemInfo;
    }
}