using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 targetPosition;
    GameObject player1;
    GameObject player2;

    float Y_OFFSET = 0f;
    // Use this for initialization
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        targetPosition = CalculatePlayerCenter();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = CalculatePlayerCenter();
        transform.LookAt(targetPosition);
    }

    Vector3 CalculatePlayerCenter()
    {
        Vector3 targetPosition = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y / 2) + Y_OFFSET, (player1.transform.position.z + player2.transform.position.z) / 2);

        return targetPosition;
    }
}
