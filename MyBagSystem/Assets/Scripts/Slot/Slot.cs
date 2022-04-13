using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public GameObject prefab;
    public void StoryItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject obj = Instantiate<GameObject>(prefab);
            obj.transform.SetParent(this.transform, false);
            obj.transform.localPosition = Vector3.zero;
            obj.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetComponentInChildren<ItemUI>().AddAmount();
        }
    }

    public Item.ItemType GetItemType()
    {
        return transform.GetComponentInChildren<ItemUI>().item.Itemtype;
    }

    public int GetItemID()
    {
        return transform.GetComponentInChildren<ItemUI>().item.ID;
    }

    public bool isNumMax(Item item)
    {
        return transform.GetComponentInChildren<ItemUI>().Amount < item.Capacity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0 && !SystemManager.Instance.PickUp)
        {
            SystemManager.Instance.Show(transform.GetChild(0).GetComponent<ItemUI>().item.GetItemInfo());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            SystemManager.Instance.Hide();
        }
    }

    private static Slot lastSlot = null;
    private static bool isController = false;
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && SystemManager.Instance.characterPanel.gameObject.activeSelf)
        {
            ItemUI itemui = transform.GetComponentInChildren<ItemUI>();
            if (itemui != null && !SystemManager.Instance.PickUp)
            {
                if (itemui.item is EquipmentItem || itemui.item is WeaponItem)
                {
                    Item currentItem = itemui.item;
                    DestroyImmediate(itemui.gameObject);
                    SystemManager.Instance.CharacterPutOn(currentItem);
                }
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (transform.childCount > 0)
        {
            ItemUI currentItemui = transform.GetComponentInChildren<ItemUI>();
            SystemManager.Instance.Hide();
            if (SystemManager.Instance.PickUp == false)
            {
                lastSlot = this;
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    isController = true;
                    int pickNum = (currentItemui.Amount + 1) / 2;
                    SystemManager.Instance.SetPickUpItem(currentItemui.item, pickNum);
                    int remanidNum = currentItemui.Amount - pickNum;
                    if (remanidNum == 0)
                    {
                        Destroy(currentItemui.gameObject);
                    }
                    else
                    {
                        currentItemui.SetItem(currentItemui.item, remanidNum);
                    }
                }
                else
                {
                    SystemManager.Instance.SetPickUpItem(currentItemui.item,currentItemui.Amount);
                    Destroy(currentItemui.gameObject);
                }
            }
            else
            {
                if (SystemManager.Instance.pickItem.item.ID == currentItemui.item.ID)
                {
                    if (currentItemui.Amount < currentItemui.item.Capacity)
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            isController = true;
                            currentItemui.AddAmount();
                            SystemManager.Instance.ReducePickItemAmount();
                        }
                        else
                        {
                            isController = false;
                            int remaindNum = currentItemui.item.Capacity - currentItemui.Amount;
                            int pickItemNum = SystemManager.Instance.pickItem.Amount;
                            if (pickItemNum > remaindNum)
                            {
                                SystemManager.Instance.ReducePickItemAmount(remaindNum);
                                currentItemui.AddAmount(remaindNum);
                            }
                            else
                            {
                                SystemManager.Instance.ReducePickItemAmount(pickItemNum);
                                currentItemui.AddAmount(pickItemNum);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (!isController)
                    {
                        for (int i = 0; i < currentItemui.Amount; i++)
                        {
                            lastSlot.StoryItem(currentItemui.item);
                        }
                        currentItemui.SetItem(SystemManager.Instance.pickItem.item, SystemManager.Instance.pickItem.Amount);
                        SystemManager.Instance.HidePickItem();
                    }
                }
            }
        }
        else
        {
            if (SystemManager.Instance.PickUp)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    isController = true;
                    this.StoryItem(SystemManager.Instance.pickItem.item);
                    SystemManager.Instance.ReducePickItemAmount(1);
                }
                else
                {
                    isController = false;
                    for (int i = 0; i < SystemManager.Instance.pickItem.Amount; i++)
                    {
                        this.StoryItem(SystemManager.Instance.pickItem.item);
                    }
                    SystemManager.Instance.ReducePickItemAmount(SystemManager.Instance.pickItem.Amount);
                }
            }
        }
    }
}