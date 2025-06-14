using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private float damage = 10f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    public void InitializeBullet()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.sprite = sprite;

        anim.enabled = true;
        anim.SetBool("Explosion", false);

        rb.velocity = transform.right * ShipStats.Instance.bulletSpeed;
    }

    void Update()
    {
        if (transform.position.y <= -7f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            capsuleCollider.enabled = false;
            if (ShipStats.Instance.currentShield > 0)
            {
                ShipStats.Instance.currentShield -= damage;
            }
            else
            {
                ShipStats.Instance.currentHealth -= damage;
            }
            StartCoroutine(BulletExposion());
        }
    }

    private IEnumerator BulletExposion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Explosion", false);
        anim.enabled = false;
        spriteRenderer.sprite = sprite;
        capsuleCollider.enabled = true;
        gameObject.SetActive(false);
    }
}
