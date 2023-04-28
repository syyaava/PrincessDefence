using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressWritter : MonoBehaviour
{
    public static GameProgressWritter Instance;

    public uint FragsCount;
    public uint Score;
    private Dictionary<EnemyType, uint> fragsByType;

    void Start()
    {
        Initiate();
    }

    private void Initiate()
    {        
        Instance = this;
        fragsByType = new Dictionary<EnemyType, uint>();
        DontDestroyOnLoad(gameObject);
        FragsCount = 0;
        Score = 0;
    }

    public void SelectGameStatisticInEnd()
    {
        Score = (uint)ResourceController.Instance.GetResourceCount(Resource.ResourceType.Score);
    }

    public void AddEnemyToStats(EnemyController enemy)
    {
        FragsCount++;        
        if (!fragsByType.ContainsKey(enemy.Characteristics.Type))
            fragsByType.Add(enemy.Characteristics.Type, FragsCount);
        else
            fragsByType[enemy.Characteristics.Type]++;
    }
}
