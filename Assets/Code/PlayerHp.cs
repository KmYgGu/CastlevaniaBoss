using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Text Hp;
    // Start is called before the first frame update
    void Start()
    {
        Hp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Hp.text = "플레이어 체력 : " + PlayerControl.Health.ToString();
    }
}
