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
    private float maxDist = 1f;

    private bool canDrag = true;

    //드래그하고 있는 거리
    private float dragDist = 0f;
    private Vector2 mouseDragStartPos = Vector2.zero;
    private Vector2 mouseDragEndPos = Vector2.zero;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
       
        _startPos = _rectTransform.localPosition;    
    }

    // Update is called once per frame
    void Update()
    {}

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        mouseDragStartPos = Input.mousePosition;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(canDrag) {
            dragDist = Input.mousePosition.y - mouseDragStartPos.y;
            //Debug.Log(dragDist);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        this.transform.localPosition = _startPos;
        mouseDragEndPos = Input.mousePosition;
    }

}
