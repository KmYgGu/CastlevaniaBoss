using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public SpriteRenderer Charno;
    public Sprite[] sprites = new Sprite[7];
    int a;
    bool b;
    // Start is called before the first frame update
    void Start()
    {
        Charno = GetComponent<SpriteRenderer>();
        Charno.sprite = sprites[0];
        a = 0;
        b = true;
        //StartCoroutine(PRESSZ());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)&&b)
        {
            a++;
            b = false;
            Invoke("Rock", 1f);
        }
        //StartCoroutine(PRESSZ());
        switch (a)
        {
            case 0:
                //gameObject.GetComponent<Image>();
                Charno.sprite = sprites[a];//1
                break;
            case 1:
                Charno.sprite = sprites[a];//2
                break;
            case 2:
                Charno.sprite = sprites[a];//3
                break;
            case 3:
                Charno.sprite = sprites[a];//4
                break;
            case 4:
                Charno.sprite = sprites[a];//5
                break;
            case 5:
                Charno.sprite = sprites[a];//6
                break;
            case 6:
                Charno.sprite = sprites[a];//7
                break;
            case 7:
                Charno.sprite = sprites[a];//8
                break;
        }
    }
    
    void Rock()
    {
        b = true;
    }
}
