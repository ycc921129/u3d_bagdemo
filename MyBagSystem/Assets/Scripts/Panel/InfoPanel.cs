using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : BasePanel 
{
    public Text text;

    protected override void Start()
    {
        base.Start();
    }
    public void SetText(string msg)
    {
        text.text = msg;
        StartCoroutine(HideText());
    }
    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2.0f);
        text.text = string.Empty;
    }
}