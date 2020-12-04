using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveFollow : MonoBehaviour
{
    [HideInInspector]
    public Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 waypointPosition;

    private Transform camera;

    [HideInInspector]
    public float speedModifier = 0.05f;

    bool coroutineAllowed;

    [HideInInspector]
    public Vector3 offset;

    [HideInInspector]
    public PositionOnBoard gestureInteraction;

    private bool flocking = true;
    private bool held = false;
    private bool waitingToRejoin = false;
    private bool firstIteration = true;
    [HideInInspector]
    public bool rejoining  = false;
    private float waitingTime = 0.0f;
    public float rejoinSpeed = 1.0f;

    public float rotationSpeed = 1.0f;
    public Vector3 nextR1;

    // Start is called before the first frame update
    void Start()
    {
        gestureInteraction = GetComponent<PositionOnBoard>();
        camera = Camera.main.transform;
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        nextR1 = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed) {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        var r1 = Random.Range(-0.2f, 0.2f);
        var r2 = Random.Range(-0.2f, 0.2f);
        var r3 = Random.Range(-0.2f, 0.2f);

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        Vector3 r2Vector = new Vector3(r1, r2, 0);

        p1 += nextR1;
        p2 += r2Vector;
        nextR1 = -1 * r2Vector;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            waypointPosition = Mathf.Pow(1 - tParam, 3) * p0 + 
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + 
                Mathf.Pow(tParam, 3) * p3;

            if (waitingToRejoin)
            {
                waitingTime += Time.deltaTime;
            }

            if (gestureInteraction.pieceInPlace)
            {
                // Don't do anything
            }
            else if (gestureInteraction.isHeld)
            {
                flocking = false;
                waitingToRejoin = false;
                rejoining = false;
                held = true;

            }
            else if (held && !gestureInteraction.isHeld)
            {
                waitingToRejoin = true;
                waitingTime = 0.0f;
                held = false;
            }
            else if (waitingToRejoin && waitingTime > 5.0f)
            {
                rejoining = true;
                waitingToRejoin = false;
                waitingTime = 0.0f;
            }
            else if (rejoining)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypointPosition + offset, 0.3f * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypointPosition  + offset) < 0.1f)
                {
                    rejoining = false;
                    flocking = true;
                }
            }
            else if (flocking)
            {               
                transform.position = waypointPosition + offset;
            }            
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }
}
