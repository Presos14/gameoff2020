using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public string levelName;
    public Button PlayButton;
    public Button CreditsButton;
    public Button ExitButton;
    public TextMeshProUGUI CreditsButtonText;

    public GameObject CreditsPanel;


    public void Awake()
    {
       PlayButton.onClick.AddListener(PlayGame);
       ExitButton.onClick.AddListener(ExitGame);
    }

    public void OnDestroy()
    {
        PlayButton.onClick.RemoveAllListeners();
    }

    public void OnEnable()
    {
        HideCredits();
    }

    public void OnDisable()
    {
        CreditsButton.onClick.RemoveAllListeners();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    public void ShowCredits()
    {
        CreditsButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.AddListener(HideCredits);
        CreditsButtonText.text = "Back";
        PlayButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        CreditsPanel.gameObject.SetActive(true);
    }

    public void HideCredits()
    {
        CreditsButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.AddListener(ShowCredits);
        CreditsButtonText.text = "Credits";
        PlayButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        CreditsPanel.gameObject.SetActive(false);
    }
}
