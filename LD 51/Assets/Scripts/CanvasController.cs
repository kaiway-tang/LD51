using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
