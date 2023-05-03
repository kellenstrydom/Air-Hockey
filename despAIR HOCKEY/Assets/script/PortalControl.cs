using System;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    private Transform portal1;
    private Transform portal2;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.rotation);
            if (child.CompareTag("Portal1")) portal1 = child;
            
            if (child.CompareTag("Portal2")) portal2 = child;
        }
    }

    public void Transport(string tag, Rigidbody2D rb)
    {
        Transform inPortal;
        Transform outPortal;

        if (tag == "portal1")
        {
            inPortal = portal1;
            outPortal = portal2;
        }
        else
        {
            inPortal = portal2;
            outPortal = portal1;
        }

        rb.position = outPortal.position;

        //rb.rotation = ;

    }
}
