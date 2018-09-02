using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject bullet;
    public bool lookAtPlayer = false;
    private GameObject player;
    private int health = 3;
    private GameObject shield;
    [Header("Lower is faster")]
    public float rateOfFire = .2f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); 
        InvokeRepeating("SpawnBullet", 0.0f, rateOfFire);
    }

    void SpawnBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), -30f));
    }

    void FixedUpdate()
    {
        if(player != null) {
            Vector3 v_diff = (player.transform.position - transform.position);

            if (lookAtPlayer) {
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(v_diff.y, v_diff.x) * Mathf.Rad2Deg + 90f);
            }
        }

        // chase the player
        float movementDistance = .2f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, 
           new Vector3(Random.Range(player.transform.position.x - 1f, player.transform.position.x + 1f), player.transform.position.y + .7f, player.transform.position.z), movementDistance);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet" && collision.transform.GetComponent<SpriteRenderer>().color == Color.magenta) {
            health--;
            if(health <= 0) {
                FindObjectOfType<GameManager>().IncreaseScore();
                Destroy(gameObject);
            }
        }
    }
}
