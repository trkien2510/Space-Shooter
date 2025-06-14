using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public List<Sprite> smallBullets = new List<Sprite>();
    public List<Sprite> mediumBullets = new List<Sprite>();
    public List<Sprite> largeBullets = new List<Sprite>();

    public int bulletSize;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private Animator anim;

    public void InitializeBullet(int size)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        anim.enabled = true;
        anim.SetBool("Explosion", false);
        bulletSize = size;
        SetBulletSprite();

        rb.velocity = transform.right * ShipStats.Instance.bulletSpeed;
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
            spriteRenderer.sprite = smallBullets[ShipStats.Instance.randBulletColor];
        }
        else if (bulletSize == 2)
        {
            spriteRenderer.sprite = mediumBullets[ShipStats.Instance.randBulletColor];
        }
        else if (bulletSize == 3)
        {
            spriteRenderer.sprite = largeBullets[ShipStats.Instance.randBulletColor];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(BulletExplosion());
        }
    }

    private IEnumerator BulletExplosion()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Explosion", true);
        capsuleCollider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        anim.enabled = false;
        capsuleCollider.enabled = true;
        SetBulletSprite();
        gameObject.SetActive(false);
    }
}
