using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject leaderboard;
    [SerializeField] GameObject restartPrompt;
    Leaderboard board;
    // Start is called before the first frame update
    void Start()
    {
        leaderboard.SetActive(true);
        restartPrompt.SetActive(false);
        board = GetComponent<Leaderboard>();
    }

    public void Transition()
    {
        leaderboard.SetActive(false);
        restartPrompt.SetActive(true);
        board.Retrieve();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
