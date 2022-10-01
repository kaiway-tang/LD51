using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public static int tenSecTimer;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] numbers;
    [SerializeField] Color alpha;
    void Start()
    {
        tenSecTimer = 500;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tenSecTimer--;
        if (tenSecTimer % 50 == 0)
        {
            alpha.a = .4f - tenSecTimer / 1250f;
            rend.color = alpha;
            rend.sprite = numbers[tenSecTimer/50];
        }
        if (tenSecTimer < 1)
        {
            alpha.a = .01f;
            rend.color = alpha;
            rend.sprite = numbers[10];
            tenSecTimer = 500;
        }
    }
}
