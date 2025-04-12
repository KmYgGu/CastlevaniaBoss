using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    SpriteRenderer End;
    byte a;
    public GameObject Vjt;
    // Start is called before the first frame update
    void Start()
    {
        Vjt.SetActive(false);
        End = GetComponent<SpriteRenderer>();
        End.color = new Color32(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.end == true && PlayerControl.Health ==0)
        {
            End.color = new Color32(255, 255, 255, a);
            if (a != 255) a++;
        }
        if(PlayerControl.end == true &&PlayerControl.eat == false)
        {
            Vjt.SetActive(true);
        }
    }
}
