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
    private UIController uIController;

    //위로 드래그할 수 있는 최대값
    [SerializeField]
    private float maxDist = 100f;

    private bool canDrag = false;
    

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _startPos = _rectTransform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(canDrag) {
            this.transform.position = new Vector3(transform.position.x,
                                                Mathf.Clamp(Input.mousePosition.y, _startPos.y , _startPos.y + maxDist), 
                                                transform.position.z);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        this.transform.position = _startPos;
    }

}
