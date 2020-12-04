using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCompensation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(90, 0, 0));
        transform.position = new Vector3(0, 1.5f, 2);
    }
}
