using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameController : MonoBehaviour
{
    public int numPuzzlePieces;
    public bool isVideo;
    private int placedPuzzlePieces = 0;
    private float timeSinceFinish = 0;
    private bool played = false;


    public CountdownTimer timer;
    public UnityEngine.Video.VideoPlayer video;
    public UnityEngine.UI.RawImage image;

    // Start is called before the first frame update
    void Start()
    {
        if (isVideo)
        {
            video.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
            video.frame = 0;
            video.loopPointReached += EndReached;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placedPuzzlePieces >= numPuzzlePieces)
        {
            FindObjectOfType<AudioManager>().Pause("Music");
            if (timer) timer.IndicateGameFinished();
            
            timeSinceFinish += Time.deltaTime;

            if (timeSinceFinish > 1 && !played)
            {
                if (isVideo)
                {
                    video.gameObject.SetActive(true);
                    video.Play();
                    image.gameObject.SetActive(true);
                } else
                {
                    FindObjectOfType<AudioManager>().Play("Finished");
                    StartCoroutine(WaitForSound());
                }
                played = true;
            }

            if (isVideo == true && timeSinceFinish > 5 && video.frame < 10)
            {
                video.Stop();
            } 
        }
    }

    public void PuzzlePiecePlaced()
    {
        placedPuzzlePieces++;
    }

    public void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.SetDirectAudioVolume(0, 0);
    }

    public IEnumerator WaitForSound()
    {
        yield return new WaitUntil(() => FindObjectOfType<AudioManager>().getSound("Finished").source.isPlaying == false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        FindObjectOfType<AudioManager>().Play("Music");
    }
}
