using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyCharacteristics", menuName = "Data/Enemy Characteristics")]
public class EnemyCharacteristics : ScriptableObject
{
    public int Weight = 1;
    public EnemyType Type = EnemyType.Simple;
    public float Speed = 3f;

    public static EnemyCharacteristics operator +(EnemyCharacteristics a,EnemyCharacteristics b)
    {
        return new EnemyCharacteristics()
        {
            Weight = a.Weight + b.Weight,
            Type = a.Type,
            Speed = a.Speed + b.Speed,
        };
    }
}
