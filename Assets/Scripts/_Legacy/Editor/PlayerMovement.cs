using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    public Camera mainCamera;
    Vector3 targetPlayerVelocity;

    float MOVSPEED = 2f;

    bool playerMovesForward = false;
    bool playerMovesBackward = false;
    bool playerMovesLeft = false;
    bool playerMovesRight = false;

    void Start()
    {
        player = this.gameObject;
        targetPlayerVelocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CheckInput();
        UpdateTargetPlayerVelocity();
    }

    void LateUpdate()
    {
        player.GetComponent<Rigidbody>().velocity = targetPlayerVelocity;
    }

    void CheckInput()
    {
        if (Input.GetAxis("Vertical") > 0.5)
        {
            playerMovesForward = true;
            playerMovesBackward = false;
        }
        else if (Input.GetAxis("Vertical") < -0.5)
        {
            playerMovesBackward = true;
            playerMovesForward = false;
        }
        else
        {
            playerMovesBackward = false;
            playerMovesForward = false;
        }

        if (Input.GetAxis("Horizontal") > 0.5)
        {
            playerMovesRight = true;
            playerMovesLeft = false;
        }
        else if (Input.GetAxis("Horizontal") < -0.5)
        {
            playerMovesLeft = true;
            playerMovesRight = false;
        }
        else
        {
            playerMovesLeft = false;
            playerMovesRight = false;
        }
    }

    void UpdateTargetPlayerVelocity()
    {
        if (playerMovesForward)
        {
            targetPlayerVelocity.z = transform.forward.z;
        }
        else if (playerMovesBackward)
        {
            targetPlayerVelocity.z = -MOVSPEED;
        }

        if (playerMovesRight)
        {
            targetPlayerVelocity.x = MOVSPEED;
        }
        else if (playerMovesLeft)
        {
            targetPlayerVelocity.x = -MOVSPEED;
        }

        //TODO: jump
    }
}
