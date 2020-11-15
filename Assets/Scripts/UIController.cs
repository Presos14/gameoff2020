using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }
    public Image bar;
    public TMP_Text gameOverText;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = bar.rectTransform.rect.width;
    }

    public void SetFuelBarValue(float value)
    {				      
        bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void showGameOverText(string message) {
        gameOverText.text = message;
        gameOverText.gameObject.SetActive(true);
    }
}
