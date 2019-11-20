using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator CreditsAnimator;
    public GameObject CreditsPanel;
    public GameObject[] MenuButtons;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].SetActive(false);
        }
        CreditsPanel.SetActive(true);
        CreditsAnimator.SetBool("IsActive", true);
    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
        CreditsAnimator.SetBool("IsActive", false);
        for (int i = 0; i < MenuButtons.Length; i++)
        {
            MenuButtons[i].SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
