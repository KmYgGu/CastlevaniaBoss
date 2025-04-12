using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteMe2());
    }

    // Update is called once per frame
    IEnumerator DeleteMe2()
    {
        yield return new WaitForSeconds(2f);//if (Rose.active == true) 
        //yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
