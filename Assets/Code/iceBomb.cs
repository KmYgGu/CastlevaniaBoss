using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceBomb : MonoBehaviour
{
    public GameObject RightAttack;
    public GameObject LeftAttack;
    GameObject Target;
    Animator Bomb;
    public bool Clean = false;//폭팔 후 정리
    public int Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("LV");
        Bomb = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Round(gameObject.transform.position.y) == Mathf.Round(Target.transform.position.y)&& !Clean)
        {
            //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Bomb.SetTrigger("DisBoom");
            RightAttack.SetActive(true);
            LeftAttack.SetActive(true);
            Clean = true;
            Invoke("Cleanup", 1f);
        }
        if(!Clean)
        {
            transform.Translate(0, -0.5f, 0);
        }
        if(gameObject.transform.position.y <= -4)
        {
            Bomb.SetTrigger("DisBoom");
            RightAttack.SetActive(true);
            LeftAttack.SetActive(true);
            Clean = true;
            Invoke("Cleanup", 1f);
        }
    }

    void Cleanup()
    {
        //Clean = false;
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")//감지안되고 그냥 떨어질때 대비
        {
                Bomb.SetTrigger("DisBoom");
                RightAttack.SetActive(true);
                LeftAttack.SetActive(true);
                Clean = true;
                Invoke("Cleanup", 1f);        
        }
    }*/
}
