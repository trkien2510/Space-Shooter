using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D circleCollider2D;

    [Header("sprite")]
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    [Header("health")]
    private float currentHealth = 20f;
    private float maxHealth;
    private bool isDead = false;

    [Header("waypoint movement")]
    private float speed = 1f;
    private List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    private bool useWaypoints = false;

    [Header("drop item")]
    public List<LootItem> lootItems = new List<LootItem>();
    private bool canDrop = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
    }

    public void InitializeEnemyShip(List<Transform> path = null)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if (circleCollider2D == null) circleCollider2D = GetComponent<CircleCollider2D>();

        rb.velocity = Vector2.zero;
        anim.enabled = true;
        anim.SetBool("Explosion", false);
        circleCollider2D.enabled = true;

        float timer = Time.time / 10f;
        maxHealth = 20f + timer;
        currentHealth = maxHealth;

        isDead = false;
        canDrop = true;

        if (path != null && path.Count > 0)
        {
            waypoints = path;
            currentWaypointIndex = 0;
            useWaypoints = true;
        }
        else
        {
            useWaypoints = false;
            rb.velocity = Vector2.down * speed;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (useWaypoints && waypoints != null && currentWaypointIndex < waypoints.Count)
        {
            Vector3 target = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }

        if (transform.position.y <= -7f || transform.position.x >= 10f || transform.position.x <= -10f)
        {
            gameObject.SetActive(false);
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
                TakeDMG(damage);
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
            TakeDMG(5f);
        }
    }

    private void TakeDMG(float dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        canDrop = false;
        DropItem();
        circleCollider2D.enabled = false;
        StartCoroutine(EnemyExplosion());
    }

    private IEnumerator EnemyExplosion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        AudioManager.Instance.PlayExplosionSFX();
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Explosion", false);
        anim.enabled = false;
        spriteRenderer.sprite = sprite;
        circleCollider2D.enabled = true;
        gameObject.SetActive(false);
    }

    private void DropItem()
    {
        foreach (LootItem lootItem in lootItems)
        {
            if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                if (lootItem.itemPrefab)
                {
                    Instantiate(lootItem.itemPrefab, transform.position, Quaternion.identity);
                }
                break;
            }
        }
    }
}
