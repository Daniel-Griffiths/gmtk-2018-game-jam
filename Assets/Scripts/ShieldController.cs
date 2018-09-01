using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.05f);
    }

    void OnCollision2d(Collision2D collision)
    {
 
    }
}
