//Reference: https://answers.unity.com/questions/1033676/3rd-person-character-movement-relative-to-camera.html
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private enum PlayerNumber
    {
        PLAYER1,
        PLAYER2
    }

    [SerializeField]
    PlayerNumber playerNumber;

    string HorizontalAxis;
    string VerticalAxis;
    string SprintButton;
    string DashButton;
    string JumpButton;

    public float speed = 2f;
    public float runSpeed = 5f;
    public float turnSmoothing = 15f;

    bool dash = false;
    bool jump = false;
    bool isGrounded = false;

    float DASHFORCE = 6f;
    float JUMPFORCE = 6f;

    float JumpCooldown = 0.2f;
    float timer = 0f;

    private Vector3 movement;
    private Rigidbody playerRigidBody;

    void Awake()
    {
        if (playerNumber == PlayerNumber.PLAYER1)
        {
            HorizontalAxis = "Horizontal1";
            VerticalAxis = "Vertical1";
            SprintButton = "Sprint1";
            DashButton = "Dash1";
            JumpButton = "Jump1";
        }
        else
        {
            HorizontalAxis = "Horizontal2";
            VerticalAxis = "Vertical2";
            SprintButton = "Sprint2";
            DashButton = "Dash2";
            JumpButton = "Jump2";
        }

        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dash = false;
        if (Input.GetButtonDown(DashButton))
        {
            dash = true;
        }

        jump = false;
        if (Input.GetButtonDown(JumpButton) && CheckIfGrounded() && timer <= 0)
        {
            timer = JumpCooldown;
            jump = true;
        }

        timer -= Time.deltaTime;
        Debug.Log(timer);
    }

    void FixedUpdate()
    {
        float lh = Input.GetAxisRaw(HorizontalAxis);
        float lv = Input.GetAxisRaw(VerticalAxis);

        Move(lh, lv);

        if (dash == true)
        {
            Dash();
        }

        if (jump == true)
        {
            Jump();
        }
    }


    void Move(float lh, float lv)
    {
        movement.Set(lh, 0f, lv);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;


        if (Input.GetButton(SprintButton))
        {
            movement = movement.normalized * runSpeed * Time.deltaTime;
        }
        else
        {
            movement = movement.normalized * speed * Time.deltaTime;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + movement);


        if (lh != 0f || lv != 0f)
        {
            Rotating(lh, lv);
        }
    }


    void Rotating(float lh, float lv)
    {
        //Vector3 targetDirection = new Vector3(lh, 0f, lv);

        Quaternion targetRotation = Quaternion.LookRotation(movement, playerRigidBody.transform.up);


        Quaternion newRotation = Quaternion.Lerp(playerRigidBody.transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        playerRigidBody.MoveRotation(newRotation);
    }

    void Dash()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * DASHFORCE, ForceMode.Impulse);
        //GetComponent<Rigidbody>().velocity = transform.forward * 6f;
        //transform.position = new Vector3(1, 0, 1);
    }

    void Jump()
    {
        if ((Input.GetAxisRaw(HorizontalAxis) == 0) && Input.GetAxisRaw(VerticalAxis) == 0)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * JUMPFORCE, ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce((transform.up + transform.forward / 2) * JUMPFORCE, ForceMode.Impulse);
        }
    }

    bool CheckIfGrounded()
    {
        //Debug.Log("Button pressed, checking grounding...");
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, 1.1f)); // if it fails, slope is too incline, increase the number or reduce incline
        //Debug.Log("Grounding check was " + isGrounded);

        return isGrounded;
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.1f);
    }
}
