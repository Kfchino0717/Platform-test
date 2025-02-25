using System;
using UnityEngine;

public class GoldenChest : MonoBehaviour
{
    bool CanOpen = false;
    bool Opened = false;
    Hint Hint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hint = GetComponentInChildren<Hint>();//由自己的子物件搜尋外部物件//放在群組裡!!
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }

    private void Open()
    {
        if(CanOpen && !Opened)
        {
            if (Input.GetButtonDown("Fire3")) 
            {
                GetComponent<Animator>().SetTrigger("開箱");
                GetComponent<AudioSource>().Play();
                Opened = true;
                Hint.CanHint = false;
                FindFirstObjectByType<Player>().可攻擊 = true;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if(Opened)
            {
                return;
            }
            CanOpen = true;
            Hint.CanHint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CanOpen = false;
            Hint.CanHint = false;
        }
    }
}
