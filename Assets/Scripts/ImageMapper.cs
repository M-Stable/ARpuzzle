using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Maps images on to board and pieces
 */
public class ImageMapper : MonoBehaviour
{
    public Material pieceImage;
    public Material boardImage;
    private Renderer boardRend;
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 1.0f;
    private float puzzleScaleX;
    private float puzzleScaleY;
    private int rows = 0;
    private int cols = 0;

    private float offsetXToAdd;
    private float[] offsetArray;
    private float vectorOffset;

    private Vector2 currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "4PiecePuzzle")
        {
            rows = 2;
            cols = 2;
            puzzleScaleX = 3.5f;
            puzzleScaleY = 3.5f;
            offsetXToAdd = 0.47f;
            vectorOffset = -0.34f;
            offsetArray = new float[] { -0.47f, -0.94f };

            GameObject piece = GameObject.Find("BoardImage");
            boardRend = piece.GetComponent<Renderer>();
            Material copy = boardImage;

            copy.mainTextureScale = new Vector2(1, 1);
            boardRend.material = copy;
        } else if (sceneName == "SmallPuzzle")
        {
            rows = 3;
            cols = 4;
            puzzleScaleX = 1.75f;
            puzzleScaleY = 1.75f;
            offsetXToAdd = 0.24f;
            offsetArray = new float[] { -0.25f, -0.49f, -0.73f };
            vectorOffset = -1.2f;
        } else
        {
            rows = 5;
            cols = 6;
            puzzleScaleX = 1f;
            puzzleScaleY = 1f;
            offsetXToAdd = 0.14f;
            offsetArray = new float[] { 0.3f, 0.16f, 0.025f, -0.115f, -0.255f };
            vectorOffset = -0.68f;
        }

        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                puzzlePiecesImageMapper(i, j);
                if (sceneName != "4PiecePuzzle")
                {
                    puzzleBoardImageMapper(i, j);
                }
            }
        }
    }

    void puzzlePiecesImageMapper(int i, int j)
    {
        GameObject piece = GameObject.Find(i + "," + j);
        Renderer rend = piece.GetComponent<Renderer>();

        rend.material = pieceImage;
        rend.material.mainTextureScale = new Vector2(puzzleScaleX, puzzleScaleY);
        rend.material.mainTextureOffset = new Vector2(vectorOffset + ((j - 1) * offsetXToAdd), offsetArray[i - 1]);
    }

    void puzzleBoardImageMapper(int i, int j)
    {
        GameObject board = GameObject.Find("Board" + i + "," + j);
        Renderer rend = board.GetComponent<Renderer>();

        rend.material = boardImage;
        rend.material.mainTextureScale = new Vector2(puzzleScaleX, puzzleScaleY);
        rend.material.mainTextureOffset = new Vector2(vectorOffset + ((j - 1) * offsetXToAdd), offsetArray[i - 1]);
    }
}