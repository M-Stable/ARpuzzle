using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPieceEndController : MonoBehaviour
{
    public int numPuzzlePieces;
    private int placedPuzzlePieces = 0;
    private float timeSinceFinish = 0;
    private bool played = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (placedPuzzlePieces >= numPuzzlePieces)
        {
            FindObjectOfType<AudioManager>().Stop("Music");


            timeSinceFinish += Time.deltaTime;
            if (timeSinceFinish > 1 && !played)
            {
                FindObjectOfType<AudioManager>().Play("Finished");

                played = true;
            }
        }
    }

    public void PuzzlePiecePlaced()
    {
        placedPuzzlePieces++;
        Debug.Log(placedPuzzlePieces);
    }

}
