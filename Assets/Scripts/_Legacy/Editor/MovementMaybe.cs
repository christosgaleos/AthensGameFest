using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMaybe : MonoBehaviour
{
    float speedMeUp = 2f;
    Quaternion desiredRotation;

    //reading the input:
    float horizontalAxis;
    float verticalAxis;

    void Start()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }

    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        //ApplyMovement();
    }


    void CheckInput()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        if (verticalAxis > 0)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 2;
        }
        if (verticalAxis < 0)
        {
            GetComponent<Rigidbody>().velocity = -transform.forward * 2;
        }
        if (horizontalAxis > 0)
        {
            GetComponent<Rigidbody>().velocity = transform.right * 2;
        }
        if (horizontalAxis < 0)
        {
            GetComponent<Rigidbody>().velocity = -transform.right * 2;
        }
        if (verticalAxis == 0 && horizontalAxis == 0)
        {
            GetComponent<Rigidbody>().velocity = transform.position * 0;
        }
    }
    //Vector3 movement;

    void ApplyMovement()
    {
        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        //desiredRotation = Quaternion.Euler(transform.rotation.x, desiredMoveDirection.y, transform.rotation.z);

        //Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.Translate(desiredMoveDirection * speedMeUp * Time.deltaTime);
    }
}
