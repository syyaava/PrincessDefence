using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverStatistic : MonoBehaviour
{
    private TMP_Text gameOverText;

    void Start()
    {
        gameOverText = GetComponent<TMP_Text>();
    }

    public void GameSessionStatisticOutput()
    {
        gameOverText.text = $"Princess dead. Game over!\n" +
            $"Score: {GameProgressWritter.Instance.Score};\n" +
            $"Kills: {GameProgressWritter.Instance.FragsCount};";
    }
}
