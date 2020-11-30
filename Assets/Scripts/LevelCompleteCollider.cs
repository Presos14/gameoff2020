using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCollider : MonoBehaviour
{
    public static LevelCompleteCollider instance = null;
    public void Awake()
    {
        instance = this;
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        WorldController.instance.levelComplete();
    }
}
