using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void switchToMenu()
    {
        FindObjectOfType<AudioManager>().stopAll();
        SceneManager.LoadScene(0);
        FindObjectOfType<AudioManager>().levelMusic[0].source.Play();
    }

    public void switchTo2()
    {
        FindObjectOfType<AudioManager>().stopAll();
        SceneManager.LoadScene(1);
        FindObjectOfType<AudioManager>().levelMusic[1].source.Play();
    }

    public void switchTo4()
    {
        FindObjectOfType<AudioManager>().stopAll();
        SceneManager.LoadScene(2);
        FindObjectOfType<AudioManager>().levelMusic[2].source.Play();
    }

    public void switchToSmall()
    {
        FindObjectOfType<AudioManager>().stopAll();
        SceneManager.LoadScene(3);
        FindObjectOfType<AudioManager>().levelMusic[3].source.Play();
    }
}
