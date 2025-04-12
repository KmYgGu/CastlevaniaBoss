using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideKick : MonoBehaviour
{
    public static bool Shork;//킥에 맞았다
    public static int jumpDamage = 1;//점프공격 데미지
    // Start is called before the first frame update
    void Start()
    {
        Shork = false;
        jumpDamage = 1;
    }

    // Update is called once per frame
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            Marx.BossHealth -= jumpDamage;
            Shork = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            jumpDamage = jumpDamage * 2;
        }
    }
}
