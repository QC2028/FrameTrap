using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    Vector3 newVector = Vector3.zero;

    void Update()
    {
        newVector = new Vector3((player2.transform.position.x + player1.transform.position.x) / 2, 0, -10);
        transform.position = newVector;
    }
}
