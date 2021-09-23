using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void switchToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void switchTo2()
    {
        SceneManager.LoadScene(1);
    }

    public void switchTo4()
    {
        SceneManager.LoadScene(2);
    }

    public void switchToSmall()
    {
        SceneManager.LoadScene(3);
    }
}
