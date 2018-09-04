using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : EnemyController {
    protected void Fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-5f, -30f));
        Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(0f, -30f));
        Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(5f, -30f));
    }
}
