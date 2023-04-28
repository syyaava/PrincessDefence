using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOrbitUpgradeData", menuName = "Data/OrbitUpgradeData")]
public class OrbitFlightStats : ScriptableObject
{
    public float Radius = 2.5f;
    public float Speed = 1f;
    public float SpeedMagnifierValue = 1f;
    public UpgradeType SpeedMagnifierType = UpgradeType.Adder;
    public UpgradeType CostMagnifierType = UpgradeType.Adder;
    public List<Resource> Cost = new List<Resource>();
    public List<Resource> CostMagnifier = new List<Resource>();
    
    public OrbitFlightStats LevelUp()
    {
        if (Cost.Count != CostMagnifier.Count || CostMagnifier.All(x => Cost.Contains(x)))
            throw new ArgumentException("\"Cost\" and \"Cost magnifier\" lists differ by resources.");

        var newUpgradeInfo = new OrbitFlightStats();        
        newUpgradeInfo.Radius = Radius;
        newUpgradeInfo.Speed = Speed;
        IncreaseSpeed(newUpgradeInfo);
        newUpgradeInfo.SpeedMagnifierValue = SpeedMagnifierValue;
        newUpgradeInfo.SpeedMagnifierType = SpeedMagnifierType;
        newUpgradeInfo.CostMagnifierType = CostMagnifierType;
        newUpgradeInfo.Cost = IncreaseUpgradeCost();
        newUpgradeInfo.CostMagnifier = CostMagnifier;

        return newUpgradeInfo;
    }

    private void IncreaseSpeed(OrbitFlightStats newUpgradeInfo)
    {
        if (SpeedMagnifierType == UpgradeType.Adder)
            newUpgradeInfo.Speed +=  SpeedMagnifierValue;
        else if (SpeedMagnifierType == UpgradeType.Multiplier)
            newUpgradeInfo.Speed *= SpeedMagnifierValue;
    }

    private List<Resource> IncreaseUpgradeCost()
    {
        var newResourceList = new List<Resource>();
        foreach (var resource in Cost)
        {
            var newResource = new Resource()
            {
                Type = resource.Type,
                Count = resource.Count,
            };

            var costRes = CostMagnifier.Find(x => x.Type == resource.Type);
            if (CostMagnifierType == UpgradeType.Adder)
                newResource.Count += costRes.Count;
            else if (CostMagnifierType == UpgradeType.Multiplier)
                newResource.Count = Mathf.RoundToInt(costRes.Count * resource.Count);

            newResourceList.Add(newResource);         
        }
        return newResourceList;
    }
}

public enum UpgradeType
{
    Adder,
    Multiplier
}