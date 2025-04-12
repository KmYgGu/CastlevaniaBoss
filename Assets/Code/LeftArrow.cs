using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArrow : MonoBehaviour
{
    public float Speed = 1;//수정가능하게 바꿈
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()//왼쪽에서 오른쪽으로 이동
    {
        transform.Translate(Speed, 0, 0);//*Time.deltaTime
        if(gameObject.transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LV")//플레이어에게 닿으면
        {
            PlayerControl.Health -= 3;//3데미지
        }
        if (collision.gameObject.tag == "Pattack2" || collision.gameObject.tag == "Pattack1" || collision.gameObject.tag == "SW")//공격에 닿으면
        {
            Destroy(gameObject);
        }
    }
}
