using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazuka : MonoBehaviour
{
    int a, c;
    float b;
    public static bool BZKOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = 1.2f;
        c = 0;
        StartCoroutine(Size());
    }

    private void Update()
    {
      if (BZKOn)
        {
            BZKOn = false;
            StartCoroutine(Size());
        }  
    }
    // Update is called once per frame

    IEnumerator Size()
    {
        //a = 0;
        //c = 0;
        //yield return new WaitForSeconds(0.7f);
        /*while (a < 5)
        {
            //transform.localScale = new Vector3(4, b, 1);
            b += 0.2f;
            a++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        while (c < 8)
        {
            b -= 0.3f;
            //transform.localScale = new Vector3(4, b, 1);
            c++;
            yield return new WaitForSeconds(0.1f);
        }*/
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LV")//플레이어에게 닿으면
        {
            PlayerControl.Health -= 300;//60데미지
        }
    }
    
}
