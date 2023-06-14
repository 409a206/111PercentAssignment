using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //메인 카메라
    public Camera mainCamera;
    public PlayerController playerController;
    private GameObject backgroundPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        // mainCamera = GameObject.FindObjectOfType<Camera>();
        // playerController = GameObject.FindObjectOfType<PlayerController>();
        // backgroundPrefab = Resources.Load("Prefabs/Background") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //TryAddAndDeleteBackground();
    }

    private void TryAddAndDeleteBackground()
    {
       var camTopLeft = mainCamera.ViewportToWorldPoint(new Vector3(0,0,0));

       //Physics2D.OverlapBox(camTopLeft ,new Vector2(0,0.1f,0), )
       //if(camTopLeft)
    }
}
