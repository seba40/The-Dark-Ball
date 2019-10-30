using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //variable for ball speed
    public float thrust; // the force of the jump
    private float moveInput; // detects left or right key when pressed
    private  Rigidbody2D ballRigidBody; // variable for rigid body (private to only access it from the script)
    private bool grounded; // ground variable changes from true or false when object enters or exits collision with the ground
    public float maxJumps; // variable for the maximum number of jumps allowed
    private float remainingJumps = 2; // variable for the number of jumps remaning
    
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>(); // access rigid body from script
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); // if I press right key moveInput = 1,left key = -1 
        ballRigidBody.velocity = new Vector2(moveInput * speed, ballRigidBody.velocity.y); // adds force to the ball movement in order for the player to move,doesn't affect y axis

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0) // checks if jumping key is pressed
        {
            ballRigidBody.AddForce(Vector2.up * thrust * 200);
            remainingJumps--; // -- decreases the amount of jumps so the player doesn't jump an infinite amount of times
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // function to detect when object enters in collision with ground
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            remainingJumps = maxJumps; // if the player is grounded the remaningJumps variable is equal to the maxJum
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // function to detect when object exits collision with ground
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }
    
}
