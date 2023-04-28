using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public int Damage = 1;
    public UnityEvent OnDamageDeal = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }

    private void DealDamage(Collider2D collision)
    {
        var damageble = collision.GetComponent<Damageble>();
        if (damageble == null) return;

        damageble.Hit(Damage);
        OnDamageDeal?.Invoke();
    }
}
