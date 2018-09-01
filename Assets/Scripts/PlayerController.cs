using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private float horizontal;
    private float vertical;
    private float speed = 1.5f;
    private int lives = 10;
    private int energy = 5;
    private int energyMax = 10;

    private Rigidbody2D rb;
    public Text livesText;
    public Text energyText;
    private GameObject shield;
    AudioSource audioSource;
    public AudioClip hitSound;
    public GameObject mainCamera;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        shield = GameObject.FindGameObjectWithTag("Shield");
        livesText.text = "Health: " + lives.ToString();
        energyText.text = "Energy: " + energy.ToString();

        InvokeRepeating("RecoverEnergy", 0f, 2f);
    }

    void FixedUpdate () {
        vertical = Input.GetAxisRaw("Vertical") / speed;
        horizontal = Input.GetAxisRaw("Horizontal") / speed;

        if (Input.GetKey("space") && energy > 0) {
            shield.SetActive(true);
        } else {
            shield.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && lives > 0) {
            PlayerHit();
        }

        if (lives <= 0) {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void PlayerHit()
    {
        audioSource.PlayOneShot(hitSound);

        lives--;
        livesText.text = "Health: " + lives.ToString();

        if (lives <= 5) {
            livesText.color = Color.yellow;
        }

        if (lives <= 3) {
            livesText.color = Color.red;
        }

        mainCamera.GetComponent<AnalogGlitch>().enabled = true;
        mainCamera.GetComponent<DigitalGlitch>().enabled = true;
        Invoke("ResetCamera", 0.5f);
    }

    void ResetCamera()
    {
        mainCamera.GetComponent<AnalogGlitch>().enabled = false;
        mainCamera.GetComponent<DigitalGlitch>().enabled = false;
    }

    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, vertical);
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }

    void RecoverEnergy()
    {
        if (energy < energyMax) {
            energy++;
            energyText.text = "Energy: " + energy.ToString();
        }
    }
}
