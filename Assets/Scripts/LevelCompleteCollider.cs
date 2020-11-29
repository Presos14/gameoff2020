using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        WorldController.instance.levelComplete("Mission accomplished! Press to continue...");
    }
}
