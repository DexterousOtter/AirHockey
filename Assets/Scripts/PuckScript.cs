using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{

    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }

    public float maxSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update

    public AudioManager audioManager;

    public bool canTeleport = true;
    private bool AIHitPuckLast = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "AIGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                StartCoroutine(ResetPuck(false));
                WasGoal = true;
                audioManager.PlayGoal();
            }
            else if (other.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AIScore);
                StartCoroutine(ResetPuck(true));
                WasGoal = true;
                audioManager.PlayGoal();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        audioManager.PlayPuckCollision();
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AIHitPuckLast)
            {
                ScoreScriptInstance.Decrement(ScoreScript.Score.PlayerScore);
            }
            AIHitPuckLast = false;
        }
        else if (other.gameObject.CompareTag("AI"))
        {
            if (AIHitPuckLast)
            {
                ScoreScriptInstance.Decrement(ScoreScript.Score.AIScore);
            }

            AIHitPuckLast = true;
        }
    }


    private IEnumerator ResetPuck(bool didAIScore)
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = new Vector2(0, 0);

        if (didAIScore)
        {
            rb.position = new Vector2(1, 0);
        }
        else
        {
            rb.position = new Vector2(-1, 0);
        }
    }

    public void CenterPuck()
    {
        rb.position = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void Update()
    {
        if (!canTeleport)
        {
            StartCoroutine(EnableTeleportAfterDelay());
        }
    }
    IEnumerator EnableTeleportAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canTeleport = true;
    }
    public void Teleport()
    {
        canTeleport = false;

        AIScript aiScript = FindObjectOfType<AIScript>();

        aiScript.SetTargetPosition(transform.position.x, transform.position.y);
        aiScript.UpdatePosition();
    }
}

