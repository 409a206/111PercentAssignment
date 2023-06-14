using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //메인 카메라
    public Camera mainCamera;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        TryAddBackground();
    }

    private void TryAddBackground()
    {
       
    }
}
