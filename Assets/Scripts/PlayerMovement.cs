using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject dialogueCloude;

    Vector2 movement;

    public static bool isTalking;

    public static bool isQuest;
    public static bool isBuy1;
    public static bool isAction;


    private void Start()
    {
        //isTalking = true;
        //dialogueCloude.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shelf")
        {
            isAction = true;
            isTalking = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shelf")
        {
            isAction = false;
            isTalking = false;
        }
    }


    void Update()
    {


        if (!isTalking)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else if (isTalking)
        {
            movement.y = 0;
            movement.x = 0;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        CheckIdlePos();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void CheckIdlePos()
    {
        if (movement.y > 0.01)
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isFront", false);
            animator.SetBool("isBack", true);
        }

        if (movement.y < -0.01)
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", true);
        }

        if (movement.x < -0.01)
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isLeft", true);
        }

        if (movement.x > 0.01)
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", true);
        }
    }
}
