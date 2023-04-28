using System.Linq;
using UnityEngine;

public class SwordUpgrader : MonoBehaviour
{
    public int CurrentLevel = 1;

    private SwordController swordController;
    private ResourceController playerResources;
    private DamageDealer damageDealer;

    private void Start()
    {
        swordController = GetComponentInChildren<SwordController>();
        playerResources = GetComponent<ResourceController>();
        damageDealer = GetComponentInChildren<DamageDealer>();
    }

    public void UpgradeOrbit(OrbitFlight orbit)
    {
        if (playerResources.RemoveResources(orbit.OrbitStats.Cost.ToArray()))
            orbit.Upgrade();
    }

    public void UpgradeSpeedSword()
    {
        swordController.Upgrade();
        CurrentLevel++;
    }
    
    public void UpgradeDamageSword()
    {
        damageDealer.Damage++;
        CurrentLevel++;
    }

    public bool CanUpgrade(OrbitFlight orbit)
    {
        return playerResources.HaveResources(orbit.OrbitStats.Cost.ToArray());
    }
}