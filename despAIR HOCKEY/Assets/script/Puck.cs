using System;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    
    public bool p1HitLast;
    public bool p2HitLast;

    public float maxSpeed;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        ResetLastHit();
    }


    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        if (velocity.magnitude > maxSpeed) rb.velocity = velocity.normalized * maxSpeed;    
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("P1"))
        {
            if (p1HitLast) gameControl.DoubleTouch(1);
            p1HitLast = true;
            p2HitLast = false;
            sprite.color = col.gameObject.GetComponent<SpriteRenderer>().color;
        }

        if (col.gameObject.CompareTag("P2"))
        {
            if (p1HitLast) gameControl.DoubleTouch(2);
            p1HitLast = false;
            p2HitLast = true;
            
            GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Goal1")) gameControl.GoalScored(1);
        
        if (col.CompareTag("Goal2")) gameControl.GoalScored(2);

        if (col.CompareTag("Collectible"))
        {
            if (p1HitLast) gameControl.PickupCollectible(1);
            
            if (p2HitLast) gameControl.PickupCollectible(2);
        }
    }

    public void ResetLastHit()
    {
        sprite.color = Color.grey;
        p1HitLast = p1HitLast = false;
    }
    
}
