using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject OptionsMenuG;
    public GameObject MainMenuG; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptions()
    {
        MainMenuG.SetActive(false);
        OptionsMenuG.SetActive(true);
    }

    public void HideOptions()
    {
        MainMenuG.SetActive(true);
        OptionsMenuG.SetActive(false);
    }

    public void SetVolume(Slider slider)
    {
        AudioListener.volume = slider.value;
        Globals.volume = slider.value;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
