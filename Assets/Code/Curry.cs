using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curry : MonoBehaviour
{
    Rigidbody2D CBody;
    int go;
    // Start is called before the first frame update
    void Start()
    {
        CBody = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(PlayerControl.key, 1, 1);//키가 1이면 왼쪽에서 오른쪽 -1이면 오른쪽에서 왼쪽
        go = PlayerControl.key;
        CBody.AddForce(new Vector3(go * 20, -20, 0), ForceMode2D.Impulse);//카레 투척
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CBody.velocity = Vector3.zero;
            Invoke("Deleteit", 1.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            Marx.BossHealth -= 3;//3데미지줌
        }
    }

    void Deleteit()
    {
        Destroy(gameObject);
    }

}
