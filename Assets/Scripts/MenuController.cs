using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject menu;

    public bool muted = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ShowMenu();
        }
    }

    public void ShowMenu(){
        if(menu.activeSelf){
            menu.SetActive(false);
            ResumeGame();
        }else{
            menu.SetActive(true);
            PauseGame();
            GameObject audioToggle = GameObject.Find("AudioToggle");
            if(AudioListener.volume == 0){
                audioToggle.GetComponent<Toggle>().isOn = true;
            }
        }
    }
    public static void PauseGame ()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        AudioListener.pause = false;
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        AudioListener.pause = false;
    }

    public void MuteAudio(){
        if(muted){
            AudioListener.volume = 1;
            muted = false;
        }else{
            AudioListener.volume = 0;
            muted = true;
        }
    }

}
