using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    float timeScale;
    [SerializeField]
    GameObject UI;

    public static bool paused = false;

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            UI.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }


    }
    public void UnPause()
    {
        paused = false;
        Time.timeScale = timeScale;
        UI.SetActive(false);
    }

    public void EndRun()
    {
        UnPause();
        SceneManager.LoadScene("Game");
    }
}
