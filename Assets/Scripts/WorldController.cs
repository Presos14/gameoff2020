using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class WorldController : MonoBehaviour
{
    public static WorldController instance { get; private set; }

    public CinemachineVirtualCamera worldCamera;
    public CinemachineVirtualCamera rocketCamera;
    public CinemachineVirtualCamera centerCamera;

    private int currentLevel = 1;

    public CinemachineVirtualCamera[] additionalCameras;
    private int currentCamFocus;

    public enum WorldState {
        Init,
        Running,
        GameOver,
        LevelComplete
    }

    private WorldState state = WorldState.Init;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        showInitialText();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WorldState.Init) {
            if (Input.anyKeyDown) {
                worldCamera.gameObject.SetActive(false);
                UIController.instance.hideText();
                Time.timeScale = 1;
                state = WorldState.Running;
            }
        } else if (state == WorldState.GameOver) {
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else if (state == WorldState.LevelComplete) {
            if (Input.anyKeyDown) {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void gameOver(string message) {
        UIController.instance.showText(message);
        state = WorldState.GameOver;
        centerCamera.gameObject.SetActive(true);
    }

    public void levelComplete(string message) {
        Time.timeScale = 0;
        UIController.instance.showText(message);
        state = WorldState.LevelComplete;
    }

    public void checkCameraNeedsUpdate(Vector2 position) {
        switch (currentLevel) {
            case 1:
                if (currentCamFocus == 0 && position.x < -20) {
                    currentCamFocus = 1;
                    rocketCamera.gameObject.SetActive(false);
                    additionalCameras[0].gameObject.SetActive(true);
                }
                break;
        }
    }

    public void showInitialText() {
        switch (currentLevel) {
            case 1:
                UIController.instance.showText("To Mars around the Sun!\nPress to start.");
                break;
        }
    }

    public WorldState getStatus() {
        return state;
    }
}
