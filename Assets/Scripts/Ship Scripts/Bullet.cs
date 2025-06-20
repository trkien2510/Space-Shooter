using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public List<Sprite> smallBullets = new List<Sprite>();
    public List<Sprite> mediumBullets = new List<Sprite>();
    public List<Sprite> largeBullets = new List<Sprite>();

    public int bulletSize;
    public int bulletColor;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    public void InitializeBullet(int size)
    {
        anim.enabled = false;
        capsuleCollider.enabled = true;
        anim.SetBool("Explosion", false);
        bulletSize = size;
        bulletColor = PlayerPrefs.GetInt("BulletColorIndex", 0);
        SetBulletSprite();

        rb.velocity = transform.right * ShipStats.Instance.GetBulletSpeed();
    }

    private void Update()
    {
        if (transform.position.y >= 7f)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetBulletSprite()
    {
        if (bulletSize == 1)
        {
            spriteRenderer.sprite = smallBullets[bulletColor];
        }
        else if (bulletSize == 2)
        {
            spriteRenderer.sprite = mediumBullets[bulletColor];
        }
        else if (bulletSize == 3)
        {
            spriteRenderer.sprite = largeBullets[bulletColor];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            anim.enabled = true;
            capsuleCollider.enabled = false;
            StartCoroutine(BulletExplosion());
        }
    }

    private IEnumerator BulletExplosion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.2f);
        anim.enabled = false;
        SetBulletSprite();
        gameObject.SetActive(false);
    }
}
