using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyCharacteristics Characteristics;
    public EnemyCharacteristics AdderCharacteristics;
    public Reward Reward;

    private EnemyMover mover;

    void Awake()
    {
        mover = GetComponent<EnemyMover>();
        Reward = GetComponent<Reward>();
        mover.Speed = Characteristics.Speed;
    }

    void FixedUpdate()
    {
        mover.Moving();
    }

    public void GetReward()
    {
        ResourceController.Instance.AddResources(Reward.Rewards.ToArray());
        GameProgressWritter.Instance.AddEnemyToStats(this);
    }
}

public enum EnemyType
{
    Simple,
    Fast,
    Big
}
