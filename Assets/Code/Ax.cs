using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    int go;//Key값 받아오는 함수
    Rigidbody2D AxBody;
    SpriteRenderer ods;
    float Rot;//회전 값
    // Start is called before the first frame update
    void Start()
    {
        AxBody = GetComponent<Rigidbody2D>();
        ods = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(PlayerControl.key, 1, 1);//키가 1이면 왼쪽에서 오른쪽 -1이면 오른쪽에서 왼쪽
        go = PlayerControl.key;
        AxBody.AddForce(new Vector3(go * 10, 30, 0), ForceMode2D.Impulse);//도끼 투척
    }

    // Update is called once per frame
    void Update()
    {
        Rot += 10;// * Time.deltaTime;
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        ods.transform.rotation = Quaternion.Euler(0, 0, (-Rot)* go);//이상하지만 상관없겟지
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            AxBody.AddForce(new Vector3(go * 10, 50, 0),ForceMode2D.Impulse);//도끼 투척
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Marx.BossHealth -= 80;//80데미지줌
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
