using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    [SerializeField] private GameObject particleDeath;

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
        if(!collision.CompareTag("Respawn"))
            Destroy(this.gameObject);
        Instantiate(particleDeath, transform.position, Quaternion.identity);
    }
}
