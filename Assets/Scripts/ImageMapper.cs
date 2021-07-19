using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float largePuzzleScaleX = 1f;
    private float largePuzzleScaleY = 1f;
    private float smallPuzzleScaleX = 1.75f;
    private float smallPuzzleScaleY = 1.75f;
    private int largePuzzleRows = 5;
    private int largePuzzleCols = 6;
    private int smallPuzzleRows = 3;
    private int smallPuzzleCols = 4;
    public bool isLargePuzzle = false;

    private Vector2 currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world");

        GameObject piece = GameObject.Find("BoardImage");
        boardRend = piece.GetComponent<Renderer>();
        Material copy = boardImage;
        
        copy.mainTextureScale = new Vector2(1, 1);
        boardRend.material = copy;

        if (true)
        {
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    FourPuzzlePiecesImageMapper(i, j);
                    //FourPuzzleBoardImageMapper(i, j);
                }
            }

        }
       else if (isLargePuzzle)
        {
            for (int i = 1; i <= largePuzzleRows; i++)
            {
                for (int j = 1; j <= largePuzzleCols; j++)
                {
                    LargePuzzlePiecesImageMapper(i, j);
                    LargePuzzleBoardImageMapper(i, j);
                }
            }
            
        }
        else
        {
            for (int i = 1; i <= smallPuzzleRows; i++)
            {
                for (int j = 1; j <= smallPuzzleCols; j++)
                {
                    Debug.Log("small is triggered");
                    SmallPuzzlePiecesImageMapper(i, j);
                    SmallPuzzleBoardImageMapper(i, j);
                }
            }

        }
    }

    void LargePuzzlePiecesImageMapper(int i, int j)
    {
        GameObject piece = GameObject.Find(i + "," + j);
        Renderer rend = piece.GetComponent<Renderer>();
        float offsetXToAdd = 0.14f;

        if (i == 1)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.3f);
        }
        else if (i == 2)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.16f);
        }
        else if (i == 3)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.025f);
        }
        else if (i == 4)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), -0.115f);
        }
        else if (i == 5)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), -0.255f);
        }

    }

    void LargePuzzleBoardImageMapper(int i, int j)
    {
        GameObject board = GameObject.Find("Board" + i + "," + j);
        Renderer rend = board.GetComponent<Renderer>();
        float offsetXToAdd = 0.14f;

        if (i == 1)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.3f);
        }
        else if (i == 2)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.16f);
        }
        else if (i == 3)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), 0.025f);
        }
        else if (i == 4)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), -0.115f);
        }
        else if (i == 5)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(largePuzzleScaleX, largePuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-0.68f + ((j - 1) * offsetXToAdd), -0.255f);
        }

    }

    void SmallPuzzleBoardImageMapper(int i, int j)
    {
        GameObject piece = GameObject.Find("Board" + i + "," + j);
        Renderer rend = piece.GetComponent<Renderer>();
        float offsetXToAdd = 0.24f;

        if (i == 1)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.25f);
        }
        else if (i == 2)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.49f);
        }
        else if (i == 3)
        {
            rend.material = boardImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.73f);
        }

    }

    void SmallPuzzlePiecesImageMapper(int i, int j)
    {
        GameObject piece = GameObject.Find(i + "," + j);
        Renderer rend = piece.GetComponent<Renderer>();
        float offsetXToAdd = 0.24f;

        if (i == 1)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.25f);
        }
        else if (i == 2)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.49f);
        }
        else if (i == 3)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(smallPuzzleScaleX, smallPuzzleScaleY);
            rend.material.mainTextureOffset = new Vector2(-1.2f + ((j - 1) * offsetXToAdd), -0.73f);
        }
    }

        float length = 0.4f;
        void FourPuzzleBoardImageMapper(int i, int j)
        {
            GameObject piece = GameObject.Find("Board" + i + "," + j);
            Renderer rend = piece.GetComponent<Renderer>();
            float offsetXToAdd = 0.2f;

        Debug.Log(1/pieceImage.mainTexture.texelSize.x);
        Debug.Log(1/pieceImage.mainTexture.texelSize.y);


        if (i == 1)
            {
                rend.material = boardImage;
                rend.material.mainTextureScale = new Vector2(1/pieceImage.mainTexture.texelSize.x/40, 1 / pieceImage.mainTexture.texelSize.x/40);
                rend.material.mainTextureOffset = new Vector2(0.4f, 0.4f);
            }
            else if (i == 2)
            {
                rend.material = boardImage;
                rend.material.mainTextureScale = new Vector2(1, 1);
                rend.material.mainTextureOffset = new Vector2(0f, 0f);
            }

        }

        void FourPuzzlePiecesImageMapper(int i, int j)
        {
            GameObject piece = GameObject.Find(i + "," + j);
            Renderer rend = piece.GetComponent<Renderer>();
            float offsetXToAdd = 0.47f;
        float x = 3.5f;

        if (i == 1)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(x, x);
            rend.material.mainTextureOffset = new Vector2(-0.34f + ((j - 1) * offsetXToAdd), -0.47f);
        }
        else if (i == 2)
        {
            rend.material = pieceImage;
            rend.material.mainTextureScale = new Vector2(x, x);
            rend.material.mainTextureOffset = new Vector2(-0.34f + ((j - 1) * offsetXToAdd), -0.94f);
        }
    }

    



    // Update is called once per frame
    void Update()
    {

    }
}
