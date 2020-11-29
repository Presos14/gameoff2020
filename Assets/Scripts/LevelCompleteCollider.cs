using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        WorldController.instance.levelComplete("Mission 1 accomplished! Press to continue...");
    }
}
