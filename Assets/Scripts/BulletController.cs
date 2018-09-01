﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    AudioSource audioSource;
    public string owner = "Enemy";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sprite.color = Color.red;

        // Destroy bullet after x seconds
        Invoke("DestroyBullet", 4f);

        // Make sure bullets cant collide with each other
        Physics2D.IgnoreLayerCollision(8, 8);
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

        // Yes, using the color is a really stupid idea
        if (collision.transform.tag == "Enemy" && owner == "Player") {
            DestroyBullet();
        }

        // Send the bullet back
        if (collision.transform.tag == "Shield") {
            audioSource.Play();
            sprite.color = Color.magenta;
            owner = "Player";
            rb.AddForce(new Vector2(Random.Range(-5f, 5f), 30f));
        }
    }
}
