using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableButton : EventTrigger
{
    private RectTransform _rectTransform;
   
    //입력이 시작되는 좌표
    private Vector3 _startPos = Vector3.zero;

    //위로 드래그할 수 있는 최대값
    [SerializeField]
    private float maxDist = 500f;

    private bool canDrag = true;

    //드래그하고 있는 거리
    private float dragDist = 0f;
    private Vector2 mouseDragStartPos = Vector2.zero;
    private Vector2 mouseDragEndPos = Vector2.zero;
    private bool _buttonPressed = false;

    // Start is called before the first frame update
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
       
        _startPos = _rectTransform.localPosition;
        //Debug.Log("_startPos: " +_startPos);    
    }

    // Update is called once per frame
    private void FixedUpdate() {
        
        //모바일에서는 터치패드 방식으로 여러 터치 입력을 받아 처리합니다.
        
        HandleTouchInput();
        //모바일이 아닌PC나 유니티 에디터상에서 작동할 때는 터치 입력이 아닌 마우스로 입력받는다.
        
        #if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
            //Debug.Log("UnityEditor!");
            HandleInput(Input.mousePosition);

        #endif
    }

    
    private void HandleTouchInput()
    {
        //throw new NotImplementedException();
    }

    private void HandleInput(Vector3 mousePosition)
    {
        if(_buttonPressed && canDrag) {
            
            dragDist = (mousePosition.y - mouseDragStartPos.y) * 3f;

            _rectTransform.localPosition = new Vector3(_startPos.x, 
                                                    _startPos.y + Mathf.Clamp(dragDist, 0, maxDist), 
                                                    _startPos.z);
        }
    }
    
    public override void OnPointerDown(PointerEventData eventData) {
        _buttonPressed = true;
        mouseDragStartPos = Input.mousePosition;
    }
    public override void OnPointerUp(PointerEventData eventData) {
        _buttonPressed = false;

        //버튼이 떼어졌을 때 터치패드와 촤표를 원래 지점으로 복귀시킵니다.
        //HandleInput(_startPos);
        _rectTransform.localPosition = _startPos;
        mouseDragEndPos = Input.mousePosition;
    }

/*     public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        mouseDragStartPos = Input.mousePosition;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(canDrag) {
            dragDist = (Input.mousePosition.y - mouseDragStartPos.y) * 3f;

            _rectTransform.localPosition = new Vector3(_startPos.x, 
                                                    _startPos.y + Mathf.Clamp(dragDist, 0, maxDist), 
                                                    _startPos.z);

        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        _rectTransform.localPosition = _startPos;
        mouseDragEndPos = Input.mousePosition;
    } */

}
