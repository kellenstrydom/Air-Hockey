using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private Puck puckScript;
    
    private void Update()
    { 
        transform.Rotate(0,0,rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("puck")) return;
        Rigidbody2D puck = col.GetComponent<Rigidbody2D>();

        var velocity = puck.velocity;
        var rotation = transform.rotation;
        Vector2 newVelocity = velocity.magnitude * (rotation * Vector3.right).normalized;
        
        // Debug.Log($"new velocity {newVelocity}");
        puck.velocity = newVelocity;
        
        puckScript.ResetLastHit();
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
    }
}
