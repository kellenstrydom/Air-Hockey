using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float distWhenConstSpeed;

    [SerializeField] private float radMultipleToStop;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    { 
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 vecBetweenMouse = mouseWorldPos - rb.position;
        Vector2 dirPaddle = vecBetweenMouse.normalized;
        
        // Debug.Log(vecBetweenMouse.magnitude);

        float radius = transform.localScale.x * 0.5f;
        float speed;

        bool inBoundsY = Mathf.Abs(mouseWorldPos.y) <= 4;
        bool inBoundsX = Mathf.Abs(mouseWorldPos.x + 9 * 0.5f) <= 9 * 0.5f;

        

        if (vecBetweenMouse.magnitude < 6) 
            speed = moveSpeed * Mathf.Pow(1.1f, vecBetweenMouse.magnitude * 2);
        else 
            speed = moveSpeed * Mathf.Pow(1.1f, 12);
        
        
        
        // x bounds
        if (rb.position.x >= 0 && dirPaddle.x > 0)
        {
            rb.position = new Vector2(0, rb.position.y);
            dirPaddle *= new Vector2(0, 1);
        }
        if (rb.position.x <= -9 + radius && dirPaddle.x < 0)
        {
            rb.position = new Vector2(-9 +radius, rb.position.y);
            dirPaddle *= new Vector2(0, 1);
        }
        
        // y bounds
        if (rb.position.y >= 4 - radius && dirPaddle.y > 0)
        {
            rb.position = new Vector2(rb.position.x, 4 - radius );
            dirPaddle *= new Vector2(1,0);
        }
        if (rb.position.y <= -4 + radius && dirPaddle.y < 0)
        {
            rb.position = new Vector2(rb.position.x, -4 + radius );
            dirPaddle *= new Vector2(1,0);
        }
        

        rb.velocity = dirPaddle * speed;

        // stop paddle close to mouse
        if (vecBetweenMouse.magnitude < radius * radMultipleToStop) rb.velocity = Vector2.zero;
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        rb.position = new Vector2(-6.5f, 0);
    }
}
