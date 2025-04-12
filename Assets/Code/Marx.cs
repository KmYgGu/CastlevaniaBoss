using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Marx : MonoBehaviour
{

    Animator Marxani;
    public Animator Aani;//자식 컴포넌트1
    public Animator Bani;//자식 컴포넌트2
    public Animator Sani;
    public GameObject CutA;
    public GameObject CutB;
    public GameObject Hole;
    public SpriteRenderer SpRend;//블랙홀 깜빡임을 표현하기위함
    public Animator Hani;//블랙홀 컴포넌트
    public GameObject Gather;//블랙홀 모아지는거 표현
    public GameObject SpinL;//왼쪽 커터
    public GameObject SpinR;
    public GameObject Seed;//3패턴씨앗
    public GameObject Target;//플레이어
    public GameObject Shawdow;//그림자
    Rigidbody2D Body;
    public GameObject LArrow;//왼쪽에서 시작하는 화살
    public GameObject RArrow;
    public GameObject Ice;//얼음폭탄
    SpriteRenderer Marxsprite;
    public GameObject BZK;//입 바주카

    public int a;//while문 반복을 위한 정수 객체
    public int ChangeCount;//패턴변형을 위한 정수
    public int ChangeCount2;
    int R1;//랜덤 X축
    int R2;//랜덤 Y축
    public int b;//3패턴 반복
    public int c;//3패턴 반복2
    //public bool SeedOn = false;//씨앗존 도착

    public static int BossHealth;//보스 체력수치
    
    // Start is called before the first frame update
    void Start()
    {
        Marxani = GetComponent<Animator>();
        Body = GetComponent<Rigidbody2D>();
        Marxsprite = GetComponent<SpriteRenderer>();
        ChangeCount = 0;
        ChangeCount2 = 0;
        Change();
        //StartCoroutine(Appear());//다햇으면 위주석과 체인지1
        //StartCoroutine(Patten4());
        BossHealth = 5000;//10000
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.SetActive(false);
        //Attack1();


    }

    IEnumerator Appear()//본체 나타나기 9
    {
        R1 = Random.Range(-9, 10);//-9에서 9무작위
        R2 = Random.Range(-2, 6);//-2에서 5까지
        this.gameObject.transform.position = new Vector3(R1, R2, 0);
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //Marxani.SetTrigger("Apear");
        Marxani.SetBool("Apear2", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;//피격판정 생성
        //StartCoroutine(Patten6());//다했으면 아래 주석과 체인지2
        ChangeCheck();
    }

    IEnumerator Patten1()//중력 패턴
    {
        a = 0;
        //Marxani.SetBool();
        yield return new WaitForSeconds(1);
        //Marxani.SetTrigger("CutGo");
        Marxani.SetBool("CutGo2", true);
        yield return new WaitForSeconds(1);//반짤리기
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Marxani.SetBool("CutGo2", false);
        //CutA.transform.position = new Vector3(-0.105f, 0, 0);
        //CutB.transform.position = new Vector3(0.105f, 0, 0);
        CutA.transform.localPosition = new Vector3(-0.105f, -0.05f, 0);//위치지정
        CutB.transform.localPosition = new Vector3(0.105f, 0.05f, 0);
        //CutA.transform.localPosition = new Vector3(-0.105f, 0, 0);//위치지정
        //CutB.transform.localPosition = new Vector3(0.105f, 0, 0);
        CutA.GetComponent<SpriteRenderer>().enabled = true;
        CutB.GetComponent<SpriteRenderer>().enabled = true;
        /*while (a < 5)
        {
            CutA.transform.Translate(0, -0.02f, 0);
            CutB.transform.Translate(0, 0.02f, 0);
            a += 1;
            yield return new WaitForSeconds(0.2f);
        }*/

        yield return new WaitForSeconds(1);//벌려지기
        CutA.transform.Translate(-1.5f, 0.5f, 0);//현재위치에다 추가하기
        CutB.transform.Translate(1.5f, -0.5f, 0);//1
        Aani.SetTrigger("AC");
        Bani.SetTrigger("BC");
        yield return new WaitForSeconds(0.8f);//블랙홀소환
        //Hole.SetActive(true);
        Hole.GetComponent<SpriteRenderer>().enabled = true;
        Hole.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(HoleBlink());
        yield return new WaitForSeconds(1);//모아지기
        Gather.transform.localScale = new Vector3(7.14f, 7.14f, 0);//크기초기화
        Gather.SetActive(true);
        MargNet.homing = true;
        while (a < 25)
        {
            Gather.transform.localScale -= new Vector3(0.3f, 0.3f, 0);
            a += 1;
            yield return new WaitForSeconds(0.1f);
            if (a == 12)//반토막 낸것들 사라지는 연출
            {
                Aani.SetBool("AD", true);
                Bani.SetBool("BD", true);
            }
            if(a == 17)//반토막 낸것들 숨기기
            {
                CutA.GetComponent<SpriteRenderer>().enabled = false;
                CutB.GetComponent<SpriteRenderer>().enabled = false;
                //Hani.SetBool("HD", true);
            }
        }
        MargNet.homing = false;
        Gather.SetActive(false);
        Hole.GetComponent<BoxCollider2D>().enabled = false;
        Hani.SetBool("HD", true);
        yield return new WaitForSeconds(0.5f);//블랙홀 사라짐
        Hole.GetComponent<SpriteRenderer>().enabled = false;
        Hani.SetBool("HD", false);
        Aani.SetBool("AD", false);
        Bani.SetBool("BD", false);
        Marxani.SetBool("Apear2", false);//사라지기
        Change();
    }

    IEnumerator HoleBlink()//블랙홀 깜빡이는것
    {
        int countTime = 0;//깜빡이는 횟수

        while (countTime < 6)
        {
            if (countTime % 2 == 0)
                SpRend.color = new Color32(255, 255, 255, 30);//90
            else
                SpRend.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.1f);

            countTime++;
        }
        SpRend.color = new Color32(255, 255, 255, 255);
        yield return null;
    }

    IEnumerator Patten2()//부메랑 패턴
    {
        a = 0;
        yield return new WaitForSeconds(1);
        while (a < 2)
        {
            Marxani.SetTrigger("Cir");
            //Marxani.SetBool("Cir2", true);
            SpinL.SetActive(true);
            SpinR.SetActive(true);
            yield return new WaitForSeconds(2);
            //Marxani.SetBool("Cir2", false);
            a += 1;
        }
        yield return new WaitForSeconds(1);
        Marxani.SetBool("Apear2", false);//사라지기
        Change();//맨끝에 넣어야됨 다쓰면 활성화
    }

    void Change()//패턴 바꾸기
    {
        StartCoroutine(Appear());
        ChangeCount += 1;
        if(BossHealth <=2500) ChangeCount2 += 1;
    }

    void ChangeCheck()//패턴 순서
    {
        if(BossHealth > 2500)
        {
            switch (ChangeCount)
        {
            case 1:
                StartCoroutine(Patten1());
                break;
            case 2:
                StartCoroutine(Patten2());
                break;
            case 3:
                StartCoroutine(Patten3());
                break;
            case 4:
                StartCoroutine(Patten4());
                break;
            case 5:
                StartCoroutine(Patten5());
                break;
            case 6:
                StartCoroutine(Patten6());
                ChangeCount = 0;//마지막엔 넣어야하는거
                break;

        }
        }
        if (0 < BossHealth &&BossHealth <= 2500)
        {
            switch (ChangeCount2)
            {
                case 1:
                    StartCoroutine(Patten2());
                    break;
                case 2:
                    StartCoroutine(Patten3());
                    break;
                case 3:
                    StartCoroutine(Patten5());
                    break;
                case 4:
                    StartCoroutine(Patten6());
                    break;
                case 5:
                    StartCoroutine(Patten1());
                    break;
                case 6:
                    StartCoroutine(Patten4());
                    ChangeCount2 = 0;//마지막엔 넣어야하는거
                    break;

            }

        }
        if(BossHealth <= 0)//사망
        {
            BossHealth = 0;
            StartCoroutine(Dead());
        }

    }

    IEnumerator Patten3()//씨뿌리기 패턴 사라지기 여기부터 새로 추가
    {
        a = 0;
        b = 0;
        c = 0;
        yield return new WaitForSeconds(1);
        Marxani.SetBool("Up", true);
        while (a < 100)//SeedOn
        {
            a ++;
            if(a < 5)//아래잠깐이동
            {
                transform.Translate(0, -0.5f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            if (a >= 5)//위로 슝
            {
                transform.Translate(0, 0.5f, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        //Marxani.SetBool("Up", false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        while (b < 7)//씨뿌리기
        {
            R1 = Random.Range(-10, 11);//-9,10
            Instantiate(Seed, new Vector2(0+R1, 8), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            b++;
        }
        yield return new WaitForSeconds(8f);//씨앗이 피고난 후의 시간 Seed참고
        this.gameObject.transform.position = new Vector3(Target.transform.position.x, -5, 0);
        Shawdow.GetComponent<SpriteRenderer>().enabled = true;
        while (c < 300)//땅속에서 따라가기
        {
            if (gameObject.transform.position.x < Target.transform.position.x)//오른쪽
            {
                //transform.Translate(0.2f, 0, 0);
                //transform.Translate(Target.transform.position.x - gameObject.transform.position.x, 0, 0);
                Body.AddForce(transform.right * (Target.transform.position.x - gameObject.transform.position.x), ForceMode2D.Impulse);
                if (gameObject.transform.position.x > 11) 
                {
                    //transform.position = new Vector3(9, -5, 0);//이탈 방지 Body.velocity = Vector3.zero;
                    Body.AddForce(transform.right * -2, ForceMode2D.Impulse);
                }
            }
            if (gameObject.transform.position.x > Target.transform.position.x)//왼쪽
            {
                //transform.Translate(-0.2f, 0, 0);
                //transform.Translate(gameObject.transform.position.x - Target.transform.position.x, 0, 0);
                Body.AddForce(transform.right * -(gameObject.transform.position.x - Target.transform.position.x), ForceMode2D.Impulse);
                if (gameObject.transform.position.x < -11)
                {
                    //transform.position = new Vector3(-9, -5, 0);// Body.velocity = Vector3.zero;
                    Body.AddForce(transform.right * 2, ForceMode2D.Impulse);
                } 

            }
            /*if (Mathf.Round(gameObject.transform.position.x) == Mathf.Round(Target.transform.position.x))//반올림
            {
                R2 = Random.Range(-2, 3);
                transform.Translate(R2, 0, 0);
            }*/
            //if(gameObject.transform.position.x < 10) transform.position = new Vector3(9, -5, 0);//이탈 방지
            //if (gameObject.transform.position.x > -10) transform.position = new Vector3(-9, -5, 0);
            if (c == 150) Sani.SetTrigger("S2Go");
            if (c == 250) Sani.SetBool("S3Go", true);
            yield return new WaitForSeconds(0.01f);
            c++;
        }
        Body.velocity = Vector3.zero;
        Shawdow.GetComponent<SpriteRenderer>().enabled = false;
        Sani.SetBool("S3Go", false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        Body.AddForce(transform.up * 50, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        Body.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        Marxani.SetBool("Up", false);
        yield return new WaitForSeconds(1.5f);

        Marxani.SetBool("Apear2", false);//사라지기
        Change();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SeedZone")
        {
            a = 100;
        }
        if (collision.gameObject.tag == "Pattack1")
        {
            BossHealth -= 48;//48데미지줌
        }
        if(collision.gameObject.tag == "LV")
        {
            PlayerControl.Health -= 30;//플레이어에게 20데미지
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pattack2")
        {
            BossHealth -= 2;//2데미지줌
        }
    }

    IEnumerator Patten4()//반 갈라서 날라가는 게 아닌 발사체 연속발사 패턴
    {
        a = 0;
        R1 = Random.Range(0, 2);//0에서 1까지
        //R1 = 1;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;//피격판정 생성
        if (R1 == 0)//왼쪽에서 발사
        {
            if(Target.transform.position.y < 0) this.gameObject.transform.position = new Vector3(-11, Target.transform.position.y, 0);//왼쪽에서 나타남
            if (Target.transform.position.y >= 0) this.gameObject.transform.position = new Vector3(-11, -0.5f, 0);//왼쪽 중간에서 나타남
            Marxani.SetBool("Side", true);//나타나기
            yield return new WaitForSeconds(1f);
            Marxani.SetBool("Go", true);
            Body.AddForce(transform.right * 0.1f, ForceMode2D.Impulse);
            while (a < 20)
            {
                Instantiate(LArrow, new Vector2(gameObject.transform.position.x - 3, gameObject.transform.position.y + 1), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(LArrow, new Vector2(gameObject.transform.position.x - 3, gameObject.transform.position.y), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(LArrow, new Vector2(gameObject.transform.position.x - 2, gameObject.transform.position.y - 1), Quaternion.identity);
                a++;
            }
            Marxani.SetBool("Go", false);
            Body.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.3f);
            Body.AddForce(transform.right * 30, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
            Marxani.SetBool("Side", false);
            Body.velocity = Vector3.zero;
        }
        if (R1 == 1)//오른쪽에서 발사
        {
            transform.localScale = new Vector3(-7, 7, 1);//뒤집기
            if (Target.transform.position.y < 0) this.gameObject.transform.position = new Vector3(11, Target.transform.position.y, 0);//오른쪽에서 나타남
            if (Target.transform.position.y >= 0) this.gameObject.transform.position = new Vector3(11, -0.5f, 0);//오른쪽 중간에서 나타남
            Marxani.SetBool("Side", true);//나타나기
            yield return new WaitForSeconds(1f);
            Marxani.SetBool("Go", true);
            Body.AddForce(transform.right * -0.1f, ForceMode2D.Impulse);
            while (a < 20)
            {
                Instantiate(RArrow, new Vector2(gameObject.transform.position.x + 3, gameObject.transform.position.y + 1), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(RArrow, new Vector2(gameObject.transform.position.x + 3, gameObject.transform.position.y), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                Instantiate(RArrow, new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y - 1), Quaternion.identity);
                a++;
            }
            Marxani.SetBool("Go", false);
            Body.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.3f);
            Body.AddForce(transform.right * -30, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
            Marxani.SetBool("Side", false);
            Body.velocity = Vector3.zero;
            transform.localScale = new Vector3(7, 7, 1);//반전 되돌리기
        }
        yield return new WaitForSeconds(1f);

        Marxani.SetBool("Apear2", false);//사라지기 추가
        Change();
    }

    IEnumerator Patten5()//얼음 발사 패턴 
    {
        a = 0;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;//피격판정 생성
        this.gameObject.transform.position = new Vector3(Target.transform.position.x, 5, 0);
        Marxani.SetTrigger("Mouth");
        while(a < 100)
        {
            if (gameObject.transform.position.x < Target.transform.position.x)
            {
                transform.Translate(0.2f, 0, 0);
            }
            if (gameObject.transform.position.x > Target.transform.position.x)
            {
                transform.Translate(-0.2f, 0, 0);
            }
            yield return new WaitForSeconds(0.01f);
            a++;
        }
        yield return new WaitForSeconds(0.4f);
        Marxani.SetBool("Drop", true);//퉤
        Instantiate(Ice, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-1), Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Marxani.SetBool("Drop", false);


        Marxani.SetBool("Apear2", false);//사라지기
        Change();
    }

    IEnumerator Patten6()//입 바주카
    {
        a = 0;
        byte d = 245;//255
        float e = 0;
        yield return new WaitForSeconds(1f);
        if(gameObject.transform.position.x < Target.transform.position.x)//왼쪽으로감
        {
            if (Target.transform.position.y < 0) this.gameObject.transform.position = new Vector3(-7, Target.transform.position.y, 0);
            if (Target.transform.position.y >= 0) this.gameObject.transform.position = new Vector3(-7, -0.5f, 0);
            e = -0.2f;//왼쪽으로 이동하게함
            b = -50;//리지드바디 움직이게 하는 수치
        }
        if (gameObject.transform.position.x > Target.transform.position.x)//오른쪽으로감
        {
            transform.localScale = new Vector3(-7, 7, 1);//뒤집기
            if (Target.transform.position.y < 0) this.gameObject.transform.position = new Vector3(7, Target.transform.position.y, 0);
            if (Target.transform.position.y >= 0) this.gameObject.transform.position = new Vector3(7, -0.5f, 0);
            e = 0.2f;//오른쪽으로 이동하게함
            b = 50;
        }
        Marxani.SetTrigger("Mouth");
        while (a < 10)//색 변경
        {
                Marxsprite.color = new Color32(d, d, d, 255);//150까지 충분
            d -= 10;
            a++;
            transform.Translate(e, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }
        Marxsprite.color = new Color32(255, 255, 255, 255);
        Marxani.SetBool("Pu", true);//우웱
        Body.AddForce(transform.right * b, ForceMode2D.Impulse);
        BZK.SetActive(true);
        Bazuka.BZKOn = true;
        yield return new WaitForSeconds(0.2f);
        Marxani.SetBool("Pu", false);
        Body.velocity = Vector3.zero;
        //yield return null;
        yield return new WaitForSeconds(2.5f);
        transform.localScale = new Vector3(7, 7, 1);//반전 되돌리기

        Marxani.SetBool("Apear2", false);//사라지기
        Change();
    }

    IEnumerator Dead()//사망
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Marxani.SetTrigger("Die");
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(5);
        if(PlayerControl.eat == true) SceneManager.LoadScene("End");
        if (PlayerControl.eat == false) SceneManager.LoadScene("End2");

    }
}
