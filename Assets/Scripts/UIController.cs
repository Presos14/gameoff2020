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

    private float textMaxTime = 0;
    private float textElapsedTime = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = bar.rectTransform.rect.width;
    }

    void Update() {
        if (textMaxTime > 0) {
            textElapsedTime += Time.deltaTime;
            if (textElapsedTime >= textMaxTime) {
                gameOverText.gameObject.SetActive(false);
                textElapsedTime = 0;
            }
        }
    }

    public void SetFuelBarValue(float value)
    {				      
        bar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void showText(string message) {
        gameOverText.text = message;
        gameOverText.gameObject.SetActive(true);
    }

    public void showText(string message, float time) {
        gameOverText.text = message;
        gameOverText.gameObject.SetActive(true);
        textMaxTime = time;
        textElapsedTime = 0;
    }

    public void hideText() {
        gameOverText.text = "";
        gameOverText.gameObject.SetActive(false);
    }
}
