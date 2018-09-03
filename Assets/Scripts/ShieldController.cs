using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    public AudioClip shot;
    private GameObject player;
    private const float shieldOffset = .05f; // How far the shield is above the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FollowPlayer();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<GameManager>().PlayAudio(shot);
    }

    void FollowPlayer()
    {
        // warp to the player
        if (player == null) {
            gameObject.SetActive(false);
        } else {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + shieldOffset);
        }
    }
}
