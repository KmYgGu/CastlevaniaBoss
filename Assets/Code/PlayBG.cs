using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBG : MonoBehaviour
{
    //private AudioSource BGPlayer;
    public AudioSource [] BGPlayer = new AudioSource[3];
    public AudioClip BGM;
    //private AudioSource BGPlayer2;
    public AudioClip GameOver;
    //private AudioSource BGPlayer3;
    public AudioClip Win;
    bool once;
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        //BGPlayer[0] = gameObject.AddComponent<AudioSource>();
        BGPlayer[0].clip = BGM;
        BGPlayer[0].loop = true;
        //BGPlayer[1] = gameObject.AddComponent<AudioSource>();
        BGPlayer[1].clip = GameOver;
        BGPlayer[1].loop = false;
        //BGPlayer[2] = gameObject.AddComponent<AudioSource>();
        BGPlayer[2].clip = Win;
        BGPlayer[2].loop = false;
        BGPlayer[0].Play();//실행
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.end == true && !once && PlayerControl.Health == 0)//캐릭터사망
        {
            once = true;
            BGPlayer[0].Stop();
            BGPlayer[0].loop = false;
            //BGPlayer[0].PlayTime = 0;
            BGPlayer[1].Play();
            //BGPlayer[2].Play();
        }
        if (PlayerControl.end == true && !once && Marx.BossHealth ==0)//승리
        {
            once = true;
            BGPlayer[0].Stop();
            BGPlayer[0].loop = false;
            //BGPlayer[0].PlayTime = 0;
            //BGPlayer[1].Play();
            BGPlayer[2].Play();
        }
    }
}
