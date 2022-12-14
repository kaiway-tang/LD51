using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public static int tenSecTimer, score, difficulty;
    [SerializeField] SpriteRenderer tmrRend, scoreHundsRend, scoreTensRend, scoreOnesRend, vignette;
    [SerializeField] Sprite[] numbers;
    [SerializeField] Color alpha, vigColor;
    [SerializeField] int diff;
    [SerializeField] GameObject binaryClockObj;
    [SerializeField] Transform binaryClockScaler, scoreTrfm;
    int powerupActive = 0;
    AudioPlayer sound;

    bool passed1Hund;

    public static manager self;
    Vector3 scale;

    private void Awake()
    {
        self = GetComponent<manager>();
        sound = tmrRend.gameObject.GetComponent<AudioPlayer>();
    }
    void Start()
    {
        tenSecTimer = 500;
        alpha.a = .05f;
        tmrRend.color = alpha;
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
            alpha.a = .25f - tenSecTimer / 2500f;
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[tenSecTimer/50];
            difficulty++;
            diff = difficulty;
            if (tenSecTimer == 150)
            {
                sound.PlayKnife();  // Actually plays a beep, not the usual knife sound
                vigColor.a = .2f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
            if (tenSecTimer == 100)
            {
                sound.PlayKnife();  // See above
                vigColor.a = .4f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
            if (tenSecTimer == 50)
            {
                sound.PlayKnife();
                vigColor.a = .6f;
                vignette.color = vigColor;
                StartCoroutine(fade());
            }
        }
        if (tenSecTimer < 1)
        {
            CamController.setTrauma(15);
            alpha.a = .05f;
            tmrRend.color = alpha;
            tmrRend.sprite = numbers[10];
            tenSecTimer = 500;
        }
        if (Input.GetKey(KeyCode.Tab)) resetGame();
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
        if (score>99)
        {
            if (!passed1Hund)
            {
                passed1Hund = true;
                scoreTrfm.localPosition += new Vector3(0.7f, 0, 0);
            }
            scoreHundsRend.sprite = numbers[score / 100];
        }
        scoreTensRend.sprite = numbers[score % 100 / 10];
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
