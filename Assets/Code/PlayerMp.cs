using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMp : MonoBehaviour
{
    Text Mp;
    // Start is called before the first frame update
    void Start()
    {
        Mp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Mp.text = "서브웨폰 마력 : " + PlayerControl.Weapon.ToString();
    }
}
