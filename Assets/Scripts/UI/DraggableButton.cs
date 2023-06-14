using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableButton : EventTrigger
{
    private RectTransform _rectTransform;
    //터치 입력 중에 방향 컨트롤러의 영역 안에 있는 입력을 구분하기 위한 아이디
    private int _touchId = -1;

   
    //입력이 시작되는 좌표
    private Vector3 _startPos = Vector3.zero;

    //위로 드래그할 수 있는 최대값
    [SerializeField]
    private float maxDist = 500f;

    protected bool canDrag = false;

    //드래그하고 있는 거리
    private float dragDist = 0f;
    private Vector2 mouseDragStartPos = Vector2.zero;
    private Vector2 mouseDragEndPos = Vector2.zero;
    private bool _buttonPressed = false;

    protected UIController uIController;

    // Start is called before the first frame update
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        uIController = GameObject.FindObjectOfType<UIController>();
       
        _startPos = _rectTransform.localPosition;
        //Debug.Log("_startPos: " +_startPos);    
    }

    // Update is called once per frame
    private void FixedUpdate() {
        
        //모바일에서는 터치패드 방식으로 여러 터치 입력을 받아 처리합니다.
        
        //HandleTouchInput();
        //모바일이 아닌PC나 유니티 에디터상에서 작동할 때는 터치 입력이 아닌 마우스로 입력받는다.
        
        //#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
            //Debug.Log("UnityEditor!");
            HandleInput(Input.mousePosition);

        //#endif
    }

    
    /* private void HandleTouchInput()
    {
        //터치 아이디(touchId)를 매기기 위한 번호입니다.
        int i = 0;
        //터치 입력은 한 번에 여러 개가 들어올 수 있습니다. 터치가 하나 이상 입력되면 실행되도록 합니다.
        if(Input.touchCount > 0) {
             //각각의 터치 입력을 하나씩 조회합니다.
            foreach (Touch touch in Input.touches) {
                //터치 아이디(touchId)를 매기기 위한 번호를 1 증가시킵니다.
                i++;

                //현재 터치 입력의 x,y 좌표를 구합니다.
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                //터치 입력이 방금 시작되었다면, 혹은 TouchPhase.Began이면,
                if(touch.phase == TouchPhase.Began) {
                    //그리고 터치의 좌표가 현재 방향키 범위 내에 있다면
                    if(touch.position.x <= (_startPos.x + _dragRadius)) {
                        //이 터치 아이디를 기준으로 방향 컨트롤러를 조작하도록 합니다.
                        _touchId = i;
                    }
                }

                //터치 입력이 움직였다거나, 가만히 있는 상황이라면
                if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                    //터치 아이디로 지정된 경우에만
                    if(_touchId == i) {
                        //좌표 입력을 받아들입니다.
                        HandleInput(touchPos);
                    }
                }
                //터치 입력이 끝났는데
                if(touch.phase == TouchPhase.Ended) {
                    //입력받고자 했던 터치 아이디라면
                    if(_touchId == i) {
                        //터치 아이디를 해제합니다.
                        _touchId = -1;
                    }
                }
            }


        }
    } */

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

#region legacy
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
#endregion
}
