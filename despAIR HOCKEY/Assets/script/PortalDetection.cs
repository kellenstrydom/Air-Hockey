using System;
using Unity.Mathematics;
using UnityEngine;

public class PortalDetection : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        float radRotation = (rb.rotation % 360) * Mathf.PI / 180;
        //Vector2 normalVec = new Vector2(Mathf.Cos(radRotation), Mathf.Sin(radRotation));
        
        Debug.Log($"{gameObject.name} - {radRotation}");
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        float radRotation = (rb.rotation % 360) * Mathf.PI / 180;

        Vector2 normalVec = new Vector2(Mathf.Cos(radRotation), Mathf.Sin(radRotation));
        GetComponentInParent<PortalControl>().Transport(tag, col.GetComponent<Rigidbody2D>());
    }
}
