using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter2 : MonoBehaviour
{
    Vector2 LeftDown = new Vector2();
    public float runningTime = 0;
    public float speed = 7;//5
    public float radius = 5;


    public float x;
    public float y;
    public float a = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
        Invoke("Delete", 0.5f);
    }

    void Spin()
    {
        //radius -= 1 * Time.deltaTime;
        runningTime -= Time.deltaTime * speed;//-로 하면 시계방향회전 
        x = radius * Mathf.Cos(runningTime);
        y = radius * Mathf.Sin(runningTime);
        LeftDown = new Vector2(x, y);
        this.transform.localPosition = LeftDown;
        //a += 1 * Time.deltaTime;
    }

    void Delete()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LV")//플레이어에게 닿으면
        {
            PlayerControl.Health -= 80;//60데미지
        }
    }
}
