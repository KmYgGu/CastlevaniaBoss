using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    Text BossHp;
    // Start is called before the first frame update
    void Start()
    {
        BossHp = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        BossHp.text = "보스 체력 : " + Marx.BossHealth.ToString();
    }
}
