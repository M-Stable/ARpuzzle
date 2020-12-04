using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 10f;
    public float finishTime = 0f;
    bool gameFinished = false;
 
    private TextMeshPro text; 

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        text = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameFinished)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime > 0.0f)
            {
                text.text = currentTime.ToString("0");
            }
            else
            {
                text.text = "Keep Going!";
            }
        }
    }

    public void IndicateGameFinished()
    {
        finishTime = currentTime;
        gameFinished = true;
        if (finishTime > 0)
        {
            text.text = "Congratulations!";
        }
        else
        {
            text.text = "Good Job! Try going faster next time.";
        }
    }
}
