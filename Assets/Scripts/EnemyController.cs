using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject bullet;
    protected GameObject player;
    protected GameObject shield;

    public Sprite[] sprites;
    public int health = 3;
    public float rateOfFire = .2f;
    public float followSpeed = .2f;
    public bool lookAtPlayer = false;

    // Use this for initialization
    protected void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); 
        InvokeRepeating("Fire", 0.0f, rateOfFire);
        RandomSprite();
    }

    protected void FixedUpdate()
    {
        if(player != null) {        
            if (lookAtPlayer) {
                Vector3 v_diff = (player.transform.position - transform.position);
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(v_diff.y, v_diff.x) * Mathf.Rad2Deg + 90f);
            }
            FollowPlayer();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Yes, using the color is a really stupid idea
        if (collision.transform.tag == "Bullet" && collision.transform.GetComponent<SpriteRenderer>().color == Color.magenta) {
            DestroyEnemy();
        }
    }

    protected void RandomSprite()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    protected virtual void Fire()
    {
        Vector2 bulletPosition = new Vector2(Random.Range(-5f, 5f), -30f);

        Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Rigidbody2D>()
            .AddForce(bulletPosition);
    }

    protected void FollowPlayer()
    {
        float movementDistance = followSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, 
           new Vector3(Random.Range(player.transform.position.x - 1f, player.transform.position.x + 1f), player.transform.position.y + .7f, player.transform.position.z), movementDistance);
    }

    protected void DestroyEnemy()
    {
        health--;
        if(health <= 0) {
            FindObjectOfType<GameManager>().IncreaseScore();
            Destroy(gameObject);
        }
    }
}
