using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    public GameObject LVattack;
    public GameObject LVlong;
    public GameObject LVDattack;
    public GameObject LVDlong;
    public GameObject SDKick;//사이드 킥
    public GameObject FKick;//하단 킥
    public GameObject Knife;//나이프
    public GameObject AX;//도끼
    public GameObject CROSS;//십자가
    public GameObject Cury;//카레

    public static int key = 1;
    public int JumpWay = 0;//점프방향
    public int JumpPower = -200;//킥중력
    //float walkForce = 30.0f;
    //float maxWalkSpeed = 5.0f;

    public float movePower = 5f;
    public float jumpPower = 0;//최대값 2000f
    public bool isJumping = false;
    public bool exit = false;//중복점프 방지
    public bool stop = false;//꾹누르면 연속점프 방지
    public int JumpCount = 0;//2단까지
    public bool deal = false;//발차기동작불가 딜레이 @땅에 닿으면 초기화
    public bool DownC = false;//숙이기중 이동불가 @일어나면 초기화
    public bool JumpC = false;//숙이기중 점프불가 @일어나면 초기화
    public bool SlidC = false;//슬라이딩중 조작불가
    public bool BSD = false;//백스텝 딜레이 @슬라이딩 후 조금만 시간 지나면 초기화
    public bool AAD = false;//돌리기중 이동불가
    //public bool OutOk = false;
    public bool JAD = false;//점프공격중 이동키를 눌러도 캐릭터 방향바꾸기 방지,점프공격 딜레이
    public int SpinBar = 0;//공격버튼을 꾹 누른후 스핀바 게이지 이상 되야 돌리기가 시전됨
    public bool DDSpinX = false;//아래 회전공격과 일반 회전공격이 겹치지 않게 방지
    public bool SDlX = false;//공격키 꾹 누르면서 슬라이딩하면 슬라이딩 끝난후에 자동으로 돌리기 공격방지

    public static int Health;//플레이어 체력
    public static int Weapon;//무기웨폰 수치

    public static int WCount;//무기교체에 사용되는 수치
    public bool Wok;//서브웨폰 딜레이

    public static bool end;//플레이어가 이겼나 혹은 졌나에 따라 움직임제한
    public static bool eat;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Health = 1000;//1000
        Weapon = 100;
        WCount = 1;
        StartCoroutine(WeaponReturn());
        Wok = false;
        end = false;
        eat = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveStop();
        if (!end)
        {
            if ((!deal) && (!SlidC))
            {
                //Kick();
                Attack();
                if (!AAD)
                {
                    Move();
                    Jump();
                    Slide();
                    UseSubWeapon();
                }
            }
            BackStep();
            Sork();
            Weaponchange();
        }
        //CharControl();
        
    }

    /*public void CharControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid2D.AddForce(transform.up * 680f);
        }
        if (Input.GetKey(KeyCode.RightArrow))//오른쪽으로 가면
        {
            rigid2D.AddForce(Vector2.right * 1, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.LeftArrow))//왼쪽으로 가면
        {
            rigid2D.AddForce(Vector2.left * 1, ForceMode2D.Impulse);
        }
        //int key = 0;
        //int stop = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
            //stop = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
            //stop = 1;
        }

        // 플레이어 속도
        float speedx = Mathf.Abs(rigid2D.velocity.x);

        //스피드 제한
        if(speedx < maxWalkSpeed)
        {
            rigid2D.AddForce(transform.right * key * walkForce);
            //rigid2D.AddForce(transform.right * key * walkForce * stop);
        }

        //움직이는 방향에 따라 이미지 반전 추가
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        //브레이크
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rigid2D.velocity = new Vector2(0, 0);
            //stop = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //rigid2D.velocity = new Vector2(0, 0);
            //stop = 0;
        }
    }*/

    public void Move()
    {
        //Kick();
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVelocity = Vector3.left;
            if ((!BSD) && (!JAD)) key = -1;
            if(JumpCount == 0)//땅에 붙어잇을때만
            {
                animator.SetBool("Walk", true);
                //animator.SetBool("Stand", false);
                //animator.SetTrigger("Run");
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVelocity = Vector3.right;
            if ((!BSD)&&(!JAD)) key = 1;
            if (JumpCount == 0)//땅에 붙어잇을때만
            {
                animator.SetBool("Walk", true);
                //animator.SetBool("Stand", false);
                //animator.SetTrigger("Run");
            }
        }
        if(!DownC) transform.position += moveVelocity * movePower * Time.deltaTime;

        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
        //else animator.SetBool("Walk", false);
        //if ((Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.RightArrow))) animator.SetBool("Stand", true);
        if ((!Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.RightArrow))) animator.SetBool("Walk", false);//좌우방향키 입력상태아님
    }

    public void Jump()//점프공격.잠깐 착지 회전 정지
    {
        //rigid2D.velocity = Vector2.zero; 골드메탈방식
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space)&& (JumpCount == 0)&&(!JumpC))//처음 점프
            {
                stop = true;
            }
            if (Input.GetKey(KeyCode.Space)&&(stop) && (JumpCount == 0))//모으기 && !Input.GetKey(KeyCode.DownArrow)
            {
                //Vector2 jumpVelocity = new Vector2(0, jumpPower);
                //rigid2D.AddForce(jumpVelocity, ForceMode2D.Impulse);
                jumpPower += 30;
                //animator.SetBool("Jump", true);
                if (jumpPower < 150) jumpPower = 150;
                if (jumpPower > 400)
                {
                    jumpPower = 400;
                    rigid2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                    //isJumping = true;
                    exit = true;
                    stop = false;
                    JumpCount = 1;
                    animator.SetBool("Jump", true);
                }

            }
            if (Input.GetKeyUp(KeyCode.Space)&&(!exit) && (JumpCount == 0))
            {
                rigid2D.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                //isJumping = true;
                stop = false;
                JumpCount = 1;
                animator.SetBool("Jump", true);
            }
            if (Input.GetKeyDown(KeyCode.Space) && (JumpCount == 1))//2단 점프
            {
                JumpCount = 2;
                rigid2D.velocity = Vector3.zero;//증폭안되게 방지
                rigid2D.AddForce(transform.up * 300, ForceMode2D.Impulse);
                //transform.position += new Vector3(0, 2, 0); 
                isJumping = true;
                animator.SetBool("Jump2", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) && (JumpCount >= 1)&& !JAD)//점프공격
        {
            isJumping = true;
            JumpCount = 3;//점프킥 못하게 방지
            animator.SetTrigger("JAttack");
            Invoke("AttackStart", 0.2f);
            Invoke("Out", 0.4f);
            JAD = true;
            Invoke("JADReset", 0.4f);
        }
        /*if (Input.GetKey(KeyCode.Z) && (JumpCount >= 0))//무기 돌리기 && !Input.GetKey(KeyCode.DownArrow)
        {
            LVlong.SetActive(false);//효과가 없는 듯하다?
            Invoke("Spin", 0.3f);//0.5
            animator.SetBool("ALong", true);
            AAD = true;
            //SlidC = true;   
            //animator.SetBool("isSpin", true);
            if (SpinBar < 500)
            {
                SpinBar += 50;
            }
            if (SpinBar >= 500)
            if (JumpCount == 0)
            {
                //Invoke("DownSpin", 0.5f);
                Spin();
                animator.SetBool("ALong", true);
                AAD = true;
            }
        }*/
    }

    void JADReset()
    {
        if(JAD) JAD = false;
    }
    
    public void Kick()
    {
        //int JumpWay = 0;
        //int JumpPower = -200;
        Vector2 JumpKick = new Vector2(JumpWay, JumpPower);
        //Vector3 moveVelocity2 = Vector3.zero;
        if (Input.GetKey(KeyCode.DownArrow)&& (JumpCount >= 1)&&(!(Input.GetKey(KeyCode.RightArrow))&&(!(Input.GetKey(KeyCode.LeftArrow)))))//아래킥
        {

            JumpWay = 0;
            JumpPower = -200;
            if (Input.GetKeyDown(KeyCode.Space) && (JumpCount == 2)&&(!deal))
            {
                deal = true;
                animator.SetBool("Kick", true);
                rigid2D.AddForce(JumpKick, ForceMode2D.Impulse);
                FKick.GetComponent<BoxCollider2D>().enabled = true;//발차기 판정 활성화
            }
        }
        if(Input.GetKey(KeyCode.RightArrow) && (JumpCount >= 1))//대각선킥
        {

            JumpWay = 400;
            JumpPower = -100;//전값 50;
            
            //moveVelocity2 = Vector3.right;
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space) && (JumpCount == 2) && (!deal))
            {
                deal = true;
                animator.SetBool("SideKick", true);
                rigid2D.AddForce(JumpKick, ForceMode2D.Impulse);
                SDKick.GetComponent<BoxCollider2D>().enabled = true;//발차기 판정 활성화
                //animator.SetBool("Jump2", false);
            }

            //transform.position += new Vector3(5, 0, 0);//테스트
            //Invoke("DelayKick", 0.1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && (JumpCount >= 1))
        {
            JumpWay = -400;
            JumpPower = -100;
            
            //moveVelocity2 = Vector3.left;
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space) && (JumpCount == 2) && (!deal))
            {
                deal = true;
                animator.SetBool("SideKick", true);
                rigid2D.AddForce(JumpKick, ForceMode2D.Impulse);
                SDKick.GetComponent<BoxCollider2D>().enabled = true;//
                //animator.SetBool("Jump2", false);
            }
        }
    }

    void Sork()//발차기 맞았다
    {
        if (SideKick.Shork)
        {
            deal = false;
            JumpCount = 2;//2단점프 코드
            rigid2D.velocity = Vector3.zero;//증폭안되게 방지
            rigid2D.AddForce(transform.up * 300, ForceMode2D.Impulse);
            //transform.position += new Vector3(0, 2, 0); 
            isJumping = true;
            animator.SetBool("Jump2", true);
            animator.SetTrigger("Noj");
            animator.SetBool("SideKick", false);
            animator.SetBool("Kick", false);
            SideKick.Shork = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")//땅에 닿을때
        {
            jumpPower = 0;
            isJumping = false;
            exit = false;
            JumpCount = 0;
            deal = false;
            rigid2D.velocity = Vector3.zero;//증폭안되게 방지
            //JumpWay = 0;
            //JumpPower = 0;
            animator.SetBool("Jump", false);
            animator.SetBool("Jump2", false);
            animator.SetBool("SideKick", false);
            animator.SetBool("Kick", false);
            JAD = false;
            SDKick.GetComponent<BoxCollider2D>().enabled = false;
            FKick.GetComponent<BoxCollider2D>().enabled = false;
            SideKick.jumpDamage = 1;//땅에 닿으면 점프공격 데미지 초기화
        }
        //else JumpCount = 1;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (JumpCount == 3)
            {
                JumpCount = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "iceAttack")
        {
            Health -= 40;
        }
    }
    /*public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            JumpCount = 1;
        }
    }*/

    public void FixedUpdate()//오류수정
    {
        if(jumpPower == 0)
        {
            isJumping = false;
            JumpCount = 0;
            animator.SetBool("Jump", false);
            animator.SetBool("Jump2", false);
        }
        //if (!deal) 
        Kick();
        if (deal)
        {
            Invoke("DelayCheck", 3f);
        }
    }

    void DelayCheck()
    {
        deal = false;
    }

    public void Slide()//다운공격,무기돌리기추가
    {
        if (Input.GetKey(KeyCode.DownArrow) && (JumpCount == 0))
        {
            //LVDlong.SetActive(false);
            animator.SetBool("Sid", true);
            animator.SetBool("Stand", false);
            DownC = true;
            JumpC = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid2D.AddForce(transform.right * 260 * key, ForceMode2D.Impulse);
                animator.SetTrigger("SidGo");
                SlidC = true;
                Invoke("SlideC", 0.69f);
            }
            if (Input.GetKeyDown(KeyCode.Z) && !SlidC)//다운공격
            {
                LVDlong.SetActive(false);//일단추가
                LVlong.SetActive(false);
                animator.SetTrigger("DAttack");
                SlidC = true;
                Invoke("SlideC", 0.4f);
                Invoke("DownAttackStart", 0.2f);
                Invoke("DownOut", 0.4f);
                SDlX = true;
                /*if (Input.GetKey(KeyCode.Z) && (JumpCount == 0) && Input.GetKey(KeyCode.DownArrow))//무기 돌리기
                {
                    Invoke("DownSpin", 0.5f);
                    animator.SetBool("DALong", true);
                    AAD = true;
                    //SlidC = true;
                }*/
            }
            if (Input.GetKey(KeyCode.Z) && (JumpCount == 0) && Input.GetKey(KeyCode.DownArrow)&& SDlX)//다운상태무기돌리기
            {
                animator.SetBool("isSpin", true);
                if (SpinBar < 250)
                {
                    SpinBar += 50;
                }
                if (SpinBar >= 250)
                {
                    //Invoke("DownSpin", 0.5f);
                    DownSpin();
                    animator.SetBool("DALong", true);
                    AAD = true;
                    DDSpinX = true;
                }
            }
        }
        if(!Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("Sid", false);
            animator.SetBool("Stand", true);
            DownC = false;
            JumpC = false;
            //SpinBar = 0;
        }
    }

    void SlideC()
    {
        SlidC = false;
    }

    void DownAttackStart()
    {
        LVDattack.SetActive(true);
        //OutOk = true;
    }

    void DownOut()
    {
        LVDattack.SetActive(false);
        //OutOk = true;
    }

    void DownSpin()
    {
        if (Input.GetKey(KeyCode.Z)) LVDlong.SetActive(true);//공격키 꾹누르고 있는지 재차 확인
        if (Input.GetKeyUp(KeyCode.Z)) LVDlong.SetActive(false);
    }

    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Z)&&(JumpCount == 0)&& !Input.GetKey(KeyCode.DownArrow))//서있는 공격자세
        {
            LVlong.SetActive(false);
            LVDlong.SetActive(false);
            animator.SetTrigger("Attack");
            animator.SetBool("Walk", false);
            SlidC = true;//이것들지우면 점프나 슬라이딩으로 캔슬가능
            Invoke("SlideC", 0.4f);//공격중 다른 행동을 못하게 하는 딜레이 
            Invoke("AttackStart", 0.2f);//일반 공격 불러오는 함수 시작
            Invoke("Out", 0.4f);//일반공격 사라지게 하는 함수 시작
            /*if (Input.GetKey(KeyCode.Z) && (JumpCount == 0) && !Input.GetKey(KeyCode.DownArrow))//무기 돌리기
            {
                LVlong.SetActive(false);//효과가 없는 듯하다?
                Invoke("Spin", 0.5f);//0.6
                animator.SetBool("ALong", true);
                AAD = true;
                //SlidC = true;          
            }*/
        }
        if (Input.GetKey(KeyCode.Z) && (JumpCount == 0) && !Input.GetKey(KeyCode.DownArrow))//무기 돌리기&& !(JumpCount > 0) && !Input.GetKey(KeyCode.DownArrow)
        {
            animator.SetBool("isSpin", true);
            AAD = true;
            if (SpinBar < 250)//500
            {
                SpinBar += 50;
            }
            if (SpinBar >= 250&& !DDSpinX)
            {
                //Invoke("Spin", 0.5f);
                Spin();
                //LVlong.SetActive(true);
                //animator.SetBool("DALong", true);//?
                animator.SetBool("ALong", true);//?
                //AAD = true;
            }
        }
        if (!Input.GetKey(KeyCode.Z))//무기 돌리기 해제&& (JumpCount == 0)
        {
            animator.SetBool("isSpin", false);
            LVlong.SetActive(false);
            animator.SetBool("ALong", false);
            AAD = false;
            //OutOk = false;
            //SlidC = false;
            LVDlong.SetActive(false);
            animator.SetBool("DALong", false);
            SpinBar = 0;
            DDSpinX = false;
            SDlX = false;
        }

    }

    void AttackStart()
    {
        LVattack.SetActive(true);
        //OutOk = true;
    }

    void Spin()
    {
        if (Input.GetKey(KeyCode.Z)) LVlong.SetActive(true);//공격키 꾹누르고 있는지 재차 확인
        if (Input.GetKeyUp(KeyCode.Z)) LVlong.SetActive(false);
    }

    void Out()
    {
        LVattack.SetActive(false);
        //OutOk = true;
        /*if (Input.GetKey(KeyCode.Z) && (JumpCount == 0) && !Input.GetKey(KeyCode.DownArrow))//무기 돌리기
        {
            //Invoke("Spin", 0.5f);//0.6
            //LVlong.SetActive(true);
            animator.SetBool("ALong", true);
            AAD = true;
            //SlidC = true;
        }*/
    }

    public void BackStep()//백스텝
    {
        if(Input.GetKeyDown(KeyCode.X)&&(JumpCount == 0) && !Input.GetKey(KeyCode.DownArrow)&&(!BSD))
        {
            Vector3 moveVelocity2 = Vector3.zero;
            SlidC = false;
            //rigid2D.velocity = Vector3.zero;//공중에서 이거 쓰면 중력속도 0이됨
            rigid2D.AddForce(transform.right * -200 * key, ForceMode2D.Impulse);
            //transform.position += moveVelocity2 * -200; //* key;
            //transform.Translate(new Vector3(-5 * key, 0, 0));
            //transform.position = new Vector3(-5 * key, 0, 0);
            BSD = true;//백스텝방지
            Invoke("BSDDown",0.4f);
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.X) &&(BSD)) rigid2D.velocity = Vector3.zero;//백스텝중 아무키나 누르면 밀림 사라짐
    }

    void BSDDown()
    {
        BSD = false;
    }

    public void UseSubWeapon()//보조무기사용
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            switch (WCount)
            {
                case 1://나이프
                    if(Weapon > 0)
                    {
                        Weapon -= 1;//나이프 1게이지 감소
                        Instantiate(Knife, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    }
                    break;
                case 2:
                    if(Weapon > 2 && !Wok)
                    {
                        Wok = true;
                        Invoke("WokDelay", 0.5f);
                        Weapon -= 3;//도끼 3게이지 감소
                        Instantiate(AX, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    }
                    break;
                case 3:
                    if(Weapon > 4 && !Wok)
                    {
                        Wok = true;
                        Invoke("WokDelay", 0.5f);
                        Weapon -= 5;//십자가 5게이지 감소
                        Instantiate(CROSS, new Vector2(gameObject.transform.position.x + key*2, gameObject.transform.position.y), Quaternion.identity);
                    }
                    break;
                case 4:
                    if(Weapon > 2)
                    {
                        Weapon -= 3;//카레 3게이지 감소
                        Instantiate(Cury, new Vector2(gameObject.transform.position.x + key, gameObject.transform.position.y + 1), Quaternion.identity);
                    }
                    break;
                case 5:
                    if(Weapon > 29 && Health < 1000)
                    {
                        eat = true;
                        Weapon -= 30;//포크찹 30게이지 감소
                        Health += 130;
                        if (Health > 1000) Health = 1000;
                    }
                    break;
            } 
        }
    }

    public void Weaponchange()//무기교체
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            WCount++;
            if (WCount == 6) WCount = 1;//초과방지
        }
    }

    IEnumerator WeaponReturn()//10초 마다 마력 회복
    {
        if (!end)//끝나지않을 경우만
        {
            Weapon += 3;//5
            if (Weapon > 100) Weapon = 100;
            yield return new WaitForSeconds(5f);
            StartCoroutine(WeaponReturn());
        }
        
    }

    void WokDelay()//서브웨폰(십자가,도끼)딜레이
    {
        Wok = false;
    }

    void MoveStop()
    {
        if(Health <= 0)//패배
        {
            animator.SetTrigger("Dead");
            Health = 0;
            end = true;
            animator.SetBool("Jump", false);
            animator.SetBool("Jump2", false);
            animator.SetBool("SideKick", false);
            animator.SetBool("Kick", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Sid", false);
            animator.SetBool("Stand", true);
        }
        if (Marx.BossHealth == 0)//승리
        {
            end = true;
            animator.SetBool("Walk", false);
        }
    }
}
