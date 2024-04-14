using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(this.gameObject, 20);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
