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
    AudioSource ����;
    [SerializeField] AudioClip[] ac;
    [SerializeField] float Speed = 7.0f;
    [SerializeField] float JumpPower = 1200.0f;
    [SerializeField] Slider HpBar;
    [SerializeField] Image ����Ϲ�;
    [SerializeField] Vector3 �����첾;
    [SerializeField] float �����d��;
    [SerializeField] LayerMask �P�w�ϼh;
    [SerializeField] GameObject �����ĪG;
    [SerializeField] GameObject �����I;
    [SerializeField] CinemachineVirtualCamera ������v��;
    public bool �i����;
    int Hp = 100;//�ͩR��
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerR = GetComponent<Rigidbody2D>();//���oRigidbody2D���ܼ�
        PlayerImage = GetComponent<SpriteRenderer>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerBody = GetComponent<CapsuleCollider2D>();
        PlayerFoots = GetComponent<BoxCollider2D>();
        ���� = GetComponent<AudioSource>();
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
        if (!�i����)
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerAnimator.SetTrigger("����");
        }
    }
    public void �����P�w()
    {
        Vector3 ������m = transform.position;//���o���a��m
        if (PlayerImage.flipX == false)//��V���k
            ������m += transform.right * �����첾.x;
        else
            ������m += transform.right * -�����첾.x;//������m�n�ۤ�
        Collider2D �Q���� = Physics2D.OverlapCircle(������m, �����d��, �P�w�ϼh);

        if (�Q���� != null)
        {
            �����I.transform.position = �Q����.transform.position + Vector3.up;
            Instantiate(�����ĪG, �����I.transform);//�ͦ�����
            StartCoroutine(�_����v��());
            ����.PlayOneShot(ac[3]);
            if (�Q����.gameObject.tag == "�ĤH1")
            {
                Debug.Log("������");
                �Q����.GetComponent<�ĤH1>().Damage(30);
            }
        }
    }
    IEnumerator �_����v��()
    {
        ������v��.Priority = 20;//�����쥻�u����
        yield return new WaitForSeconds(0.3f);//�Ȱ�0.3��
        ������v��.Priority = 5;//�ܦ^�쥻�u����
    }
    private void Hurt()
    {
        if(PlayerBody.IsTouchingLayers(LayerMask.GetMask("�ĤH")))
        {
            //���˰ʵe
            PlayerAnimator.SetTrigger("����");
            //�u�}
            if(PlayerImage.flipX == false)//�y�¥k
            {
                PlayerR.AddForce(Vector2.left * 1000f);
            }
            else
            {
                PlayerR.AddForce(Vector2.right * 1000f);
            }
            //����
            ����.PlayOneShot(ac[2]);
            //����
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
            Debug.Log("���`");
        }
    }

    private void Jump()
    {
        if(!PlayerFoots.IsTouchingLayers(LayerMask.GetMask("���x")))//���I���쥭�x
        {
            return;
        }
        if(Input.GetButtonDown("Jump"))
        {
            //PlayerR.AddForce(new Vector2(0f,1f));
            PlayerR.AddForce(Vector2.up*JumpPower);
            PlayerAnimator.SetBool("��",true);                               //���q
            AudioSource.PlayClipAtPoint(ac[1], Camera.main.transform.position,0.3f);
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
            PlayerR.linearVelocity = new Vector2(h * Speed, PlayerR.linearVelocity.y);
        if (h > 0)//�¥k(-����V)
        {
            PlayerImage.flipX = false;
            PlayerAnimator.SetBool("��",true);
        }
        else if (h < 0)//�¥�
        {
            PlayerImage.flipX = true;
            PlayerAnimator.SetBool("��", true);
        }
        else if (h == 0)//���m
        {
            PlayerAnimator.SetBool("��", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerFoots.IsTouchingLayers(LayerMask.GetMask("���x")))//���I���쥭�x
        {
            PlayerAnimator.SetBool("��", false);
        }
    }
    void ��������()
    {
        ����.volume = 0.2f;
        ����.PlayOneShot(ac[0]);
    }
}
