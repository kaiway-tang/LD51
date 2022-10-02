using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public static int tenSecTimer, score, difficulty;
    [SerializeField] SpriteRenderer tmrRend, scoreTensRend, scoreOnesRend;
    [SerializeField] Sprite[] numbers;
    [SerializeField] Color alpha;
    [SerializeField] int diff;

    public static manager self;

    private void Awake()
    {
        self = GetComponent<manager>();
    }
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
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[tenSecTimer/50];
            difficulty++;
            diff = difficulty;
        }
        if (tenSecTimer < 1)
        {
            alpha.a = .01f;
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[10];
            tenSecTimer = 500;
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            score = 0; difficulty = 0;
            PlayerController.knivesLeft = 9;
            SceneManager.LoadScene("knife throw");
        }
    }

    public void incrementScore(int amount)
    {
        if (PlayerController.self.hp < 1) return;
        score += amount;
        difficulty += 3;
        scoreTensRend.sprite = numbers[score / 10];
        scoreOnesRend.sprite = numbers[score % 10];
    }

    static float result;
    public static float difficultyScaler(float max, int min = 1, float distribution = 100)
    {
        result = (1 - manager.difficulty / distribution) * max;
        //Debug.Log("ratio: " + manager.difficulty / distribution);
        if (result < min) return min;
        else return result;
    }
}
