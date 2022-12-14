using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    // [SerializeField] InputField input1;
    [SerializeField] TMP_InputField input0;
    [SerializeField] TMP_Text playerDisp;
    [SerializeField] TMP_Text scoreDisp;
    [SerializeField] TMP_Text highPlayer;
    [SerializeField] TMP_Text highScore;
    CanvasController canvas;
    bool inPostRequest = false;

    [SerializeField] string baseUrl = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdi3X-YFdFZAK4PQ7PKycQ7RJq9EMBpyG9k8lZ4t0bze9lyTA/formResponse";

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasController>();
    }

    // Sends post request to given url
    IEnumerator Post(string scoreCount, string playerName)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1826190743", playerName);
        form.AddField("entry.1165984352", scoreCount); 
        byte[] data = form.data;
        WWW www = new WWW(baseUrl, data);
        yield return www;
        canvas.Transition();
        inPostRequest = false;
    }

    public void Submit()
    {
        if (!inPostRequest)
        {
            inPostRequest = true;
            StartCoroutine(Post(manager.score.ToString(), input0.text.ToLower()));
        }
    }

    public void Retrieve()
    {
        Debug.Log("Attempting call");
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        Debug.Log("Getting");
        using (UnityWebRequest req = UnityWebRequest.Get("docs.google.com/spreadsheets/d/1XLYpqbOVAczZpyll9rSo69-HG7KwTBhyjc5dtYIhMHw/export?format=csv"))
        {
            Debug.Log("In progress");
            yield return req.SendWebRequest();
            string players = "";
            string scores = "";

            string topPlayers = "";
            string topScores = "";

            string[] response = req.downloadHandler.text.Split('\n');

            foreach (string line in response.Take(9))
            {
                string[] entry = line.Split(',');
                // elem 0 is time, 1 is name, 2 is score
                string name = entry[1];
                string score = entry[2];
                if (score == "Score")
                    continue;
                players += name + '\n';
                scores += score + '\n';
            }
            playerDisp.text = players;
            scoreDisp.text = scores;

            int len = response.Length;

            // One extra because ends in the categories
            for (int i = 0; i < Mathf.Min(10, len); i++)
            {
                string[] entry = response[len - 1 - i].Split(',');
                // elem 0 is time, 1 is name, 2 is score
                string name = entry[1];
                string score = entry[2];
                if (score == "Score")
                    continue;
                topPlayers += name + '\n';
                topScores += score + '\n';
            }

            highPlayer.text = topPlayers;
            highScore.text = topScores;
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
