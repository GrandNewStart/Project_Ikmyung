using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private int lastDirection;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private string[] idleDirections = { "Idle_N", "Idle_NW", "Idle_W", "Idle_SW", "Idle_S", "Idle_SE", "Idle_E", "Idle_NE" };
    private string[] walkDirections = { "Walk_N", "Walk_NW", "Walk_W", "Walk_SW", "Walk_S", "Walk_SE", "Walk_E", "Walk_NE" };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = Vector2.down;
        SetDirection(movement);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        movement.y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        movement.Normalize();
        movement /= 10;

        SetDirection(movement);
        rb.MovePosition(rb.position + movement);
    }

    void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < 0.001f)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(direction);
        }

        animator.Play(directionArray[lastDirection]);
    }

    int DirectionToIndex(Vector2 direction)
    {
        Vector2 normalizedDir = direction.normalized;
        float step = 360 / 8;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normalizedDir);
        angle += offset;
        if (angle < 0f)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
