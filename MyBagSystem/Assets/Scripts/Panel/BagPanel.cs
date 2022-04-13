using System;
using UnityEngine;

public class BagPanel : BasePanel
{
    protected override void Start()
    {
        base.Start();
    }

    public void AddItem()
    {
        StoryItem(UnityEngine.Random.Range(1, 17));
    }       

    public void PutOn(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0 && item != null)
            {
                StoryItem(item);
                break;
            }
        }
    }
    public void PutOn(int id)
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                StoryItem(id);
                break;
            }
        }
    }

    public void ZhengliItem()
    {
        int nullIndex = 0;
        for (int i = 0; i < slots.Length - 1; i++)
        {
            ItemUI current = slots[i].GetComponentInChildren<ItemUI>();
            if (current == null)
            {
                nullIndex = i;
                continue;
            }
            for (int j = i + 1; j < slots.Length; j++)
            {
                ItemUI next = slots[j].GetComponentInChildren<ItemUI>();
                if (next == null)
                {
                    continue;
                }
                if (current.item.ID == next.item.ID)
                {
                    if (current.item.Capacity > current.Amount + next.Amount)
                    {
                        current.SetItem(current.item, current.Amount + next.Amount);
                        Destroy(next.gameObject);
                    }
                    else
                    {
                        int addnum = current.item.Capacity - current.Amount;
                        current.SetItem(current.item, addnum);
                        next.SetItem(next.item, -addnum);
                        break;
                    }
                }
            }
        }      
    }
}