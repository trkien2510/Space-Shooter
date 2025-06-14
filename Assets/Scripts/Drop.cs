using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2f;
    public string itemType;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    private void Update()
    {
        if (transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShipStats.Instance.AddPower(itemType);
            Destroy(gameObject);
        }
    }
}
