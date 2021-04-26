using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Play("BackgroundGame");
        Time.timeScale = 0.0f;
    }
        
    void Update()
    {
        
    }
}