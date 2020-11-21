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

    public enum WorldState {
        Running,
        GameOver,
        LevelComplete
    }

    public WorldState state = WorldState.Running;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        worldCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WorldState.GameOver) {
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
}
