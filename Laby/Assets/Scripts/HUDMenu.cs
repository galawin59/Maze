using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDMenu : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void JeuDePair()
    {
        SceneManager.LoadScene("JeuDePair");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
