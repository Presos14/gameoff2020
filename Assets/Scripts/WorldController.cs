using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    public static WorldController instance { get; private set; }

    public GameObject worldCamera;
    public GameObject rocketCamera;

    public enum WorldState {
        Running,
        GameOver
    }

    public WorldState state = WorldState.Running;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        worldCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WorldState.GameOver) {
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
