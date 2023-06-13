using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    //component variables
    private Camera _cam;

    //reference variables
    [SerializeField]
    private GameObject target;
    public float speed = 2.0f;
    
    //카메라가 가질 수 있는 최대 x와 y좌표
    private Vector2 maxXAndY;

    //카메라가 가질 수 있는 최소 x와 y좌표
    private Vector2 minXAndY;


    private void Awake() {
        
        _cam = GetComponent<Camera>();

        var backgroundBounds = 
            GameObject.Find("Background").GetComponent<Renderer>().bounds;

        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();   
    }

    private void FollowPlayer()
    {
        float Interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;

        position.y = Mathf.Lerp(transform.position.y, target.transform.position.y, Interpolation);
        
        transform.position = position;
    }
}
