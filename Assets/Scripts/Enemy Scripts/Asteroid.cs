using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private float currentHealth = 10f;
    private float maxHealth;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    public void InitializeAsteroid()
    {
        float fallSpeed = Random.Range(0.5f, 1.5f);
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        anim.enabled = true;
        anim.SetBool("Explosion", false);

        float timer = Time.time/10f;
        maxHealth = 10f + timer;
        currentHealth = maxHealth;
        rb.velocity = new Vector2(-0.1f, -0.2f).normalized * fallSpeed;
    }

    private void Update()
    {
        if (transform.position.y <= -7f)
        {
            gameObject.SetActive(false);
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(AsteroidExplosion());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                float damage = bullet.bulletSize * ShipStats.Instance.bulletDamage;
                TakeDamage(damage);
            }
        }

        if (collision.CompareTag("Player"))
        {
            if (ShipStats.Instance.currentShield > 0)
            {
                ShipStats.Instance.currentHealth -= (ShipStats.Instance.currentShield - 10 >= 0) ? 0 : -(ShipStats.Instance.currentShield - 10);
                ShipStats.Instance.currentShield -= 10;
            }
            else
            {
                ShipStats.Instance.currentHealth -= 10;
            }
            TakeDamage(5f);
        }
    }

    private void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
    }

    private IEnumerator AsteroidExplosion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        AudioManager.Instance.PlayExplosionSFX();
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Explosion", false);
        anim.enabled = false;
        spriteRenderer.sprite = sprite;
        gameObject.SetActive(false);
    }
}
