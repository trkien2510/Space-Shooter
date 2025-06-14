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

    [Header("end of path idle timeout")]
    private float idleTime = 0f;
    private float maxIdleTime = 2f;
    private bool reachedEndOfPath = false;

    [Header("drop item")]
    public List<LootItem> lootItems = new List<LootItem>();

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

        if (path != null && path.Count > 0)
        {
            waypoints = new List<Transform>(path);
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
        if (useWaypoints && waypoints != null && currentWaypointIndex < waypoints.Count && waypoints[currentWaypointIndex] != null)
        {
            Vector3 target = waypoints[currentWaypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                currentWaypointIndex++;
            }

            reachedEndOfPath = false;
            idleTime = 0f;
        }
        else if (useWaypoints && (waypoints == null || currentWaypointIndex >= waypoints.Count))
        {
            if (!reachedEndOfPath)
            {
                reachedEndOfPath = true;
                idleTime = 0f;
            }

            idleTime += Time.deltaTime;
            if (idleTime >= maxIdleTime)
            {
                gameObject.SetActive(false);
            }
        }

        if (transform.position.y <= -10f || transform.position.x >= 15f || transform.position.x <= -15f)
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
                float damage = bullet.bulletSize * ShipStats.Instance.GetBulletDamage();
                TakeDMG(damage);
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
