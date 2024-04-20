using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    private Vector2 playerMovementInput;

    public Collider2D feetTransform;
    public Rigidbody2D playerBody;
    public float speed;
    public float jumpForce;
    public float midairSpeed;
    private bool facingRight = true;


    // Update is called once per frame
    void Update()
    { 
        playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Physics2D.IsTouchingLayers(feetTransform))//Проверка на касание пола
        {
            MovementSpeed(speed);
        }
        else //Движение в воздухе
        {
            MovementSpeed(midairSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W)) /*|| /playerMovementInput == new Vector2 (0f,1f)*/)//Прыжок
        {
            if (Physics2D.IsTouchingLayers(feetTransform))//Проверка на касание пола
            {
                playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        //if (playerMovementInput == new Vector2(1f, 0f) && !facingRight)
        //{
        //    Flip();
        //}
        //else if (playerMovementInput == new Vector2(-1f,0f) && facingRight)
        //{
        //    Flip();
        //}
    }

    void MovementSpeed (float speed)
    {
        Vector2 MoveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector2(MoveVector.x, playerBody.velocity.y);
    }

    //private void Flip()
    //{
    //    // Switch the way the player is labelled as facing.
    //    facingRight = !facingRight;

    //    transform.Rotate(0f, 180f, 0f);
    //}
}
