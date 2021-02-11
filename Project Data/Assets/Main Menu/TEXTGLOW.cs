using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TEXTGLOW : MonoBehaviour
{
    public TMP_FontAsset Glow;
    public TMP_FontAsset Normal;

    public TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        tmp.font = Normal;
    }

    private void OnDisable()
    {
        tmp.font = Normal;
    }

    public void PointerEnter()
    {
        tmp.font = Glow;
    }

    public void PointerExit()
    {
        tmp.font = Normal;
    }
}
