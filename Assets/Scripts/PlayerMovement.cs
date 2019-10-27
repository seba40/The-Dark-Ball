using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //variable for ball speed
    public float thrust; // the force of the jump
    private float moveInput; // detects left or right key when pressed
    private  Rigidbody2D ballRigidBody; // variable for rigid body (private to only access it from the script)

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>(); // access rigid body from script
    }

   
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); // if I press right key moveInput = 1,left key = -1 
        ballRigidBody.velocity = new Vector2(moveInput * speed, ballRigidBody.velocity.y); // adds force to the ball movement in order for the player to move,doesn't affect y axis
        if (Input.GetKeyDown(KeyCode.Space))
            ballRigidBody.AddForce(Vector2.up * thrust * 200);
    }
}
