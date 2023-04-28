using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsReader : MonoBehaviour
{
    private GameProgressWritter progressWritter;
    void Start()
    {
        progressWritter = FindAnyObjectByType<GameProgressWritter>();
        if (progressWritter != null)
            Destroy(progressWritter.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
