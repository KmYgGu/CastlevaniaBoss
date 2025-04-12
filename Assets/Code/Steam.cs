using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public GameObject Rose;
    int a;//장미 랜덤값
    // Start is called before the first frame update
    void Start()
    {
        a = Random.Range(0, 3);
        if(a==2) Instantiate(Rose, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        //Invoke("DeleteMe", 2f);
        StartCoroutine(DeleteMe2());
    }

    // Update is called once per frame

    IEnumerator DeleteMe2()
    {
        yield return new WaitForSeconds(2f);//if (Rose.active == true) 
        //Rose.SetActive(false);//Destroy(Rose);
        //Destroy(Rose.gameObject);
        //yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LV")
        {
            PlayerControl.Health -= 20;
        }
    }
}
