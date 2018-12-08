using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAI : MonoBehaviour {

    // State Tracker
    private string KingState; // Used to track which state the king is in
    /* State reference table:
     * idle
     *      == Initial state. King will remain in place and watch until the player come in range.
     * run_into_room
     *      == Transitional state from "idle" to "stand_at_attention", only involves moving from one spot to another.
     *
     * stand_at_attention
     *      == One of the two in-combat states. Will fire from a standing position, and jump at random intervals 
     * jumping
     *      == One of the two in-combat states. King is airborn. Will fire at randomized, but specified heights.
     *          Note: The set points for jump height include the following:
     *          - standing_headshot: Will connect with the player if they stand still, but will likely miss if they jump.
     *          - standing_miss: If the player is on the ground or at the peak of a jump, this will miss. Connects mid-jump.
     *          - peak: Approximately level with the peak of the player's jump. Misses the lower half of the jump and standing players.
     * 
     * victory_animation
     *      == Loops between two frames in celebration.
     * defeat_animation
     *      == Playes the death animation then removes the ability for the player to interact with the king.
     */
    
    // Constants
    private const int ShootingQueueMax = 1; // Upper limit on how quickly projectiles can be fired
    private const float ShootingChance = 50;//0.40f; // Probability For the king to shoot. Should shoot if random # is lower than ShootingChance
    private const float ShootingDelay = 2f;//0.75f; // Delay between shots that the king makes

    // Variables
    public float JumpHeight; // The height that the king should jump
    private bool IsGrounded; // If the king is grounded or not
    //private Time PrevJumpTime; // The timestamp for the last time the king jumped
    //private int TimeToNextJump; // Target length of time between jumps (this should be between 1 and 2 seconds
    private bool JumpTimeAugment; // Augments the legnth of time between jumps (will add 1 second (true) or 0 seconds (false))
    private int ShootingKey; // Used to limit the king's rate of fire. Function similarly to a Semaphore
    private bool JumpKey; // Used to prevent multiple instances of the king jumping being queued. Note: False == key available, True = key taken
    Rigidbody2D KingRigidbody; // Rigidbody reference to the king's Rigidbody
    public GameObject PlayerCharacter; // Object reference to the player character
    public GameObject Projectile; // The projectile that the king shoots

    // Shooting object references
    public GameObject peakObj;
    public GameObject StandMissObj;
    public GameObject standHeadObj;
    public GameObject groundedObj;

    public bool peakCheck;
    public bool StandMissCheck;
    public bool standHeadCheck;
    public bool groundedCheck;

    // Note on jumping: Range of time between jumps will be 

    // Initialization
    void Start () {
        KingState = "idle"; // King starts in the idle state
        IsGrounded = true; // King stars on the ground
        //TimeToNextJump = 1; // Initial jump will take 2 seconds to occour
        JumpTimeAugment = false; // Initially do not augment jump time
        ShootingKey = 0; // Initially, no shots have been fired
        KingRigidbody = GetComponent<Rigidbody2D>(); // Acquire the King's Rigidbody

        // Get the transforms for the shooting object references
        //peakTrans = standHeadObj.transform.;

        PlayerCharacter = GameObject.FindGameObjectWithTag("Player"); // Locate the player character
    }

    // Update is used to redirect to the code for the relevant state
    void Update() {
        // Check to see if there is an update in the king's state
        // Check if the player is in nearby
        if (KingState == "idle" && Vector2.Distance(this.transform.position, PlayerCharacter.transform.position) <= 10f)
        {
            Debug.Log("King: idle -> stand_at_attention");
            KingState = "stand_at_attention";
        }
        /*
        // Check if enough time has passed, and the king should jump
        else if (KingState == "stand_at_attention" && !IsGrounded) // and is not grounded
        {
            Debug.Log("King: stand_at_attention -> jumping");
            KingState = "jumping";  
        }
        else if (KingState == "jumping" && IsGrounded) // and is grounded
        {
            Debug.Log("King: jumping -> stand_at_attention");
            KingState = "stand_at_attention";
        }
        */

        // Check which state the King currently is in
        if (KingState == "idle")
            IdleAI();
        else if (KingState == "stand_at_attention")
            StandAI();
        else if (KingState == "jumping")
            JumpAI();
        else if (KingState == "victory_animation")
            ;//JumpAI();
    }

    /* * * * * * * * *\
     *   Idle Logic  *
    \* * * * * * * * */

    void IdleAI()
    {
        // Check to see if the player is within detection range
        if(PlayerCharacter/* is within X units of This*/)
        {
            // Move out of "idle" state
            // WIP NOTE: Either will move directly into stand_at_attention if already in place
            //   or will move into run_into_room if king doesn't start in-position
        }
    }

    /* * * * * * * * * *\
     *   Combat Logic  *
    \* * * * * * * * * */

    // Standing AI - Determine if the King should shoot, and if the king should jump
    void StandAI()
    {
        // -- Check to see if the king should shoot
        if (ShootingKey < ShootingQueueMax) // If there is a free ShootingKey to take
        {
            //Debug.Log("King running KingShootCheck()");
            KingShootCheckV2();
        }

        // -- Check if the King should jump
        // Notes:
        // Jump height for player is soft coded as float 340
        // Jump height for King should be at about 900 to match

        // Check if the king is grounded, and if there isn't a jump command queued
        //KingJumpCheck();
        //*
        if (IsGrounded == true && JumpKey == false) // Note: JumpKey == false means no jumping command has been queued
        {
            //JumpKey = true; // Take JumpKey
            //Debug.Log("King called to queue a jump");
            //IsGrounded = false;
            StartCoroutine(QueueKingJump());
        }
        // */
    }

    // Jumping AI - Determine if the king should shoot, and checks if the king is shooting within valid ranges
    void JumpAI()
    {
        // Check to see if the king is in standing_headshot, standing_miss, or peak shooting ranges
    }

    /*
    // King Jump Check - Checks to see if the king should jump
    void KingJumpCheck()
    {
        // Notes:
        // Jump height for player is soft coded as float 340
        // Jump height for King should be at about 900 to match
        
        // Check if the king is grounded, and if they should jump
        if (IsGrounded == true && JumpKey == false) // Note: JumpKey == false means no jumping command has been queued
        {
            JumpKey = true; // Take JumpKey
            Debug.Log("King called to queue a jump");
            IsGrounded = false;
            QueueKingJump(); //KingRigidbody.AddForce(new Vector2(0, JumpHeight));
        }
    }
    // */

    // Queue up a jump for the king. Should only allow one instance of this command to be queued at one time.
    IEnumerator QueueKingJump()
    {
        //Debug.Log("In QueueKingJump(). Note: IsGrounded == " + IsGrounded.ToString() + " | JumpKey == " + JumpKey.ToString());
        // Ensure that this is the only jump command queued
        if (IsGrounded == true && JumpKey == false)
        {
            JumpKey = true; // Take JumpKey

            // Set randomized delay on the jump
            int DelayDuration;
            DelayDuration = Random.Range(0, 2);

            /*
            // Add 1 second to the delay every other jump
            if (JumpTimeAugment) DelayDuration++; // If true, add 1 second
            JumpTimeAugment = !JumpTimeAugment; // Invert JumpTimeAugment
            */

            // Delay jump by the randomized amount
            //Debug.Log("King waits " + DelayDuration.ToString() + " seconds");
            yield return new WaitForSeconds(DelayDuration);

            // Have the king jump
            JumpKey = false; // Released JumpKey
            //Debug.Log("King jump occoured");
            IsGrounded = false;
            KingRigidbody.AddForce(new Vector2(0, JumpHeight));
        }
    }

    // King Shoot Check - Check to see if the king should shoot
    void KingShootCheck()
    {
        // Variables
        float KingHeight = KingRigidbody.transform.position.y;
        float peakHeight = peakObj.transform.position.y;
        float missHeight = StandMissObj.transform.position.y;
        float headHeight = standHeadObj.transform.position.y;
        float groundHeight = groundedObj.transform.position.y;

        // If there are no available keys
        if (ShootingKey >= ShootingQueueMax)
        {
            return;
        }

        //Debug.Log(");

        //Debug.Log("KingHeight: " + KingHeight);
        // -- Find random probability to determine if the king should shoot
        int RandomShotChance;
        RandomShotChance = Random.Range(0, 100); // 0 to 100 -> Represents 0% to 100%
        Debug.Log("Key: " + ShootingKey + " / " + ShootingQueueMax + "\n" +
            "Rand: " + RandomShotChance);
        if (RandomShotChance < ShootingChance) // If the randomized chance is lower than the staic chance to fire
        {
            // -- Check if the king is in a valid height range
            // If so, call to queue a shot

            // Check and set the height that the king should shoot at
            // Note: Ranges give about 0.25
            /*
    public bool peakCheck;
    public bool StandMissCheck;
    public bool standHeadCheck;
    public bool groundedCheck;
             */
            if (peakHeight - 0.25 <= KingHeight && RandomShotChance < ShootingChance * 2 && peakCheck == false) // peak; Half the chance to fire
            {
                Debug.Log("Shoot peak");
                peakCheck = true;
                StartCoroutine(QueueKingShot(peakObj.transform.position));
            }
            else if (missHeight - 0.125 <= KingHeight && KingHeight <= missHeight + 0.125 && StandMissCheck == false) // standing_miss
            {
                Debug.Log("Shoot miss");
                StandMissCheck = true;
                StartCoroutine(QueueKingShot(StandMissObj.transform.position));
            }
            else if (headHeight - 0.125 <= KingHeight && KingHeight <= headHeight + 0.125 && standHeadCheck == false) // standing_headshot
            {
                Debug.Log("Shoot headshot");
                standHeadCheck = true;
                StartCoroutine(QueueKingShot(standHeadObj.transform.position));
            }
            else if (KingHeight <= groundHeight + 0.25 && RandomShotChance < ShootingChance / 2 && (groundedCheck == false) || (groundedCheck == true)) // grounded; Half the chance to fire
            {
                Debug.Log("Shoot grounded");
                groundedCheck = true;
                StartCoroutine(QueueKingShot(groundedObj.transform.position));
            }
            else return;
            // End setting height of shot
        }
        // End randomization "if"
    }

    IEnumerator QueueKingShot(Vector3 shootingHeight/*, GameObject HeightObj*/)
    {
        // Ensure that there is an available key to take
        if (ShootingKey < ShootingQueueMax) // If the number of taken keys are less than the number of total keys that can be taken
        {
            ShootingKey++; // Take a key
            Debug.Log("King has taken ShotKey " + ShootingKey);

            // Shoot a projectile
            
            Instantiate(Projectile, shootingHeight /*KingRigidbody.position*/, Quaternion.Euler(new Vector3(0, 0, 180f)));
            Debug.Log("Pew pew");

            // Wait some time to return a key
            yield return new WaitForSeconds(ShootingDelay);

            // Aaaaa?
            ShootingKey++; // Take a key
            Debug.Log("King has taken ShotKey " + ShootingKey);

            /*
            Debug.Log("King has returned ShootingKey" + ShootingKey);
            ShootingKey--; // Return a key
            */
        }
    }

    void KingShootCheckV2()
    {
        // Variables
        float KingHeight = KingRigidbody.transform.position.y; // King's Y posision, but shorter
        float peakHeight = peakObj.transform.position.y; // Location that a "peak" shot will be fired from
        float missHeight = StandMissObj.transform.position.y; // Location that a "standing_miss" shot will be fired from
        float headHeight = standHeadObj.transform.position.y; // Location that a "standing_headshot" shot will be fired from
        float groundHeight = groundedObj.transform.position.y; // Location that a "grounded" shot will be fired from

        // -- Check if the height is valid
        // Peak
        if (peakHeight - 0.25 <= KingHeight && peakCheck == false) // peak
        {
            Debug.Log("Shoot peak");
            peakCheck = true;
            QueueKingShotV2(peakObj);
        }
        // standing_miss
        else if (missHeight - 0.125 <= KingHeight && KingHeight <= missHeight + 0.125
            && StandMissCheck == false)
        {
            Debug.Log("Shoot miss");
            StandMissCheck = true;
            QueueKingShotV2(StandMissObj);
        }
        // standing_headshot
        else if (headHeight - 0.125 <= KingHeight && KingHeight <= headHeight + 0.125
            && standHeadCheck == false)
        {
            Debug.Log("Shoot headshot");
            standHeadCheck = true;
            QueueKingShotV2(standHeadObj);
        }
        // grounded
        else if (KingHeight <= groundHeight + 0.25 && groundedCheck == false)
        {
            Debug.Log("Shoot grounded");
            groundedCheck = true;
            QueueKingShotV2(groundedObj);
        }
        else return;
    }

    private void QueueKingShotV2(GameObject HeightObj)
    {
        // Randomize chance to fire
        int RandomShotChance;
        RandomShotChance = Random.Range(0, 100); // 0 to 100 -> Represents 0% to 100%

        if (RandomShotChance < ShootingChance)
        {
            Instantiate(Projectile, HeightObj.transform.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            Debug.Log("Pew pew");
            return;
        }
        return;
    }

    /* * * * * * *\
     * * * * * * *
    \* * * * * * */

    // Check to see if the King has landed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Unused
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1)); // Shoot a ray into the ground

        // Confirm a collision with the ground
        if (collision.gameObject.layer == 8) // If the object collided with was on the ground layer
        {
            //Debug.Log("King has landed");
            IsGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter 2D fired");

        // If we enter one of the shooting range objects, and a key is checked out
        if (collision.tag == "KingShootingRange" && // If we entered a shooting range
            0 < ShootingKey && ShootingKey <= ShootingQueueMax) // If the ShootingKey count is between 0 and ShootingQueueMax
        {
            Debug.Log("King has returned ShootingKey" + ShootingKey);
            ShootingKey--; // Return a key
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter 2D fired");

        if (collision.tag == "KingShootingRange")
            Debug.Log("Tag entered KingShootingRange");

        if (collision.gameObject.name == "peak")
        {
            Debug.Log("Entered peak");
            peakCheck = false;
        }
        else if (collision.gameObject.name == "standing_miss")
        {
            Debug.Log("Entered standing_miss");
            StandMissCheck = false;
        }
        else if (collision.gameObject.name == "standing_headshot")
        {
            Debug.Log("Entered standing_headshot");
            standHeadCheck = false;
        }
        else if (collision.gameObject.name == "grounded")
        {
            Debug.Log("Entered grounded");
            groundedCheck = false;
        }
        else
            Debug.Log("Unspecified enter");

        return;
    }

    private void OnTriggerExit2D(Collider other)
    {
        if (other.tag == "KingShootingRange")
            Debug.Log("Tag left KingShootingRange");

        if (other.gameObject.name == "peak")
        {
            Debug.Log("Exited peak");
            peakCheck = false;
        }
        else if (other.gameObject.name == "standing_miss")
        {
            Debug.Log("Exited standing_miss");
            StandMissCheck = false;
        }
        else if (other.gameObject.name == "standing_headshot")
        {
            Debug.Log("Exited standing_headshot");
            standHeadCheck = false;
        }
        else if (other.gameObject.name == "grounded")
        {
            Debug.Log("Exited grounded");
            groundedCheck = false;
        }
        else
            Debug.Log("Unspecified exit");

        return;
    }

    /* * * * * * * * * * * * * *\
     *   Victory/Defeat Logic  *
    \* * * * * * * * * * * * * */


}
