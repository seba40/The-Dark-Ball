using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxJumps; // maximum number of jumps allowed
    public float speed; // ball speed
    public float thrust; // the force of the jump
    public int playerHealth = 100;
    public float secondsUntilRespawn;
    public PointCounter pointCounter;
    public int flashNumber;
    public ParticleSystem damageParticles;
    public GameObject retryButton;

    private float moveInput; // horizontal axis value for input
    private Rigidbody2D ballRigidBody; // rigid body (private to only access it from the script) for the player ball
    private Transform ballTransform;
    private float remainingJumps; // number of jumps remaning
    private int environmentDamage = 50;
    private int lethalDamage = 999;
    private bool isFacingRight = true;
    private IEnumerator damageEffectsLoop;



    void Start()
    {
        damageEffectsLoop = DamageEffects(flashNumber);
        ballRigidBody = GetComponent<Rigidbody2D>(); // access rigid body from script
        ballTransform = GetComponent<Transform>(); // access Transform component of the player
        remainingJumps = maxJumps; // initialize the remaining jumps

    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal"); // if I press right key moveInput = 1, left key = -1
        if (isFacingRight == true && moveInput < 0 || isFacingRight == false && moveInput > 0)
            isFacingRight = !isFacingRight;
        ballRigidBody.velocity = new Vector2(moveInput * speed, ballRigidBody.velocity.y); // adds force to the ball movement in order for the player to move,doesn't affect y axis
        ballTransform.localEulerAngles = new Vector3(ballTransform.localEulerAngles.x, isFacingRight == true ? 0 : 180, ballTransform.localEulerAngles.z);
        // AddForce for dash(impulse)

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0) // checks if jump key is pressed
        {
            ballRigidBody.velocity = new Vector2(ballRigidBody.velocity.x, 0);
            ballRigidBody.AddForce(Vector2.up * thrust * 200);
            remainingJumps--; // -- decreases the amount of jumps so the player doesn't jump an infinite amount of times
        }

        // if the y position is < -20 or > 20  the death variable in gamemanager script becomes true and the player dies and the game restarts
        if (playerHealth > 200)
            playerHealth = 200;
    }

    IEnumerator OnDeath(float time)
    {
        yield return new WaitForSeconds(time);

        retryButton.GetComponent<Canvas>().enabled = true;
    }

    IEnumerator DamageEffects(int flashNumber)
    {
        damageParticles.Play();
        for (int i=1; i<=flashNumber; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

            yield return new WaitForSeconds(0.1f);

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(0.15f);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) // function to detect when object enters in collision with ground
    {
        switch (collision.gameObject.tag)
        {
            case "ground":
                remainingJumps = maxJumps; // if the player is grounded the remaningJumps variable is equal to the maxJumps   
                break;
            case "environment":
                StopCoroutine(damageEffectsLoop);
                StartCoroutine(damageEffectsLoop);
                playerHealth -= environmentDamage;
                break;
            case "Lethal":
                StopCoroutine(damageEffectsLoop);
                StartCoroutine(damageEffectsLoop);
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
