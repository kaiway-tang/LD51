using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject tutorial;

    Anim knifeAnim;
    
    public enum Window
    {
        menu,
        credits,
        tutorial
    }
    
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        tutorial.SetActive(false);
        knifeAnim = GetComponent<Anim>();
    }

    public void ToggleDisplay(Window display)
    {
        menu.SetActive(display == Window.menu);
        credits.SetActive(display == Window.credits);
        tutorial.SetActive(display == Window.tutorial);
        knifeAnim.HideKnives();
    }

    public void ToggleDisplay(int disp)
    {
        Window display = (Window)disp;
        menu.SetActive(display == Window.menu);
        credits.SetActive(display == Window.credits);
        tutorial.SetActive(display == Window.tutorial);
        knifeAnim.HideKnives();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("knife throw");
    }

}
