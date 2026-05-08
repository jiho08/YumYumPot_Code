using Code.Combat;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class WaterBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float damage;
    private GameObject owner;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // 물리 강제 세팅
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;

        // 콜라이더 강제 세팅
        var col = GetComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = 0.2f;

        // 🔥 렌더 강제 세팅 (핵심)
        sr.enabled = true;
        sr.color = Color.cyan;
        sr.sortingOrder = 999;

        // 스프라이트가 없으면 런타임 생성
        if (sr.sprite == null)
        {
            Texture2D tex = new Texture2D(8, 8);
            Color[] pixels = new Color[64];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.cyan;
            tex.SetPixels(pixels);
            tex.Apply();

            sr.sprite = Sprite.Create(
                tex,
                new Rect(0, 0, 8, 8),
                new Vector2(0.5f, 0.5f),
                8f
            );
        }
    }

    public void Launch(
        Vector2 dir,
        float speed,
        float dmg,
        float lifeTime,
        GameObject ownerObj
    )
    {
        owner = ownerObj;
        damage = dmg;

        // 🔥 위치 강제 보정 (Z 포함)
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            -1f
        );

        rb.linearVelocity = dir.normalized * speed;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 🔥 Scene/Game 둘 다에서 무조건 보이게 디버그
        Debug.DrawLine(
            transform.position,
            transform.position + Vector3.up * 0.5f,
            Color.red
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner != null && other.transform.IsChildOf(owner.transform))
            return;

        if (other.TryGetComponent<IDamageable>(out var d))
        {
            d.ApplyDamage(damage, transform.position);
            Destroy(gameObject);
        }
    }
}
