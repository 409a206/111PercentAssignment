using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    //시스템 조정 데이터
    [SerializeField]
    private int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeDamage(int damage) {
        hp -= damage;
        if(hp <= 0) {
            GetDestroyed();
        }
    }

    private void GetDestroyed() {
        Destroy(this.gameObject);
    }
}
