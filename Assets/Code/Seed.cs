using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public GameObject Steam;

    int a;//반복 설정하는 정수
    float b;//줄기 길이 증가 하는 정수
    float c;//줄기 X값 랜덤

    // Start is called before the first frame update
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")//땅에 닿으면
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            a = 0;
            b = 0;
            StartCoroutine(Bloom2());
        }
        if (collision.gameObject.tag == "LV")//플레이어에게 닿으면
        {
            PlayerControl.Health -= 55;//35데미지
            Destroy(gameObject);
        }
    }

    IEnumerator Bloom2()
    {
        yield return new WaitForSeconds(6f);//씨앗이피는시간
        while (a < 7)//길이설정
        {
            c = Random.Range(-0.5f, 0.5f);
            Instantiate(Steam, new Vector2(gameObject.transform.position.x+c, -4.5f+ b), Quaternion.identity);//gameObject.transform.position.y 
            a++;
            b += 1.8f;//간격설정
            yield return new WaitForSeconds(0.1f);//솟아오르는 속도
        }
        Destroy(gameObject);
    }
}
