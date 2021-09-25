using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void switchToMenu()
    {
        switchScene(0);
    }

    public void switchTo2()
    {
        switchScene(1);
    }

    public void switchTo4()
    {
        switchScene(2);
    }

    public void switchToSmall()
    {
        switchScene(3);
    }

    private void switchScene(int i)
    {
        FindObjectOfType<AudioManager>().stopAll();
        SceneManager.LoadScene(i);
        FindObjectOfType<AudioManager>().levelMusic[i].source.Play();
    }
}
