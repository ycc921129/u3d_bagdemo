using UnityEngine;
using UnityEngine.EventSystems;

public class EquitMentSlot : Slot 
{
    public EquipmentItem.EquitType equitType;
    public WeaponItem.WeaponType weaponType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && SystemManager.Instance.bagPanel.gameObject.activeSelf)
        {
            ItemUI itemui = transform.GetComponentInChildren<ItemUI>();
            if (itemui != null)
            {
                Item item = itemui.item;
                DestroyImmediate(itemui.gameObject);
                SystemManager.Instance.BagPutOn(item);
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (SystemManager.Instance.PickUp)
        {
            ItemUI pickItem = SystemManager.Instance.pickItem;
            if (isRightItem(pickItem.item))
            {
                ItemUI currentItem = transform.GetComponentInChildren<ItemUI>();
                if (currentItem != null)
                {
                    pickItem.ExchangeItem(currentItem);
                }
                else
                {
                    StoryItem(pickItem.item);
                    SystemManager.Instance.ReducePickItemAmount(pickItem.Amount);
                }
            }
        }
        else
        {
            ItemUI currentItemui = transform.GetComponentInChildren<ItemUI>();
            if (currentItemui != null)
            {
                SystemManager.Instance.SetPickUpItem(currentItemui.item, currentItemui.Amount);
                Destroy(currentItemui.gameObject);
            }
        }
    }
    public bool isRightItem(Item item)
    {
        if (item is EquipmentItem && (item as EquipmentItem).Equittype == equitType ||
                  item is WeaponItem && (item as WeaponItem).Weapontype == weaponType)
        {
            return true;
        }
        return false;
    }
}