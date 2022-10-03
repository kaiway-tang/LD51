using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public static int tenSecTimer, score, difficulty;
    [SerializeField] SpriteRenderer tmrRend, scoreTensRend, scoreOnesRend, vignette;
    [SerializeField] Sprite[] numbers;
    [SerializeField] Color alpha, vigColor;
    [SerializeField] int diff;
    [SerializeField] GameObject binaryClockObj;
    [SerializeField] Transform binaryClockScaler;
    int powerupActive = 0;

    public static manager self;
    Vector3 scale;

    private void Awake()
    {
        self = GetComponent<manager>();
    }
    void Start()
    {
        tenSecTimer = 500;
    }

    public void applyPowerup()
    {
        powerupActive = 500;
        binaryClockObj.SetActive(true);
        scale = Vector3.one;
        binaryClockScaler.localScale = scale;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (powerupActive > 0) {
            powerupActive--;
            scale.x = powerupActive / 500f;
            binaryClockScaler.localScale = scale;
            if (powerupActive < 1)
            {
                binaryClockObj.SetActive(false);
            }
            if (tenSecTimer > 100) { tenSecTimer = 100; }         
        }
        tenSecTimer--;
        if (tenSecTimer % 50 == 0)
        {
            alpha.a = .25f - tenSecTimer / 2000f;
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[tenSecTimer/50];
            difficulty++;
            diff = difficulty;
            if (tenSecTimer == 150)
            {
                vigColor.a = .2f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
            if (tenSecTimer == 100)
            {
                vigColor.a = .4f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
            if (tenSecTimer == 50)
            {
                vigColor.a = .6f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
        }
        if (tenSecTimer < 1)
        {
            CamController.setTrauma(15);
            alpha.a = .01f;
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[10];
            tenSecTimer = 500;
        }
        if (Input.GetKey(KeyCode.Backspace)) resetGame();
    }

    IEnumerator fade()
    {
        while (vignette.color.a>0)
        {
            vigColor.a -= .01f;
            vignette.color = vigColor;
            yield return new WaitForSeconds(.04f);
        }
    }

    public static void resetGame()
    {
        score = 0; difficulty = 0;
        PlayerController.knivesLeft = 9;
        SceneManager.LoadScene("knife throw");
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
    public static float difficultyScaler(float start, int end = 1, float distribution = 100)
    {
        result = (1 - manager.difficulty / distribution) * start;
        //Debug.Log("ratio: " + manager.difficulty / distribution);
        if (result < end) return end;
        else return result;
    }
}
