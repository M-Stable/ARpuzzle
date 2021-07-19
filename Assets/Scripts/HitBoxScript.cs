using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This script is applied to the Hitboxes
 * Positions a piece if it has collided with its correct hitbox
 * and it changes the piece's halo colour to green
 */
public class HitBoxScript : MonoBehaviour
{

    public float PiecePositionX = 0;
    public float PiecePositionY = 0;
    public float PiecePositionZ = 0;
    public int id = -1;
    public EndOfGameController controller;
    private bool hasPieceBeenDropped = false;
    public static bool isInsideHitbox = false;
    public float offset = 0.075f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    /**
     * This is triggered when a piece is inside a hitbox
     */
    private void OnTriggerStay(Collider coll)
    {
        if (coll.name == "Quad")
        {
            return;
        }
Debug.Log(gameObject.name + " IS HITTING hitbox" + coll.name + " and " + hasPieceBeenDropped);
        Debug.Log(Equals("hitbox" + coll.name, gameObject.name));
        isInsideHitbox = true;
        // Checks if the hitbox corresponds to piece and the piece hasn't been dropped
        if ("hitbox" + coll.name == gameObject.name && !hasPieceBeenDropped)
        {
            
            bool isPieceDropped = coll.gameObject.GetComponent<PositionOnBoard>().droppedPiece;
            coll.gameObject.GetComponent<PositionOnBoard>().yellowHalo.enabled = false;
            coll.gameObject.GetComponent<PositionOnBoard>().greenHalo.enabled = true;

            if (isPieceDropped)
            {
                // Place piece in correct position
                Vector3 finalPos = transform.position;
                finalPos.z -= offset;
                coll.gameObject.transform.position = finalPos;
                hasPieceBeenDropped = true;
                controller.PuzzlePiecePlaced();
                coll.gameObject.GetComponent<PositionOnBoard>().pieceInPlace = true;
                coll.gameObject.GetComponent<PositionOnBoard>().greenHalo.enabled = false;
                coll.gameObject.GetComponent<BoxCollider>().enabled = false;

                Debug.Log("Piece is dropped" + id);
            }
            else
            {
                Debug.Log("Piece is being held " + id);
            }
        }
    }

    /**
     * This is triggered when the piece leaves the hitbox
     */
    private void OnTriggerExit(Collider coll)
    {
        coll.gameObject.GetComponent<PositionOnBoard>().yellowHalo.enabled = true;
        coll.gameObject.GetComponent<PositionOnBoard>().greenHalo.enabled = false;
    }
}
