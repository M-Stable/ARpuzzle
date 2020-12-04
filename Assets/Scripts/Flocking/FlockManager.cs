using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    CurveFollow[] pieces;
    Vector3[] offsets;

    public float speedModifier = 0.1f;
    public Transform[] routes;

    // Start is called before the first frame update
    void Start()
    {

        pieces = FindObjectsOfType<CurveFollow> ();
        Debug.Log(pieces.Length);
        if (pieces.Length == 12) {
            offsets = new Vector3[12];
            Debug.Log("Gets here");

            offsets[0] = new Vector3(0, 0, 0);
            offsets[1] = new Vector3(0, -0.1f, -0.35f);
            offsets[2] = new Vector3(0, 0.3f, 0.15f);
            offsets[3] = new Vector3(0, -0.25f, -0.25f);
            offsets[4] = new Vector3(0.4f, -0.2f, 0);
            offsets[5] = new Vector3(0, 0, 0.45f);
            offsets[6] = new Vector3(0.2f, -0.2f, 0.4f);
            offsets[7] = new Vector3(-0.2f, 0.35f, 0.3f);
            offsets[8] = new Vector3(-0.25f, -0.2f, 0.35f);
            offsets[9] = new Vector3(0.3f, 0.2f, 0);
            offsets[10] = new Vector3(0.1f, -0.3f, 0);
            offsets[11] = new Vector3(0.3f, 0.2f, 0.4f);
        } else {
            offsets = new Vector3[1];
            offsets[0] = new Vector3(0, 0, 0);
        }

        Shuffle();
        int index = 0;

        foreach (CurveFollow piece in pieces) {
            piece.offset = offsets[index];
            piece.routes = routes;
            piece.speedModifier = speedModifier;
            index++;
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < offsets.Length; i++)
        {
            int rnd = Random.Range(0, offsets.Length);
            Vector3 tempOffset = offsets[rnd];
            offsets[rnd] = offsets[i];
            offsets[i] = tempOffset;
        }
    }
}
