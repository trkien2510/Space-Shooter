using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    [Header("health")]
    private float currentHealth = 10f;
    private float maxHealth;
    private bool isDead = false;

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
        isDead = false;

        rb.velocity = new Vector2(-0.1f, -0.2f).normalized * fallSpeed;
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 1f * Time.fixedDeltaTime);
        if (transform.position.y <= -7f)
        {
            gameObject.SetActive(false);
        }
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
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
                float damage = bullet.bulletSize * ShipStats.Instance.GetBulletDamage();
                TakeDamage(damage);
            }
        }

        if (collision.CompareTag("Player"))
        {
            if (ShipStats.Instance.GetCurrentShield() > 0)
            {
                ShipStats.Instance.SetCurrentShield(ShipStats.Instance.GetCurrentShield() - 10f);
            }
            else
            {
                ShipStats.Instance.SetCurrentHealth(ShipStats.Instance.GetCurrentHealth() - 10f);
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
        GameManager.Instance.SetScore(GameManager.Instance.GetScore() + 10);
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
