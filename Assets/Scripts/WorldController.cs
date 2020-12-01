using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class WorldController : MonoBehaviour
{
    const int N_LEVELS = 4;
    public static WorldController instance { get; private set; }

    public CinemachineVirtualCamera worldCamera;
    public CinemachineVirtualCamera rocketCamera;
    public CinemachineVirtualCamera centerCamera;

    private int currentLevel;

    public CinemachineVirtualCamera[] additionalCameras;
    private int currentCamFocus;

    bool holdingDown;

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
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current level: " + currentLevel);
        showInitialText();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WorldState.Init) {
            if (Input.anyKey) {
                holdingDown = true;
            }
            if (!Input.anyKey && holdingDown) {
                holdingDown = false;
                worldCamera.gameObject.SetActive(false);
                UIController.instance.hideText();
                Time.timeScale = 1;
                state = WorldState.Running;
            }
        } else if (state == WorldState.GameOver) {
            if (Input.anyKeyDown) {
                MusicController.instance.StopAudio();
                Debug.Log(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else if (state == WorldState.LevelComplete) {
            if (Input.anyKeyDown) {
                MusicController.instance.StopAudio();
                Time.timeScale = 1f;
                int nextScene = currentLevel + 1;
                if (nextScene <= N_LEVELS) {
                    SceneManager.LoadScene(nextScene);
                } else {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    public void gameOver(string message) {
        UIController.instance.showText(message);
        MusicController.instance.PlayMissionFailed();
        state = WorldState.GameOver;
        centerCamera.gameObject.SetActive(true);
    }

    public void levelComplete() {
        Time.timeScale = 0;
        string message = "";
        switch (currentLevel) {
            case N_LEVELS:
                message = "Congratulations!! Monkey successfully delivered.\nThanks for playing!";
                break;
            default:
                message = "Mission accomplished! Press to continue...";
                break;
        }
        UIController.instance.showText(message);
        MusicController.instance.PlayMissionPassed();
        state = WorldState.LevelComplete;
    }

    public void checkCameraNeedsUpdate(Vector2 position) {
        switch (currentLevel) {
            case 2:
                if (currentCamFocus == 0 && position.x < -20) {
                    currentCamFocus = 1;
                    rocketCamera.gameObject.SetActive(false);
                    additionalCameras[0].gameObject.SetActive(true);
                }
                break;
            case 3:
                if (currentCamFocus == 0 && position.x > 20) {
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
                UIController.instance.showText("Controls:\n[Space] for take off\nLeft/Right arrows (or A/D keys) to adjust direction...\n\nTo the moon!\nPress to start");
                break;
            case 2:
                UIController.instance.showText("To Mars around the Sun!\nPress to start");
                break;
            case 3:
                UIController.instance.showText("Things are getting weird... Get to the blue planet!\nPro tip: do not think much, go straight to the goal!\nPress to start");
                break;
            case 4:
                UIController.instance.showText("Deliver the monkey... Towards the black hole!\nPress to start");
                break;
        }
    }

    public WorldState getState() {
        return state;
    }
}
