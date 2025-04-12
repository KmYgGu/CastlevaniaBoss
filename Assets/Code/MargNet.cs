using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargNet : MonoBehaviour
{

    public Transform player;
    public float GrabSpped = 10f;
    static public bool homing;
    public Vector3 disvec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (homing) 
        {
            disvec = (player.position - this.gameObject.transform.position).normalized;

            player.transform.position -= disvec * Time.deltaTime * GrabSpped;
            //player.transform.up = disvec;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LV")//플레이어에게 닿으면
        {
            PlayerControl.Health -= 2;//60데미지
        }
    }
}
