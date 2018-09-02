using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    private GameObject player;
    public AudioClip shot;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FollowPlayer();

    }

    void FollowPlayer()
    {
        // warp to the player
        if (player == null) {
            gameObject.SetActive(false);
        } else {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.05f);
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        FindObjectOfType<GameManager>().PlayAudio(shot);
    }
}
