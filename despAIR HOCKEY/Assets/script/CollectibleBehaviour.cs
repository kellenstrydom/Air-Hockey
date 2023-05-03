using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviour: MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float phaseTime;
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    private bool growingPhase;
    private float timeInPhase;
    private float timeAlive;

    private Transform icon;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        {
            icon = child;
            //Debug.Log($"child --- {child.name}");
        }
        
        timeInPhase = 0;
        timeAlive = 0;
        growingPhase = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime) Destroy(gameObject);
        
        icon.transform.Rotate(0,0,rotSpeed * Time.deltaTime);
        
        if (growingPhase)
        {
            timeInPhase += Time.deltaTime;

            if (timeInPhase >= phaseTime)
            {
                timeInPhase = phaseTime;
                growingPhase = false;
            }
        }
        else
        {
            timeInPhase -= Time.deltaTime;

            if (timeInPhase <= 0)
            {
                timeInPhase = 0;
                growingPhase = true;
            }
        }
        
        float t = timeInPhase / phaseTime;
        icon.transform.localScale = Vector3.Lerp(minScale, maxScale, t);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("puck")) return;
        int player = 0;
        if (col.gameObject.GetComponent<Puck>().p1HitLast) player = 1;
        if (col.gameObject.GetComponent<Puck>().p2HitLast) player = 2;

        Debug.Log($"player {player} hit the collectible");
        
        //col.gameObject.GetComponent<Puck>().GoalScored(player);
        
        Destroy(gameObject);
    }
}
