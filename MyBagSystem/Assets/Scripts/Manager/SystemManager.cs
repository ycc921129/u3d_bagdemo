using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour 
{
    private static SystemManager _instance = null;
    public static SystemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("SystemManager").GetComponent<SystemManager>();
            }
            return _instance;
        }
    }

    public InfoPanel infoPanel;
    public BagPanel bagPanel;
    public TotlePanel totlePanel;
    public ChestPanel chestPanel;
    public CharacterPanel characterPanel;
    public ShotPanel shotPanel;
    public ForgePanel forgePanel;

    public ItemUI pickItem;
    private Canvas canvas;
    private bool isPickup = false;
    public bool PickUp { get { return isPickup; } set { isPickup = value; } }

    private void Awake()
    {
        canvas = transform.parent.GetComponent<Canvas>();
        Coin = 500;
        HidePickItem();
        ParseItemJson(Application.streamingAssetsPath + "/document.json");
    }
    private void Update()
    {
        if (isPickup)
        {
            Vector2 localpositoin;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out localpositoin);
            pickItem.SetPosition(localpositoin);
            pickItem.Show();
        }
        if (isPickup && Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(-1) == false)
        {
            isPickup = true;
            HidePickItem();
        }
    }
  
    public void SetPanelActive(BasePanel panel)
    {
        if (panel!= null && panel.gameObject.activeSelf == true)
        {
            if (panel == chestPanel) 
            {
                characterPanel.Hide();
                shotPanel.Hide();
                forgePanel.Hide();
            }
            else if (panel == characterPanel)
            {
                chestPanel.Hide();
                shotPanel.Hide();
                forgePanel.Hide();
            }
            else if (panel == shotPanel)
            {
                chestPanel.Hide();
                characterPanel.Hide();
                forgePanel.Hide();
            }
            else if (panel == forgePanel)
            {
                chestPanel.Hide();
                characterPanel.Hide();
                shotPanel.Hide();
            }
        }
    }

    #region Player
    public Text coinText;
    private int coin = 100;
    public int Coin { get { return coin; } set { coin = value; SetCoin(); } }
    public void SetCoin()
    {
        coinText.text = Coin.ToString();
    }
    #endregion

    #region PickItem 
    public void HidePickItem()
    {
        pickItem.Hide();
        isPickup = false;
    } 
    public void SetPickUpItem(Item item,int amount)
    {
        isPickup = true;
        pickItem.SetItem(item, amount);
    }
    public void ReducePickItemAmount(int amount = 1)
    {
        pickItem.AddAmount(-amount);
        if (pickItem.Amount == 0)
        {
            isPickup = false;
            HidePickItem();
        }
    }
    #endregion

    #region InfoPanel
    public void SetInfo(string msg)
    {
        infoPanel.SetText(msg);
    }
    #endregion

    #region TotlePanel
    public void Show(string info)
    {
        totlePanel.Show(info);
    }
    public void Hide()
    {
        totlePanel.Hide();
    }
    #endregion

    #region bagPanel
    public void OpenBagPanel()
    {
        bagPanel.ShowAndHide();
    }
    public void BagPutOn(Item item)
    {        
        bagPanel.PutOn(item);
    }
    public void BagPutOn(int id)
    {
        bagPanel.PutOn(id);
    }
    #endregion

    #region ChestPanel
    public void OpenChestPanel()
    {
        chestPanel.ShowAndHide();
        SetPanelActive(chestPanel);
    }
    #endregion

    #region CharacterPanel
    public void OpenCharacterPanel()
    {
        characterPanel.ShowAndHide();
        SetPanelActive(characterPanel);
    }
    public void CharacterPutOn(Item item)
    {
        characterPanel.PutOn(item);
    }
    #endregion

    #region ForgePanel
    public void OpenForgePanel()
    {
        forgePanel.ShowAndHide();
        SetPanelActive(forgePanel);
    }
    #endregion

    #region ShotPanel
    public void OpenShotPanel()
    {
        shotPanel.ShowAndHide();
        SetPanelActive(shotPanel);
    }
    #endregion

    #region Save and load info
    public void SaveInfo()
    {
        bagPanel.SaveInfo();
        chestPanel.SaveInfo();
        characterPanel.SaveInfo();
        forgePanel.SaveInfo();
        PlayerPrefs.SetString(gameObject.name, coin.ToString());
    }
    public void LoadInfo()
    {
        bagPanel.LoadInfo();
        chestPanel.LoadInfo();
        characterPanel.LoadInfo();
        forgePanel.LoadInfo();
        Coin = PlayerPrefs.GetString(gameObject.name) == string.Empty ? 500 : int.Parse(PlayerPrefs.GetString(gameObject.name));
    }
    #endregion

    #region Item
    private List<Item> ItemList = new List<Item>();
    private void ParseItemJson(string jsonPath)
    {
        if (File.Exists(jsonPath))
        {
            StreamReader reader = new StreamReader(jsonPath);
            string data = reader.ReadToEnd();
            reader.Close();
            JsonData jsondata = JsonMapper.ToObject(data);            

            foreach (JsonData jdata in jsondata)
            {
                int id = int.Parse(jdata["id"].ToString());
                string name = jdata["name"].ToString();
                string description = jdata["description"].ToString();
                int capacity = int.Parse(jdata["capacity"].ToString());
                int buyprice = int.Parse(jdata["buyprice"].ToString());
                int sellprice = int.Parse(jdata["sellprice"].ToString());
                string spritePath = jdata["spritePath"].ToString();
                Item.ItemType itemType = (Item.ItemType)(Enum.Parse(typeof(Item.ItemType), jdata["type"].ToString()));
                Item.QualityType qualityType = (Item.QualityType)(Enum.Parse(typeof(Item.QualityType), jdata["quality"].ToString()));
                Item item = null;
                switch (itemType)
                {
                    case Item.ItemType.ConsumableItem:
                        {
                            int hp = int.Parse(jdata["hp"].ToString());
                            int mp = int.Parse(jdata["hp"].ToString());
                            item = new ConsumableItem(id, name, itemType, qualityType, description, capacity, buyprice, sellprice, spritePath, hp, mp);
                            break;
                        }
                    case Item.ItemType.EquipmentItem:
                        {
                            int strength = int.Parse(jdata["strength"].ToString());
                            int intellect = int.Parse(jdata["intellect"].ToString());
                            int agility = int.Parse(jdata["agility"].ToString());
                            int statmina = int.Parse(jdata["statmina"].ToString());
                            EquipmentItem.EquitType equittype = (EquipmentItem.EquitType)(Enum.Parse(typeof(EquipmentItem.EquitType), jdata["equittype"].ToString()));
                            item = new EquipmentItem(id, name, itemType, qualityType, description, capacity,
                                buyprice, sellprice, spritePath, strength, intellect, agility, statmina, equittype);
                            break;
                        }
                    case Item.ItemType.WeaponItem:
                        {
                            int damage = int.Parse(jdata["damage"].ToString());
                            WeaponItem.WeaponType weaponType = (WeaponItem.WeaponType)(Enum.Parse(typeof(WeaponItem.WeaponType), jdata["weapontype"].ToString()));
                            item = new WeaponItem(id, name, itemType, qualityType, description, capacity,buyprice, sellprice, spritePath, damage, weaponType);
                            break;
                        }
                    case Item.ItemType.MaterialItem:
                        item = new MaterialItem(id, name, itemType, qualityType, description, capacity, buyprice, sellprice, spritePath);
                        break;
                    default: break;
                }
                ItemList.Add(item);
            }
        }
    }
    public Item GetItem(int id)
    {
        foreach (Item item in ItemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }
    #endregion
}