using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWimage : MonoBehaviour
{
    public SpriteRenderer Charno;
    public Sprite[] sprites = new Sprite[7];

    // Start is called before the first frame update
    void Start()
    {
        Charno = GetComponent<SpriteRenderer>();
        Charno.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerControl.WCount)
        {
            case 1:
                //gameObject.GetComponent<Image>();
                Charno.sprite = sprites[PlayerControl.WCount];
                break;
            case 2:
                Charno.sprite = sprites[PlayerControl.WCount];
                break;
            case 3:
                Charno.sprite = sprites[PlayerControl.WCount];
                break;
            case 4:
                Charno.sprite = sprites[PlayerControl.WCount];
                break;
            case 5:
                Charno.sprite = sprites[PlayerControl.WCount];
                break;
        }
    }
}
