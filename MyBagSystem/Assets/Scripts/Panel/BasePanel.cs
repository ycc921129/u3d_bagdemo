using UnityEngine;
using System.Text;

public class BasePanel : MonoBehaviour 
{
    protected Slot[] slots;
    protected SystemManager systemManager;
    protected CanvasGroup canvasGroup;
    protected virtual void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        systemManager = SystemManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void ShowAndHide()
    {
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public bool StoryItem(int id)
    {
        Item item = systemManager.GetItem(id);
        return StoryItem(item);
    }
    public bool StoryItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("item is null");
            return false;
        }
        Slot slot = null;
        if (item.Capacity == 1) //比如装备 一个物品槽只能放一个
        {
            slot = FindSlot(item);
            if (slot == null)
            {
                systemManager.SetInfo("背包已经满了");
                return false;
            }
            else
            {
                slot.StoryItem(item);
            }
        }
        else
        {
            slot = FindSameSlot(item);
            if (slot == null)
            {
                slot = FindSlot(item);
                if (slot == null)
                {
                    systemManager.SetInfo("背包已经满了");
                    return false;
                }
                else
                {
                    slot.StoryItem(item);
                }
            }
            else
            {
                slot.StoryItem(item);
            }
        }
        return true;
    }

    protected Slot FindSlot(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    protected Slot FindSameSlot(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemID() == item.ID && slot.isNumMax(item))
            {
                return slot;
            }
        }
        return null;
    }

    #region Save adn Load
    public void SaveInfo()
    {
        StringBuilder strs = new StringBuilder();
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemui = slot.GetComponentInChildren<ItemUI>();
                strs.Append(itemui.item.ID.ToString()+","+itemui.Amount.ToString() + "_");
            }
            else
            {
                strs.Append("0");
            }
        }
        PlayerPrefs.SetString(gameObject.name, strs.ToString());
    }

    public void LoadInfo()
    {
        string str = PlayerPrefs.GetString(gameObject.name);
        string[] infos = str.Split('_');
        for (int i = 0; i < infos.Length - 1; i++)
        {
            string[] iteminfo = infos[i].Split(',');
            int id = int.Parse(iteminfo[0]);
            int amount = int.Parse(iteminfo[1]);
            for (int j = 0; j < amount; j++)
            {
                StoryItem(id);
            }
        }
    }
    #endregion
}