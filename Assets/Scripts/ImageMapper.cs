using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Maps images on to board and pieces
 */
public class ImageMapper : MonoBehaviour
{
    public Texture2D pieceImage;
    public Shader bwShader;
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

        if (sceneName == "2PieceTabletop")
        {
            rows = 1;
            cols = 2;
            puzzleScaleX = 1.9f; //1.9
            puzzleScaleY = 4f; //3.8
            offsetXToAdd = -0.52f; //-1.167f
            vectorOffset = -1.2f;
            offsetArray = new float[] { -3f };

            GameObject piece1 = GameObject.Find("1,1");
            GameObject piece2 = GameObject.Find("1,2");
            piece1.transform.localRotation = Quaternion.identity;
            piece2.transform.localRotation = Quaternion.identity;

            GameObject piece = GameObject.Find("BoardImage");
            boardRend = piece.GetComponent<Renderer>();

            boardRend.material = new Material(bwShader);
            boardRend.material.mainTexture = pieceImage;
            boardRend.material.mainTextureScale = new Vector2(1, 1);
        }
        else if (sceneName == "4PiecePuzzle" || sceneName == "4PieceTabletop")
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

            boardRend.material = new Material(bwShader);
            boardRend.material.mainTexture = pieceImage;
            boardRend.material.mainTextureScale = new Vector2(1, 1);

        }
        else if (sceneName == "SmallPuzzle" || sceneName == "12PieceSlanted")
        {
            rows = 3;
            cols = 4;
            puzzleScaleX = 1.75f;
            puzzleScaleY = 1.75f;
            offsetXToAdd = 0.24f;
            offsetArray = new float[] { -0.25f, -0.49f, -0.73f };
            vectorOffset = -1.2f;
        }
        else
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
                if (sceneName != "4PiecePuzzle" || sceneName != "4PieceTabletop")
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
        
        rend.material = new Material(Shader.Find("Unlit/Texture"));
        rend.material.mainTexture = pieceImage;

        rend.material.mainTextureScale = new Vector2(puzzleScaleX, puzzleScaleY);
        rend.material.mainTextureOffset = new Vector2(vectorOffset + ((j - 1) * offsetXToAdd), offsetArray[i - 1]);
    }

    void puzzleBoardImageMapper(int i, int j)
    {
        GameObject board = GameObject.Find("Board" + i + "," + j);
        Renderer rend = board.GetComponent<Renderer>();
        rend.material = new Material(bwShader);
        rend.material.mainTexture = pieceImage;
        rend.material.mainTextureScale = new Vector2(puzzleScaleX, puzzleScaleY);
        rend.material.mainTextureOffset = new Vector2(vectorOffset + ((j - 1) * offsetXToAdd), offsetArray[i - 1]);
    }
}

