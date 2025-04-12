using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterRE : MonoBehaviour//개발자 역량 부족으로 구현못함
{
    GameObject Boss;
    public static bool CutGo;
    Rigidbody2D rigid2D;
    Vector2 LeftCut = new Vector2(-10, 0);
    Vector3 Back = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        Boss = GameObject.FindGameObjectWithTag("Boss");
        CutGo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CutGo)
        {
            Throw();
        }
        if (!CutGo)
        {
            StartCoroutine(BackCut());
        }
    }

    void Throw()
    {
        rigid2D.AddForce(LeftCut, ForceMode2D.Impulse);
        CutGo = false;
    }

    IEnumerator BackCut()
    {
        //this.transform.position = new Vector3(this.transform.position.x +)
        Back = (gameObject.transform.position - Boss.transform.position).normalized;
        this.gameObject.transform.Translate(Back * 1);

        yield return null;
    }
}
