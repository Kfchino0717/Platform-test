using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class 敵人1 : MonoBehaviour
{
    Rigidbody2D Body;
    SpriteRenderer Image;
    bool MoveLeft = false;
    int Hp;
    [SerializeField] Slider 血量條;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Image = GetComponent<SpriteRenderer>();
        Hp = 100;
        血量條.value = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        MoveLeft = Image.flipX;
        if(MoveLeft)
        {
            Body.linearVelocity = Vector2.left * 2f;

        }
        else
        {
            Body.linearVelocity = Vector2.right * 2f;
        }
        
    }
    public void Damage(int number)
    {
        Hp -= number;
        血量條.value = Hp;
        Debug.Log(Hp);
        if (Hp<0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MoveLeft)//碰到觸發器就翻轉
        {
            Image.flipX = false;
        }
        else
        {
            Image.flipX = true;
        }
    }
}
