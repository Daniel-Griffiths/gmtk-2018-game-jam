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
    private int energy = 10;
    private int energyMax = 10;

    private Rigidbody2D rb;
    public Text livesText;
    public Text energyText;
    private GameObject shield;
    AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip warningSound;
    public GameObject mainCamera;
    private bool shieldActive = false;
    private GameManager gameManager;
    private bool playedWarning = false;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        shield = GameObject.FindGameObjectWithTag("Shield");
        gameManager = FindObjectOfType<GameManager>();
        livesText.text = "Health: " + lives.ToString();
        energyText.text = "Energy: " + energy.ToString();

        InvokeRepeating("RecoverEnergy", 0f, .5f);
        InvokeRepeating("DrainEnergy", 0f, 1f);
    }

    void FixedUpdate () {
        vertical = Input.GetAxisRaw("Vertical") / speed;
        horizontal = Input.GetAxisRaw("Horizontal") / speed;

        if (Input.GetKey("space") || Input.GetKey("joystick button 0")) {
            shieldActive = true;
            shield.SetActive(true);
        } else {
            shieldActive = false;
            shield.SetActive(false);
        }

        if (energy == 0) {
            shieldActive = false;
            shield.SetActive(false);
        }

        UpdateText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && lives > 0) {
            PlayerHit();
        }

        if (lives <= 0) {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }

    void PlayerHit()
    {
        audioSource.PlayOneShot(hitSound);

        lives--;
        livesText.text = "Health: " + lives.ToString();

        mainCamera.GetComponent<AnalogGlitch>().enabled = true;
        mainCamera.GetComponent<DigitalGlitch>().enabled = true;
        Invoke("ResetCamera", 0.5f);
    }

    void UpdateText()
    {
        if (energy > 5) {
            energyText.color = Color.white;
        }

        if (energy <= 5) {
            energyText.color = Color.yellow;
        }

        if (energy <= 3) {
            energyText.color = Color.red;
        }

        if (lives <= 5) {
            livesText.color = Color.yellow;
        }

        if (lives <= 3) {
            PlayWarning();
            livesText.color = Color.red;
        }
    }

    void PlayWarning() {
        if(playedWarning == false) {
            gameManager.PlayAudio(warningSound);
            playedWarning = true;
        }
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
        if (energy < energyMax && shieldActive == false && !Input.GetKey("space") && !Input.GetKey("joystick button 0")) {
            energy++;
            energyText.text = "Energy: " + energy.ToString();
        }
    }

    void DrainEnergy()
    {
        if (shieldActive && energy > 0) {
            energy--;
            energyText.text = "Energy: " + energy.ToString();
        }
    }
}
