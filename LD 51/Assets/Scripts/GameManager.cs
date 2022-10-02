using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.self.hp <= 0)
        {
            canvas.SetActive(true);
            Time.timeScale = 0.2f;
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
        canvas.SetActive(false);
    }
}
