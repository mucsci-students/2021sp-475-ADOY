using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public int LevelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void LoadNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GameEnd()
    {
        FadeToLevel(0);
    }

    public void ReloadLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        animator.SetTrigger("FadeOut");
        PlayerPrefs.SetFloat("completed", 0);
        Application.Quit();
    }

    public void FadeToLevel(int levelIndex)
    {
        LevelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }


    // Update is called once per frame
    void Update()
    {
       // if(Input.GetMouseButtonDown(0))
       // {
       //     LoadNextLevel();
       // }

       if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
       {
           FadeToLevel(0);
       }
    }
    
}
