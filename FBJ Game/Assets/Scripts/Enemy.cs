using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform upPoint;
    public Transform downPoint;
    public Transform headPoint;
    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;
    public LayerMask layer;
    
    private Rigidbody2D body2D;
    private Animator anim;
    private bool colliding;

    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        body2D.velocity = new Vector2(speed, body2D.velocity.y);
        colliding = Physics2D.Linecast(upPoint.position, downPoint.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("die");
                boxCollider.enabled = false;
                circleCollider.enabled = false;
                body2D.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.5f);
            }
        }
    }
}