using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody2D PlayerR;
    SpriteRenderer PlayerImage;
    Animator PlayerAnimator;
    CapsuleCollider2D PlayerBody;
    BoxCollider2D PlayerFoots;
    AudioSource 音源;
    [SerializeField] AudioClip[] ac;
    [SerializeField] float Speed = 7.0f;
    [SerializeField] float JumpPower = 1200.0f;
    [SerializeField] Slider HpBar;
    [SerializeField] Image 血條圖像;
    [SerializeField] Vector3 攻擊位移;
    [SerializeField] float 攻擊範圍;
    [SerializeField] LayerMask 判定圖層;
    [SerializeField] GameObject 攻擊效果;
    [SerializeField] GameObject 攻擊點;
    [SerializeField] CinemachineVirtualCamera 受傷攝影機;
    public bool 可攻擊;
    int Hp = 100;//生命值
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerR = GetComponent<Rigidbody2D>();//取得Rigidbody2D其變數
        PlayerImage = GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerBody = GetComponent<CapsuleCollider2D>();
        PlayerFoots = GetComponent<BoxCollider2D>();
        音源 = GetComponent<AudioSource>();
        HpBar.value = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Hurt();
        Attack();
    }

    private void Attack()
    {
        if (!可攻擊)
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerAnimator.SetTrigger("攻擊");
        }
    }
    public void 攻擊判定()
    {
        Vector3 攻擊位置 = transform.position;//取得玩家位置
        if (PlayerImage.flipX == false)//方向為右
            攻擊位置 += transform.right * 攻擊位移.x;
        else
            攻擊位置 += transform.right * -攻擊位移.x;//攻擊位置要相反
        Collider2D 被撞物 = Physics2D.OverlapCircle(攻擊位置, 攻擊範圍, 判定圖層);

        if (被撞物 != null)
        {
            攻擊點.transform.position = 被撞物.transform.position + Vector3.up;
            Instantiate(攻擊效果, 攻擊點.transform);//生成物件
            StartCoroutine(震動攝影機());
            音源.PlayOneShot(ac[3]);
            if (被撞物.gameObject.tag == "敵人1")
            {
                Debug.Log("攻擊到");
                被撞物.GetComponent<敵人1>().Damage(30);
            }
        }
    }
    IEnumerator 震動攝影機()
    {
        受傷攝影機.Priority = 20;//提高原本優先級
        yield return new WaitForSeconds(0.3f);//暫停0.3秒
        受傷攝影機.Priority = 5;//變回原本優先級
    }
    private void Hurt()
    {
        if(PlayerBody.IsTouchingLayers(LayerMask.GetMask("敵人")))
        {
            //受傷動畫
            PlayerAnimator.SetTrigger("受傷");
            //彈開
            if(PlayerImage.flipX == false)//臉朝右
            {
                PlayerR.AddForce(Vector2.left * 1000f);
            }
            else
            {
                PlayerR.AddForce(Vector2.right * 1000f);
            }
            //音效
            音源.PlayOneShot(ac[2]);
            //扣血
            Damage(5);
        }
    }

    public void Damage(int number)
    {
        Hp -= number;
        //Debug.Log(Hp);
        HpBar.value = Hp;
        if(Hp <= 0)
        {
            Debug.Log("死亡");
        }
    }

    private void Jump()
    {
        if(!PlayerFoots.IsTouchingLayers(LayerMask.GetMask("平台")))//當未碰撞到平台
        {
            return;
        }
        if(Input.GetButtonDown("Jump"))
        {
            //PlayerR.AddForce(new Vector2(0f,1f));
            PlayerR.AddForce(Vector2.up*JumpPower);
            PlayerAnimator.SetBool("跳",true);                               //音量
            AudioSource.PlayClipAtPoint(ac[1], Camera.main.transform.position,0.3f);
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
            PlayerR.linearVelocity = new Vector2(h * Speed, PlayerR.linearVelocity.y);
        if (h > 0)//朝右(-的方向)
        {
            PlayerImage.flipX = false;
            PlayerAnimator.SetBool("走",true);
        }
        else if (h < 0)//朝左
        {
            PlayerImage.flipX = true;
            PlayerAnimator.SetBool("走", true);
        }
        else if (h == 0)//閒置
        {
            PlayerAnimator.SetBool("走", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerFoots.IsTouchingLayers(LayerMask.GetMask("平台")))//當未碰撞到平台
        {
            PlayerAnimator.SetBool("跳", false);
        }
    }
    void 走路音效()
    {
        音源.volume = 0.2f;
        音源.PlayOneShot(ac[0]);
    }
}
