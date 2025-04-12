using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public float Speed = 0.3f;//수정가능하게 바꿈
    int go;//Key값 받아오는 함수
    int back;//되돌아 오기
    //float Rot;//회전 값
    //SpriteRenderer ods;//?
    // Start is called before the first frame update
    void Start()
    {
        //ods = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(PlayerControl.key, 1, 1);//키가 1이면 왼쪽에서 오른쪽 -1이면 오른쪽에서 왼쪽
        go = PlayerControl.key;
        back = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Rot += 50;
        //ods.transform.rotation = Quaternion.Euler(0, 0, (-Rot) * go);//이상하지만 상관없겟지
        back++;
        if(back < 35)transform.Translate(Speed * go, 0, 0);//앞으로 가다가
        if (45 > back &&back >= 35) transform.Translate(0, 0, 0);//잠깐 멈춤
        if (back >= 45) transform.Translate(Speed * -go, 0, 0);//뒤로감
        if (gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Marx.BossHealth -= 40;//40데미지줌
        }
        if (collision.gameObject.tag == "LV")
        {
            PlayerControl.Weapon += 2;
            Destroy(gameObject);
        }
    }
}
