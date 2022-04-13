using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ForgePanel : BasePanel 
{
    private List<ForgeData> foregeList;
    protected override void Start()
    {
        base.Start();
        PauseJson();
    }
    private void PauseJson()
    {
        foregeList = new List<ForgeData>();
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/Forgedata.json.txt"));
        foreach (JsonData data in jsonData)
        {
            int Item1ID = int.Parse(data["Item1ID"].ToString());
            int Item1Amount = int.Parse(data["Item1Amount"].ToString());
            int Item2ID = int.Parse(data["Item2ID"].ToString());
            int Item2Amount = int.Parse(data["Item2Amount"].ToString());
            int ItemForgeResult = int.Parse(data["ItemForgeResult"].ToString());
            ForgeData forgeData = new ForgeData(Item1ID, Item1Amount, Item2ID, Item2Amount, ItemForgeResult);
            foregeList.Add(forgeData);
        }
    }

    public void OnclickBtForgeItem()
    {
        List<int> itemList = new List<int>();
        foreach (Slot slot in slots)
        {
            ItemUI itemui = slot.GetComponentInChildren<ItemUI>();
            if (itemui != null && !systemManager.PickUp)
            {
                for (int i = 0; i < itemui.Amount; i++)
                {
                    itemList.Add(itemui.item.ID);
                }
            }
        }
        foreach (ForgeData forge in foregeList)
        {
            if (forge.IsForgeResult(itemList))
            {
                systemManager.BagPutOn(forge.ItemForgeResult);
                RemoveAmount(forge.needForgeList);
                break;
            }
        }
    }
    private void RemoveAmount(List<int> needList)
    {
        foreach (int needid in needList)
        {
            foreach (Slot slot in slots)
            {
                ItemUI itemui = slot.GetComponentInChildren<ItemUI>();
                if (itemui != null && itemui.item.ID == needid)
                {
                    if (itemui.Amount > 0)
                    {
                        itemui.AddAmount(-1);
                    }
                    if (itemui.Amount <= 0)
                    {
                        DestroyImmediate(itemui.gameObject);
                    }
                }
            }
        }
    }
}

public class ForgeData
{
    public int Item1ID { get; set; }
    public int Item1Amount { get; set; }
    public int Item2ID { get; set; }
    public int Item2Amount { get; set; }
    public int ItemForgeResult { get; set; }

    public List<int> needForgeList = new List<int>();

    public ForgeData(int id1,int amount1,int id2,int amount2,int forgeResult)
    {
        Item1ID = id1;
        Item1Amount = amount1;
        Item2ID = id2;
        Item2Amount = amount2;
        ItemForgeResult = forgeResult;
        InitForgeList();
    }
    private void InitForgeList()
    {
        for (int i = 0; i < Item1Amount; i++)
        {
            needForgeList.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            needForgeList.Add(Item2ID);
        }
    }

    public bool IsForgeResult(List<int> itemList)
    {
        List<int> tempList = new List<int>(itemList);
        foreach (int id in needForgeList)
        {
            bool isResult = tempList.Remove(id);
            if (isResult == false)
            {
                return false;
            }
        }
        return true;
    }
}