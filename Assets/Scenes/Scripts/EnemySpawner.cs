using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform Princess;
    public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
    public float Delay = 1f;
    public Transform PrefabsCopyParent;

    private int originalEnemyCount = 0;
    private List<Bounds> spawnBounds = new List<Bounds>();
    private IEnemyCopier<EnemyController> enemyCopier;
    private int currentMaxWeight = 1;

    void Start()
    {
        TimeManager.OnIncreaseDifficult.AddListener(IncreaseDifficulty);
        SetSpawnBounds();
        StartCoroutine(SpawnEnemies());
        enemyCopier = new EnemyCopier();
        originalEnemyCount = EnemyPrefabs.Count;
    }

    private void SetSpawnBounds()
    {
        var mainCamera = Camera.main;
        var cameraMin = mainCamera.ScreenToWorldPoint(new Vector3(0, 0));
        var cameraMax = mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight));
        var cameraCenter = (cameraMin + cameraMax) / 2;

        var upBounds = new Bounds(new Vector3(cameraCenter.x, cameraMax.y + 3f), new Vector3(Mathf.Abs(cameraMax.x) + Mathf.Abs(cameraMin.x) + 1f, 2f));
        var downBounds = new Bounds(new Vector3(cameraCenter.x, cameraMin.y - 3f), new Vector3(Mathf.Abs(cameraMax.x) + Mathf.Abs(cameraMin.x) + 1f, 2f));
        var leftBounds = new Bounds(new Vector3(cameraMin.x - 3f, cameraCenter.y), new Vector3(2f, Mathf.Abs(cameraMax.y) + Mathf.Abs(cameraMin.y) + 1f));
        var rightBounds = new Bounds(new Vector3(cameraMax.x + 3f, cameraCenter.y), new Vector3(2f, Mathf.Abs(cameraMax.y) + Mathf.Abs(cameraMin.y) + 1f));

        spawnBounds.Add(upBounds);
        spawnBounds.Add(downBounds);
        spawnBounds.Add(leftBounds);
        spawnBounds.Add(rightBounds);
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (EnemyPrefabs == null)
                break;
            yield return new WaitForSeconds(Delay);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var selelctedEnemy = SelectEnemy();
        var side = GetSideToSpawn();
        var spawnPosition = GetPositionToSpawn(side);
        var enemy = Instantiate(selelctedEnemy, spawnPosition, Quaternion.identity);
        enemy.gameObject.SetActive(true);
        enemy.transform.localScale = (Princess.position - enemy.transform.position).normalized.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        enemy.GetComponent<EnemyMover>().Princess = Princess;
    }
        
    private EnemyController SelectEnemy()
    {
        if(EnemyPrefabs == null || EnemyPrefabs.Count == 0)
            throw new ArgumentException("Enemy prefabs not found.", nameof(gameObject.name));

        var chances = new List<float>();
        for(var i = 0; i < EnemyPrefabs.Count; i++)
            chances.Add(EnemyPrefabs[i].Characteristics.Weight);

        var value = UnityEngine.Random.Range(0, chances.Sum());
        float sum;
        for (var i = 0; i < 1000; i++)
        {
            sum = 0f;

            for (var j = 0; j < chances.Count; j++)
            {
                sum += chances[j];
                if (value < sum && EnemyPrefabs[j].Characteristics.Weight <= currentMaxWeight)
                    return EnemyPrefabs[j];              
            }
        }
        return EnemyPrefabs[0];
    }

    private Vector2 GetPositionToSpawn(Bounds side)
    {
         return new Vector2(UnityEngine.Random.Range(side.min.x, side.max.x), UnityEngine.Random.Range(side.min.y, side.max.y));
    }

    private Bounds GetSideToSpawn()
    {
        return spawnBounds[UnityEngine.Random.Range(0, spawnBounds.Count)];
    }

    private void IncreaseDifficulty()
    {
        currentMaxWeight += UnityEngine.Random.Range(1, 3);
        var newEnemies = new List<EnemyController>();
        Delay = Mathf.Clamp(Delay - 0.1f, 0.1f, 10);
        for(var i = 1; i <= originalEnemyCount; i++)
        {
            var newEnemy = enemyCopier.CopyObject(EnemyPrefabs[^i]);
            newEnemy.gameObject.transform.SetParent(PrefabsCopyParent);
            newEnemy.gameObject.SetActive(false);
            newEnemy.Characteristics += newEnemy.AdderCharacteristics;
            newEnemy.name = EnemyPrefabs[^i].name + $"{EnemyPrefabs.Count / originalEnemyCount}";
            newEnemies.Add(newEnemy);
        }
        EnemyPrefabs.AddRange(newEnemies);
    }

    #region Draw on gizmos spawner bounds
    private void OnDrawGizmos()
    {
        if (spawnBounds.Count == 0) return;
        Gizmos.color = Color.blue;
        foreach(var bounds in spawnBounds)
        {
            DrawBounds(bounds);
        }
    }

    private void DrawBounds(Bounds bounds)
    {
        Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.min.y), new Vector3(bounds.max.x, bounds.min.y));
        Gizmos.DrawLine(new Vector3(bounds.max.x, bounds.min.y), new Vector3(bounds.max.x, bounds.max.y));
        Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.max.y), new Vector3(bounds.max.x, bounds.max.y));
        Gizmos.DrawLine(new Vector3(bounds.min.x, bounds.max.y), new Vector3(bounds.min.x, bounds.min.y));
    }
    #endregion
}
