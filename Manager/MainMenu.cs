using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator anim;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowOpt() 
    {
        anim.SetBool("Show", true);
    }

    public void HideOpt() 
    {
        anim.SetBool("Show", false);
    }
}
