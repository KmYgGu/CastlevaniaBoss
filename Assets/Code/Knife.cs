using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float Speed = 1;//수정가능하게 바꿈
    int go;//Key값 받아오는 함수
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(PlayerControl.key, 1, 1);//키가 1이면 왼쪽에서 오른쪽 -1이면 오른쪽에서 왼쪽
        go = PlayerControl.key;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed * go, 0, 0);//*Time.deltaTime
        if (gameObject.transform.position.x > 20 || gameObject.transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            Marx.BossHealth -= 10;//10데미지줌
        }
    }
}
