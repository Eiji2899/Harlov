using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AllienScript1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction;
    public float speed;

    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag ("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
