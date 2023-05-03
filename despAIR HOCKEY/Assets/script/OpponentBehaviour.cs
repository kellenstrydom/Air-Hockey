using UnityEngine;
using Random = UnityEngine.Random;

public class OpponentBehaviour : MonoBehaviour
{
    [SerializeField] private Transform puck;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 startPos;
    //[SerializeField] bool isPuckInRange;


    private bool isGoingBase = false;
    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGoingBase)
        {
            Vector2 toStart = startPos - (Vector2)transform.position;
            rb.velocity = toStart.normalized * moveSpeed;

            if (toStart.magnitude < 0.5)
            {
                isGoingBase = false;
            }
            else
            {
                return;
            }
        }
        
        if (puck.position.x > 0)
        {
            Vector2 tarPos = puck.position;
            Vector2 dirToTarget = (tarPos - (Vector2)transform.position).normalized;
            rb.velocity = dirToTarget * (moveSpeed * Random.Range(0.4f,1));
        }
        else
        {
            Vector2 tarPos = new Vector2(startPos.x ,puck.position.y);
            Vector2 dirToTarget = (tarPos - (Vector2)transform.position).normalized;
            rb.velocity = dirToTarget * moveSpeed;
            
            if (transform.position.x >= startPos.x)
            {
                rb.velocity *= new Vector2(0,1);
            }
            if (Mathf.Abs(tarPos.y - transform.position.y) < 0.3 ) 
                rb.velocity *= new Vector2(1,0);

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("puck"))
            isGoingBase = true;
    }


    public void Reset()
    {
        rb.velocity = Vector2.zero;
        rb.position = new Vector2(6.5f, 0);
    }
}
