using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    Vector3 velocity;

    // Animation Controller
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x + transform.forward*z;
        bool isMove = CheckMove(move);
        // Debug.Log(isMove);

        animator.SetBool("IsMove", isMove);

        controller.Move(move * speed * Time.deltaTime);

        // jumping
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJump", true);
        } else if (isGrounded) {
            animator.SetBool("IsJump", false);
        }
        
        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    bool CheckMove (Vector3 move) {
        if (move.x != 0 || move.z != 0) {
            return true;
        }
        return false;
    }
}


// https://www.youtube.com/watch?v=_QajrabyTJc