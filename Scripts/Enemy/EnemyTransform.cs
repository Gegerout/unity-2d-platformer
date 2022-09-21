using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransform : MonoBehaviour
{
    public float speed = 2.5f;
    public float onPlayerSpeed = 10f;
    public float patrolDistance;
    public Transform groundDetection;
    public LayerMask whatIsGround;
    public float playerDistance;
    public bool isFlipped = false;
    private bool movingRight = false;
    bool isTouchingFront;
    Transform player;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) < playerDistance)
        {
            LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 movePos = Vector2.MoveTowards(rb.position, target, onPlayerSpeed * Time.fixedDeltaTime);
            rb.MovePosition(movePos);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            //RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, patrolDistance);
            isTouchingFront = Physics2D.OverlapCircle(groundDetection.position, patrolDistance, whatIsGround);

            if (isTouchingFront)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
