using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public List<Resource> Rewards = new List<Resource>();

    public void Upgrade()
    {
        var rewards = new List<Resource>();
        foreach (var r in Rewards)
        {
            rewards.Add(new Resource()
            {
                Type = r.Type,
                Count = r.Count + 1,
            });
        }
        Rewards = rewards;
    }
}
