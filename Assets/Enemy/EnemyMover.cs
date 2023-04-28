using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform Princess;
    public float Speed = 3f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Moving()
    {
        var directToGo = (Princess.transform.position - transform.position).normalized;
        rb2d.velocity = (Vector2)directToGo * Speed * Time.fixedDeltaTime;
    }
}
