using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : Slot 
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && SystemManager.Instance.shotPanel.gameObject.activeSelf)
        {
            ItemUI currentItemui = transform.GetComponentInChildren<ItemUI>();
            if (currentItemui != null)
            {
                Item currentItem = currentItemui.item;
                transform.parent.parent.SendMessage("BuyItem", currentItem);
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left && SystemManager.Instance.shotPanel.gameObject.activeSelf)
        {
            if (SystemManager.Instance.PickUp)
            {
                transform.parent.parent.SendMessage("SellItem", SystemManager.Instance.pickItem);
            }
        }
    }
}