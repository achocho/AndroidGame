using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventSystem : MonoBehaviour
{
    public GameObject PauseObject;
    public bool Paused=false;
    
   
    public void Retry() 
    {
        PlayerPrefs.SetInt("Score",0);
        SceneManager.LoadScene("Level");
        PlayerPrefs.SetInt("Revived", 0);
    }
    public void Quit() 
    {
        PlayerPrefs.SetInt("Score",0);
        PlayerPrefs.SetInt("Revived", 0);
        Application.Quit();
    }
    public void Pause() 
    {
        Paused = true;
        PauseObject.SetActive(true);
    }
    public void Resume() 
    {
        Paused = false;
        PauseObject.SetActive(false);
    }
    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
 
}
