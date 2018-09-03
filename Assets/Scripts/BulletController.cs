using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody2D rb;
    private string owner = "Enemy";
    private SpriteRenderer sprite;
    private const float bulletLifetime = 4f;
    private const int bulletCollisionLayer = 8;
    private const float bulletReflectionSpeed = 55f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Make the bullets red by default
        sprite.color = Color.red;

        // Destroy bullet after x seconds
        Invoke("DestroyBullet", bulletLifetime);

        // Make sure bullets cant collide with each other
        Physics2D.IgnoreLayerCollision(bulletCollisionLayer, bulletCollisionLayer);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kill the bullet if it hits the enemy
        if (collision.transform.tag == "Player" && owner == "Enemy") {
            DestroyBullet();
        }

        // Send the bullet back
        if (collision.transform.tag == "Shield") {
            owner = "Player";
            sprite.color = Color.magenta;
            // Add a random range to give the bullets a spread
            rb.AddForce(new Vector2(Random.Range(-5f, 5f), bulletReflectionSpeed));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Yes, using the color is a really stupid idea
        if (collision.transform.tag == "Enemy" && owner == "Player") {
            DestroyBullet();
        }
    }
}
