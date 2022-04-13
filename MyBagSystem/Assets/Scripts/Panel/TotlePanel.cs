using UnityEngine;
using UnityEngine.UI;

public class TotlePanel : BasePanel 
{
    public Text ShowPanel;
    public Text ShowText;
    public CanvasGroup canvasGroup;
    private float smothSpeed = 6.6f;
    private float targetAlpha = 0;
    private bool isShow = false;
    private Canvas canvas;

    protected override void Start()
    {
        base.Start();
        canvas = transform.parent.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smothSpeed * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
        if (isShow)
        {
            Vector2 localposition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out localposition);
            transform.localPosition = localposition + new Vector2(20, -20);
        }
    }
    public void Show(string info)
    {
        ShowPanel.text = info;
        ShowText.text = info;
        isShow = true;
        targetAlpha = 1;
    }

    public void Hide()
    {
        isShow = false;
        targetAlpha = 0;
    }
}