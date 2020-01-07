using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //variable for ball speed
    public float thrust; // the force of the jump
    private float moveInput; // detects left or right key when pressed
    private Rigidbody2D ballRigidBody; // variable for rigid body (private to only access it from the script)
    public float maxJumps; // variable for the maximum number of jumps allowed
    private float remainingJumps; // variable for the number of jumps remaning
    public int playerHealth = 100;
    private int environmentDamage = 50;
    private int lethalDamage = 999;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>(); // access rigid body from script
        remainingJumps = maxJumps;
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

        if (ballRigidBody.position.y < -20f)
        {
            playerHealth -= lethalDamage;
            StartCoroutine(OnDeath(3));
        }
        // if the y position is < or = to -10 the death variable in gamemanager script becomes true and the player dies and the game restarts

    }
    IEnumerator OnDeath(float time)
    {
        yield return new WaitForSeconds(time);

        FindObjectOfType<Controller>().Reload();
    }
    private void OnCollisionEnter2D(Collision2D collision) // function to detect when object enters in collision with ground
    {
        if (collision.gameObject.tag == "ground")
            remainingJumps = maxJumps; // if the player is grounded the remaningJumps variable is equal to the maxJumps            
        

        if (collision.gameObject.tag == "environment")
            playerHealth -= environmentDamage;
        

        if (collision.gameObject.tag == "Lethal")
            playerHealth -= lethalDamage;
    

        if (playerHealth <= 0)
        {
            speed = 0;
            thrust = 0;
            StartCoroutine(OnDeath(3));
        }
}     

    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Point")
        {
            Debug.Log("point collected");
            Destroy(other.gameObject);
        }
    }
}
