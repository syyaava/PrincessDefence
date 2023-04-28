using System.Linq;
using UnityEngine;

public class EnemyCopier : MonoBehaviour, IEnemyCopier<EnemyController>
{
    public EnemyController CopyObject(EnemyController obj)
    {
        var newEnemy = Instantiate(obj);
        newEnemy.Characteristics = new EnemyCharacteristics()
        {
            Speed = obj.Characteristics.Speed,
            Type = obj.Characteristics.Type,
            Weight = obj.Characteristics.Weight,
        };        
        newEnemy.AdderCharacteristics = obj.AdderCharacteristics;

        var damageble = newEnemy.GetComponent<Damageble>();
        damageble.MaxHealth = obj.GetComponent<Damageble>().MaxHealth + 1;

        newEnemy.Reward = newEnemy.GetComponent<Reward>();
        var score = newEnemy.Reward.Rewards.FirstOrDefault(x => x.Type == Resource.ResourceType.Score);
        if (score != default)
            score.Count++;

        return newEnemy;
    }
}
