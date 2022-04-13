using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour 
{
    public Item item { get; set; }
    public int Amount { get; set; }
    private Image image;
    public Image Image { get { if (image == null) { image = GetComponent<Image>(); } return image; } }
    private Text amountText;
    public Text AmountText { get { if (amountText == null) { amountText = GetComponentInChildren<Text>(); } return amountText; } }

    private float targetScale = 1;
    private float speed = 5.0f;
    private float animaScale = 1.3f;

    private void Update()
    {
        if (animaScale != targetScale)
        {
            animaScale = Mathf.Lerp(animaScale, targetScale, Time.deltaTime * speed);
            transform.localScale = new Vector3(animaScale, animaScale, animaScale);
            if (Mathf.Abs(animaScale - targetScale) < 0.02f)
            {
                transform.localScale = Vector3.one;
                animaScale = targetScale;
            }
        }
    }

    public void SetItem(Item item,int amount = 1)
    {
        animaScale = 1.3f;
        transform.localScale = animaScale * Vector3.one;
        this.item = item;
        Amount = amount;
        Image.sprite = Resources.Load<Sprite>(item.SpritePath);
        if (Amount == 1)
        {
            AmountText.text = string.Empty;
        }
        else
        {
            AmountText.text = Amount.ToString();
        }
    }
    public void AddAmount(int amount = 1)
    {
        animaScale = 1.3f;
        transform.localScale = animaScale * Vector3.one;
        Amount += amount;
        AmountText.text = Amount.ToString();       
    }

    public void ExchangeItem(ItemUI itemui)
    {
        animaScale = 1.3f;
        transform.localScale = animaScale * Vector3.one;
        Item tempItem = itemui.item;
        int tempAmount = itemui.Amount;
        itemui.item = item;
        itemui.Amount = Amount;
        this.item = tempItem;
        this.Amount = tempAmount;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}