using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    public Rigidbody2D Puck;

    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;

    private Vector2 targetPosition;

    private bool isFirstTimeInOpponentHalf = true;
    private float offsetYFromTarget;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        startingPosition = rb.position;

        playerBoundary = new Boundary(
            PlayerBoundaryHolder.GetChild(0).position.y,
            PlayerBoundaryHolder.GetChild(1).position.y,
            PlayerBoundaryHolder.GetChild(2).position.x,
            PlayerBoundaryHolder.GetChild(3).position.x

            );


        puckBoundary = new Boundary(
            PuckBoundaryHolder.GetChild(0).position.y,
            PuckBoundaryHolder.GetChild(1).position.y,
            PuckBoundaryHolder.GetChild(2).position.x,
            PuckBoundaryHolder.GetChild(3).position.x

            );
    }

    // Update is called once per frame
    public void SetTargetPosition(float newX, float newY)
    {
        float clampedX = Mathf.Clamp(newX, playerBoundary.Left, playerBoundary.Right);
        float clampedY = Mathf.Clamp(newY, playerBoundary.Down, playerBoundary.Up);
        targetPosition = new Vector2(clampedX, clampedY);
    }

    void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            float movementSpeed;

            if (Puck.position.x > puckBoundary.Right)
            {
                if (isFirstTimeInOpponentHalf)
                {
                    isFirstTimeInOpponentHalf = false;
                    offsetYFromTarget = Random.Range(-1, 1);
                }

                movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
                SetTargetPosition(startingPosition.x, Puck.position.y + offsetYFromTarget);
            }
            else
            {
                isFirstTimeInOpponentHalf = true;

                movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
                SetTargetPosition(Puck.position.x, Puck.position.y);
            }

            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime));
        }
    }
    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
    public void UpdatePosition()
    {


        float movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, movementSpeed * Time.fixedDeltaTime));


    }
}