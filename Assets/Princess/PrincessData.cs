using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPrincessData", menuName = "Data/PrincessData")]
public class PrincessData : ScriptableObject
{
    public int MaxHp = 10;
    public int HpMagnifier = 2;
    public UpgradeType CostMagnifierType = UpgradeType.Adder;
    public List<Resource> Cost = new List<Resource>();
    public List<Resource> CostMagnifier = new List<Resource>();

    public PrincessData LevelUp()
    {
        var newPrincessData = new PrincessData();
        newPrincessData.MaxHp = MaxHp + HpMagnifier;
        newPrincessData.HpMagnifier = HpMagnifier;
        newPrincessData.CostMagnifierType = CostMagnifierType;
        newPrincessData.Cost = new List<Resource>();
        foreach(var resource in Cost)
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

            newPrincessData.Cost.Add(newResource);
        }
        newPrincessData.CostMagnifier = CostMagnifier;
        return newPrincessData;
    }
}
