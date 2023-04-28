using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static UnityEvent OnIncreaseDifficult = new UnityEvent();

    public float IncreaseDifficultyTime = 90f;
    
    private void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(IncreaseDifficult());
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator IncreaseDifficult()
    {
        while (true)
        {
            yield return new WaitForSeconds(IncreaseDifficultyTime);
            OnIncreaseDifficult?.Invoke();
        }
    }
}
