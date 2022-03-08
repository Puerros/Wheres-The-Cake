using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{

    public GameObject Menu;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(!Menu.activeSelf);

            if (Menu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Player.GetComponentInChildren<Gun>().enabled = false;
                Player.BroadcastMessage("ChangeAlive", false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Player.GetComponentInChildren<Gun>().enabled = true;
                Player.BroadcastMessage("ChangeAlive", true);
            }
        }
        
    }

    public void Resume()
    {
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player.GetComponentInChildren<Gun>().enabled = true;
        Player.BroadcastMessage("ChangeAlive", true);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
