using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed; // ball speed
    public float thrust; // the force of the jump
    private float moveInput; // horizontal axis value for input
    private Rigidbody2D ballRigidBody; // rigid body (private to only access it from the script) for the player ball
    public float maxJumps; // maximum number of jumps allowed
    private float remainingJumps; // number of jumps remaning
    public int playerHealth = 100;
    private int environmentDamage = 50;
    private int lethalDamage = 999;
    public float secondsUntilRespawn;
    public PointCounter pointCounter;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>(); // access rigid body from script
        remainingJumps = maxJumps; // initialize the remaining jumps
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); // if I press right key moveInput = 1, left key = -1 
        ballRigidBody.velocity = new Vector2(moveInput * speed, ballRigidBody.velocity.y); // adds force to the ball movement in order for the player to move,doesn't affect y axis

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0) // checks if jump key is pressed
        {
            ballRigidBody.velocity = new Vector2(ballRigidBody.velocity.x, 0);
            ballRigidBody.AddForce(Vector2.up * thrust * 200);
            remainingJumps--; // -- decreases the amount of jumps so the player doesn't jump an infinite amount of times
        }

        if (ballRigidBody.position.y < -20f || ballRigidBody.position.y > 20f)
        {
            playerHealth -= lethalDamage;
            StartCoroutine(OnDeath(secondsUntilRespawn));
        }
        // if the y position is < -20 or > 20  the death variable in gamemanager script becomes true and the player dies and the game restarts
        if (playerHealth > 200)
            playerHealth = 200;
    }

    IEnumerator OnDeath(float time)
    {
        yield return new WaitForSeconds(time);

        FindObjectOfType<Controller>().Reload();
    }

    private void OnCollisionEnter2D(Collision2D collision) // function to detect when object enters in collision with ground
    {
        switch (collision.gameObject.tag)
        {
            case "ground":
                remainingJumps = maxJumps; // if the player is grounded the remaningJumps variable is equal to the maxJumps   
                break;
            case "environment":
                playerHealth -= environmentDamage;
                break;
            case "Lethal":
                playerHealth -= lethalDamage;
                break;
        }

        if (playerHealth <= 0)
        {
            speed = 0;
            thrust = 0;
            StartCoroutine(OnDeath(secondsUntilRespawn));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // detects when the player collides with other triggers
    {
        if (other.tag == "Point")
        {
            pointCounter.pointCount += 1;
            playerHealth += 50;
            Destroy(other.gameObject);
        }
    }
}
