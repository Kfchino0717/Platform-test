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
        Hint = GetComponentInChildren<Hint>();//�Ѧۤv���l����j�M�~������//��b�s�ո�!!
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
                GetComponent<Animator>().SetTrigger("�}�c");
                GetComponent<AudioSource>().Play();
                Opened = true;
                Hint.CanHint = false;
                FindFirstObjectByType<Player>().�i���� = true;
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
