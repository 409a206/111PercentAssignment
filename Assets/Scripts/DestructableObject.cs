using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    //component variables
    private DestructableObject _destructableObject;
    //시스템 조정 데이터
    [SerializeField]
    private int hp = 3;
    
    void Start()
    {
        _destructableObject = this.GetComponent<DestructableObject>();
    }

    public void TakeDamage(int damage) {
        Debug.Log("TakeDamage called");
        hp -= damage;
        if(hp <= 0) {
            GetDestroyed();
        }
    }

    private void GetDestroyed() {
        Destroy(this.gameObject);
    }

}
