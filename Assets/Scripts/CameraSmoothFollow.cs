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

    //derived variables

    //카메라가 가질 수 있는 최소 y좌표
    private float minY;


    private void Awake() {
        
        _cam = GetComponent<Camera>();

        var backgroundBounds = 
            GameObject.Find("Background").GetComponent<Renderer>().bounds;

        //월드 좌표계에서 카메라가 볼 수 있는 경계를 얻음
        var camTopLeft = _cam.ViewportToWorldPoint(new Vector3(0,0,0));
        var camBottomRight = _cam.ViewportToWorldPoint(new Vector3(1,1,0));
        // Debug.Log("camTopLeft: " + camTopLeft);
        // Debug.Log("camBottomRight: " + camBottomRight);

        //자동으로 max값 설정
        minY = backgroundBounds.max.y - camBottomRight.y;
        Debug.Log("minY: " + minY);
       
        target = GameObject.Find("Player");

        if(target == null) {
            Debug.LogError("Player object not found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();   
    }

    private void FollowPlayer()
    {
       
        float Interpolation = speed * Time.fixedDeltaTime;

        Vector3 pos = transform.position;

        pos.y = Mathf.Lerp(transform.position.y, target.transform.position.y, Interpolation);

        if(pos.y <= minY) {
            pos.y = minY;
        }
       
        this.transform.position = pos;
    }
}
