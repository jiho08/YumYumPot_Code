using Code.Combat;
using UnityEngine;
using System.Collections;

public class PesticideConeArea : MonoBehaviour
{
    private float damage;
    private float duration;

    public void Initialize(float damage, float duration)
    {
        this.damage = damage;
        this.duration = duration;
        StartCoroutine(LifeRoutine());
    }

    IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var d))
        {
            d.ApplyDamage(damage * Time.deltaTime, transform.position);
        }
    }
}
