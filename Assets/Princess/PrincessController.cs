using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : MonoBehaviour
{
    public PrincessData Stats;
    private Damageble damageble;

    private void Awake()
    {
        damageble = GetComponentInChildren<Damageble>();
        damageble.MaxHealth = Stats.MaxHp;
        damageble.Health = Stats.MaxHp;
    }

    public void Upgrade()
    {
        if (!ResourceController.Instance.RemoveResources(Stats.Cost.ToArray()))
            return;

        Stats = Stats.LevelUp();
        damageble.MaxHealth = Stats.MaxHp;
        damageble.Health = Stats.MaxHp;
    }
}
