using UnityEngine;

public class ShotPanel : BasePanel 
{
    protected override void Start()
    {
        base.Start();
        InitShotPanel();
    }

    public void InitShotPanel()
    {
        foreach (Slot slot in slots)
        {
            ShopSlot shopSlot = slot as ShopSlot;
            StoryItem(UnityEngine.Random.Range(3, 15));
        }
    }

    public void SellItem(ItemUI itemui)
    {
        if (itemui != null)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                systemManager.Coin += itemui.item.SellPrice;
                systemManager.ReducePickItemAmount(1);
            }
            else
            {
                systemManager.Coin += itemui.item.SellPrice * itemui.Amount;
                systemManager.ReducePickItemAmount(itemui.Amount);
            }
        }
    }
    public void BuyItem(Item item)
    {
        if (item != null && systemManager.Coin >= item.BuyPrice && systemManager.bagPanel.gameObject.activeSelf)
        {
            systemManager.Coin -= item.BuyPrice;
            systemManager.BagPutOn(item);
        }
    }
}