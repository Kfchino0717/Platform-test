using System;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public bool CanHint;

    SpriteRenderer Image;
    float x = 0f;
    float i = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanHint = false;
        Image = GetComponent<SpriteRenderer>();
        Image.enabled = CanHint;//ªì©l¤£¥i¨£
    }

    // Update is called once per frame
    void Update()
    {
        MakeHint();
    }

    private void MakeHint()
    {
        Image.enabled = CanHint;

        if (CanHint)
        {
            x += i;
            if (x > 1 || x < 0)
                i = -i;
            Image.color = new Color(1f, 1f, 1f, x);
        }
    }
}
