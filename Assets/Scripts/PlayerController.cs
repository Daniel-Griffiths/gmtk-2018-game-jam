﻿using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    // Gameobjects
    private GameObject shield;
    public GameObject mainCamera;
    private GameManager gameManager;

    // Misc
    private Rigidbody2D rb;
    private float vertical;
    private float horizontal;
    private int lives = 10;
    private Explodable _explodable;
    private SpriteRenderer shieldSprite;

    private float speed = 1.5f;
    private bool shieldActive = false;
    private bool playedHealthWarning = false;
    private bool playedEnergyWarning = false;

    // Energy
    private int energy = 10;
    private int energyMax = 10;
    private float energyDrainSpeed = 1f;
    private float energyRecoverySpeed = 0.75f;

    // Gui
    public Text livesText;
    public Text energyText;
    
    // Audio
    AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip warningSound;
    
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        shield = GameObject.FindGameObjectWithTag("Shield");
        shieldSprite = shield.GetComponent<SpriteRenderer>();
        _explodable = GetComponent<Explodable>();

        livesText.text = "Health: " + lives.ToString();
        energyText.text = "Energy: " + energy.ToString();

        InvokeRepeating("DrainEnergy", 0f, energyDrainSpeed);
        InvokeRepeating("RecoverEnergy", 0f, energyRecoverySpeed);
    }

    void FixedUpdate () {
        vertical = Input.GetAxisRaw("Vertical") / speed;
        horizontal = Input.GetAxisRaw("Horizontal") / speed;

        if (Input.GetKey("space") || Input.GetKey("joystick button 0")) {
            shieldActive = true;
            shield.SetActive(true);
            shield.SetActive(true);
        } else {
            shieldActive = false;
            shield.SetActive(false);

            // Move the shield super far off screen to prevent the issue 
            // where it would deflect bullets in it's old position for 
            // one frame when it respawned
            shield.transform.position = new Vector3(1000f, 1000f, 0);
        }

        if (energy == 0) {
            shieldActive = false;
            shield.SetActive(false);
        }

        // Update shield color
        float opacity = Mathf.Clamp(0.2f * (float)energy, .4f, 1f);
        shieldSprite.color = new Color(0f, 171f / 255, 1f, opacity);

        UpdateText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet" && lives > 0) {
            PlayerHit();
        }

        if (lives <= 0) {
            _explodable.explode();
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

        if (energy < 6 && energy > 4) {
            playedEnergyWarning = false;
            energyText.color = Color.yellow;
        }

        if (energy < 4) {
            PlayEnergyWarning();
            energyText.color = Color.red;
        }

        if (lives < 6) {
            livesText.color = Color.yellow;
        }

        if (lives < 4) {
            PlayHealthWarning();
            livesText.color = Color.red;
        }
    }

    void PlayHealthWarning() {
        if(playedHealthWarning == false) {
            gameManager.PlayAudio(warningSound);
            playedHealthWarning = true;
        }
    }

    void PlayEnergyWarning()
    {
        if (playedEnergyWarning == false) {
            gameManager.PlayAudio(warningSound);
            playedEnergyWarning = true;
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
