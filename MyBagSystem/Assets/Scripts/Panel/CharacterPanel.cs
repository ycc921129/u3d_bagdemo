using UnityEngine;

public class CharacterPanel : BasePanel 
{
    protected override void Start()
    {
        base.Start();
    }

    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (var slot in slots)
        {
            EquitMentSlot quitMentSlot = slot as EquitMentSlot;
            ItemUI itemui = slot.GetComponentInChildren<ItemUI>();
            if (quitMentSlot.isRightItem(item))
            {
                if (itemui != null)
                {
                    exitItem = itemui.item;
                    itemui.SetItem(item);
                }
                else
                {
                    quitMentSlot.StoryItem(item);
                }
                break;
            }
        }

        if (exitItem != null)
        {
            systemManager.BagPutOn(exitItem);
        }
    }
}