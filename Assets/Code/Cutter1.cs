using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter1 : MonoBehaviour
{
    Vector2 LeftDown = new Vector2();
    public float runningTime = 0;
    public float speed = 7;//5
    public float radius = 5;


    public float x;
    public float y;
    public float a = 0;

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        /*if (a < 2)
        {
            radius = 5;
            Spin();
        }
        if(a >= 2)
        {
            radius = 1;
            Spin();
        }*/
        Spin();
        Invoke("Delete", 0.5f);
    }

    void Spin()
    {
        //radius -= 1 * Time.deltaTime;
        runningTime += Time.deltaTime * speed;//-로 하면 시계방향회전 
        //float x = radius * Mathf.Cos(runningTime);
        //float y = radius * Mathf.Sin(runningTime);
        x = radius * Mathf.Cos(runningTime);
        y = radius * Mathf.Sin(runningTime);
        LeftDown = new Vector2(x, y);
        //this.transform.position = LeftDown;
        this.transform.localPosition = LeftDown;
        //this.transform.Translate (x,y,0);
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
