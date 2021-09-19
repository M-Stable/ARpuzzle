using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameController : MonoBehaviour
{
    public int numPuzzlePieces = 4;
    private int placedPuzzlePieces = 0;
    private float timeSinceFinish = 0;
    private bool played = false;

    public CountdownTimer timer;
    public UnityEngine.Video.VideoPlayer video;
    public UnityEngine.UI.RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        video.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        video.frame = 0;
        video.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        if (placedPuzzlePieces >= numPuzzlePieces)
        {
            timer.IndicateGameFinished();
            timeSinceFinish += Time.deltaTime;
            if (timeSinceFinish > 3 && !played)
            {
                FindObjectOfType<AudioManager>().Stop("Music");
                video.gameObject.SetActive(true);
                video.Play();
                image.gameObject.SetActive(true);
                played = true;
            }

            if (timeSinceFinish > 5 && video.frame < 10)
            {
                video.Stop();
            } 
        }
    }

    public void PuzzlePiecePlaced()
    {
        placedPuzzlePieces++;
        Debug.Log(placedPuzzlePieces);
    }

    public void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.SetDirectAudioVolume(0, 0);
    }
}
